using AAV.Sys.Ext;
using AAV.Sys.Helpers;
using SpellingBee.Db.DbModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SpellingBee
{
  public partial class MainWindow : Window
  {
    List<Problem> _probList;    //string[] _probList = new string[] { "Abandon", "Ornament", "Tendency", "Specifications", "Ascertain", "Affidavit", "Hysterical", "Supplemental", "Cerebral", "Dismal", "Perpetual", "Upheaval", "Pollen", "Succulent", "Immigration", "Perpendicular", "Glacier", "Occasionally", "Acquitted", "Forbidden", "Deterring", "Monotone", "Semiprecious", "Decimeter", "Milliliter", "Milligram", "Monolith", "Protrude", "Preamble", "Antecedent", "Commencement", "Euphoria", "Benefactor", "Contraband", "Benign", "Deplete", "Maladjusted", "Metamorphosis", "Dissatisfaction", "Malevolent", "Metaphor", "Descendant", "Metabolism", "Aquarium", "Quadrilateral", "Simultaneous", "Hospice", "Mortgage", "Inexplicable", "Suspended", "Duplicate", "Perplexing", "Pendulum", "Advocate", "Provoke", "Variation", "Concentric", "Exterminate", "Terrain", "Vagabond", "Eccentric", "Inspiration", "Expectation", "Perspective", "Transpired", "Anxiety", "Eminent", "Sophomore", "Eloquent", "Equally", "Fallacy", "Courtesy", "Controversial", "Syllabus", "Cooperative", "Synthesis", "Coherent", "Collateral", "Synchronize", "Symptom", "Anemia", "Invalid", "Negligent", "Impersonate", "Anesthesia", "Nonchalant", "Immobilize", "Incognito", "Impatiently", "Anonymous", "Maximum", "Juvenile", "Opulent", "Quarantine", "Exhilarated", "Ordinance", "Exorbitant", "Molecular", "Regime", "Elastic", "Aeronautics", "Architect", "Theology", "Aristocrat", "Thermostat", "Parasite", "Enthusiasm", "Barometer", "Itemize", "Jubilant", "Compromise", "Permanent", "Capability", "Generosity", "Complimentary", "Infirmary", "Revolutionary", "Protege", "Opaque", "Resume", "Etiquette", "Fiancee", "Dilapidated", "Tyranny", "Disastrous", "Indigestion", "Perennial", "Iodine", "Larynx", "Administrator", "Manicurist", "Veterinarian", "Arbitrator", "Personification", "Allegory", "Simile", "Alliteration", "Pseudonym", "Exposition", "Stanza" };
    readonly SpeechSynthesizer _synth = new SpeechSynthesizer();
    int curIdx = 0, ttlAnswers = 0, ttlMistake = 0, ttlCorrect = 0, rereads = -1, _doneSinceTime0;
    A0DbContext _db;
    string _curWord;
    readonly MyData ErrList = new MyData();
    DateTime _startedThinkAt;
    readonly DateTime _time0 = new DateTime(2019, 8, 16);
    bool _ready = false;
    const int _maxErrors = 99, _dailyQuota = 32;
    bool _correctingTheMistake = false, _skipSavingOfCorrection = false;

    public MainWindow()
    {
#if __DEBUG
			TTS_Console_Sample_1.xPoc.M1();
			synth.Speak("Shutting down now...");
			Application.Current.Shutdown();
#endif

      InitializeComponent();
      KeyUp += new KeyEventHandler((s, e) =>
      {
        switch (e.Key)
        {
          case Key.F5: { reRead(); } break;
          case Key.Escape: { Close(); Application.Current.Shutdown(); } break;
        }
      }); //tu:
      MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => DragMove()); //tu:

      t1.Focus();
    }

    public static readonly DependencyProperty AnswerProperty = DependencyProperty.Register("Answer", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string Answer { get => (string)GetValue(AnswerProperty); set => SetValue(AnswerProperty, value); }
    public static readonly DependencyProperty InfoMsgProperty = DependencyProperty.Register("InfoMsg", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string InfoMsg { get => (string)GetValue(InfoMsgProperty); set => SetValue(InfoMsgProperty, value); }
    public static readonly DependencyProperty DetailsProperty = DependencyProperty.Register("Details", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string Details { get => (string)GetValue(DetailsProperty); set => SetValue(DetailsProperty, value); }
    public static readonly DependencyProperty ErroneusProperty = DependencyProperty.Register("Erroneus", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string Erroneus { get => (string)GetValue(ErroneusProperty); set => SetValue(ErroneusProperty, value); }
    public static readonly DependencyProperty CorrectnProperty = DependencyProperty.Register("Correctn", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string Correctn { get => (string)GetValue(CorrectnProperty); set => SetValue(CorrectnProperty, value); }
    public static readonly DependencyProperty BtnTextProperty = DependencyProperty.Register("BtnText", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string BtnText { get => (string)GetValue(BtnTextProperty); set => SetValue(BtnTextProperty, value); }
    public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(MainWindow), new PropertyMetadata(0)); public int Score { get => (int)GetValue(ScoreProperty); set => SetValue(ScoreProperty, value); }
    public static readonly DependencyProperty CurUserProperty = DependencyProperty.Register("CurUser", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string CurUser { get => (string)GetValue(CurUserProperty); set => SetValue(CurUserProperty, value); }
    public static readonly DependencyProperty NewUserProperty = DependencyProperty.Register("NewUser", typeof(string), typeof(MainWindow), new PropertyMetadata("")); public string NewUser { get => (string)GetValue(NewUserProperty); set => SetValue(NewUserProperty, value); }
    public static readonly DependencyProperty CorrectionVisibilityProperty = DependencyProperty.Register("CorrectionVisibility", typeof(Visibility), typeof(MainWindow)); public Visibility CorrectionVisibility { get => (Visibility)GetValue(CorrectionVisibilityProperty); set => SetValue(CorrectionVisibilityProperty, value); }
    public string EnvUser
    {
#if DEBUG
      get { return "ZoePi"; }
#else
      get { return Environment.UserName; }
#endif
    }

    class Score1
    {
      public string DoneBy { get; set; }
      public int? Nailed { get; set; }
      public int? Missed { get; set; }
    }

    async void Window_Loaded(object s, RoutedEventArgs e)
    {
      try
      {
        var lst = _synth.GetInstalledVoices(/*new CultureInfo("fr-CA")*/);
        _synth.SpeakAsync($"{lst.Count} voices are installed.");
        await Task.Delay(555);

        _synth.SpeakAsync("Press Enter when ready.");


        // Aug 2019: db compatibilty can only be ...
        ////MyDbCreatorSame.CreateDbIfNotThere();
        //MyDbCreatorSame.setDbInitializer();
        ////var source = A0DbContext.Create().Problems.ToList(); // var db = A0DbContext.Create();				db.Problems.Load();				foreach (var mi in db.Problems.Local) Debug.WriteLine(mi);

        _db = A0DbContext.Create();

        VersioInfo.Text = $"{VerHelper.CurVerStr(".NET 4.8")}\n{_db.ServerDatabase()}";

        var batches = _db.Problems.Select(r => r.BatchSource).Distinct();
        var binding2 = new Binding { Source = new ObservableCollection<string>(batches.ToList()) };
        lkuB.SetBinding(ListBox.ItemsSourceProperty, binding2);
      }
      catch (Exception ex) { ex.Log(); MessageBox.Show(ex.Message, "Exception"); }

      load(1);
      ((CollectionViewSource)(FindResource("lkuLanguageViewSource"))).Source = _db.LkuLanguages.Local;
      _ready = true;
    }

    void load(int langId, string batch = "")
    {
      try
      {
        dg1.DataContext = _db.Database.SqlQuery<Score1>(sqlScore).ToList();

        //_db.Problems.Load();
        _db.Auditions.Load();
        _db.TombStones.Load();
        _db.LkuLanguages.Load();

        var cu = _db.TombStones.Local.FirstOrDefault();
        CurUser = cu == null || string.IsNullOrEmpty(cu.User) ? EnvUser : cu.User;

        //var ws = _db.Database.SqlQuery(typeof(Problem), sqlProblemsOrdered, new SqlParameter("@User", CurUser), new SqlParameter("@LangID", langId));
        var wt = _db.Database.SqlQuery<Problem>(sqlProblemsOrdered, new SqlParameter("@User", CurUser), new SqlParameter("@LangID", langId), new SqlParameter("@BatchSource", batch));

        _probList = wt.Cast<Problem>().Select(p => p).ToList(); ; // _db.Problems.ToList();				//_probList = _db.Problems.ToList();
        VersioInfo.Text = $"{VerHelper.CurVerStr(".NET 4.8")}\n{_db.ServerDatabase()}\n{_probList.Count} ttl words";

        _doneSinceTime0 = _db.Auditions.Count(r => r.IsCorrect && r.DoneBy.Equals(EnvUser, StringComparison.OrdinalIgnoreCase) && r.DoneAt > _time0);

        if (_probList.Count < 1) return;

        _curWord = _probList[curIdx].ProblemText; // cw = _db.TestWords.FirstOrDefault().BeeWord;
      }
      catch (Exception ex) { ex.Log(); MessageBox.Show(ex.Message, "Exception"); }
    }
    void reRead()
    {
      _synth.Rate = -(3 * rereads) % 10;
      _synth.SpeakAsync(_curWord);

      if (rereads == -1) _startedThinkAt = DateTime.Now;
      rereads++;
    }
    void runThroughPopup(string words) { if (new TakeInNewBatch(words).ShowDialog() == true) load(1); }
    void onCbxChange()
    {
      var l = ((LkuLanguage)(lkuL.SelectedItem)).ID; // ((SpellingBee.Model.Models.LkuLanguage)(((object[])(e.AddedItems))[0])).ID
      switch (l)
      {
        default:
        case 1: _synth.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo("en")); _synth.SpeakAsync("Hello, everybody!"); break;
        case 2: _synth.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo("fr-FR")); _synth.SpeakAsync("Bonjour à tous, je cherche désespérément "); break;
        case 3: _synth.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo("ru")); _synth.SpeakAsync("Всем привет! Путин дурак; развалил Россию!"); break;
        case 4: _synth.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo("zh")); _synth.SpeakAsync("Nee How. Nee cheer la ma."); break;
        case 5: _synth.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo("he")); _synth.SpeakAsync("Shalom, motyk."); break;
      }

      var b = (lkuB.SelectedValue as string) ?? "";

      load(l, b);

      onEnter(null, null);
      t1.Focus();
    }
    static void openTextFileIntoTheRichTextbox(DragEventArgs e)
    {
      var docPath = (string[])e.Data.GetData(DataFormats.FileDrop);

      if (System.IO.File.Exists(docPath[0]))
      {
        try
        {
          // Open the document in the 
          var richTextBox1 = new RichTextBox(); //todo:	have it on the window.
          var range = new System.Windows.Documents.TextRange(richTextBox1.Document.ContentStart, richTextBox1.Document.ContentEnd);
          var fStream = new System.IO.FileStream(docPath[0], System.IO.FileMode.OpenOrCreate);
          range.Load(fStream, e.KeyStates == DragDropKeyStates.ShiftKey ? DataFormats.Text : DataFormats.Rtf);
          fStream.Close();
        }
        catch (System.Exception ex) { MessageBox.Show(ex.Message, "File could not be opened. Make sure the file is a text file."); }
      }
    }

    void onReRead(object s, RoutedEventArgs e) => reRead();
    void onEnter(object s, RoutedEventArgs e)
    {
      try
      {
        if (_probList.Count < 1) return;

        if (Answer.Length == 0)
        {
          reRead();
        }
        else
        {
          _synth.Rate = 0;
          var correctIf0 = string.Compare(_curWord.Replace("’", "").Replace("'", ""), Answer.Replace("'", "").Trim(), true);
          if (correctIf0 != 0)
          {
            _synth.SpeakAsync("Oopsy... Reenter!");
            ErrList.Add((Erroneus = Answer) + (Correctn = _curWord));
            CorrectionVisibility = btnBadSay.Visibility = Visibility.Visible;
            ttlMistake++;
            _correctingTheMistake = true;
          }
          else
          {
            Correctn = Erroneus = "";
            CorrectionVisibility = btnBadSay.Visibility = Visibility.Collapsed;

            if (_correctingTheMistake)
            {
              _correctingTheMistake = false;
              _skipSavingOfCorrection = true;
              _synth.SpeakAsync("Corrected.");
            }
            else
            {
              ttlCorrect++;
              _synth.SpeakAsync("Good!");
            }
          }

          var newAudition = new Audition
          {
            Problem = _probList[curIdx],
            IsCorrect = correctIf0 == 0,
            PlayerAnswer = Answer.Trim(),
            DoneBy = CurUser,
            DoneAt = DateTime.Now,
            TimeToAnswerSec = (DateTime.Now - _startedThinkAt).TotalSeconds,
            ReReadCount = rereads
          };

          btnBadSay.Tag = newAudition;

          if (_skipSavingOfCorrection)
            _skipSavingOfCorrection = false;
          else
          {
            var dbAud = _db.Auditions.Add(newAudition);
            _db.Problems.Attach(_probList[curIdx]); //tu: prevent new row inserts for the existing Problem.
            var rowsSaved = _db.SaveChanges(); //
            Debug.WriteLine($"{rowsSaved} Rows saved.");
            //_synth.SpeakAsync($"{rowsSaved} Rows saved.");
          }

          if (correctIf0 == 0) // May 2019
          {
            ttlAnswers++;
            curIdx = curIdx < _probList.Count - 1 ? curIdx + 1 : 0;
            _curWord = _probList[curIdx].ProblemText; //cw = _db.TestWords.SequenceEqual.[cur].BeeWord;
          }

          if (ttlMistake >= _maxErrors)
          {
            _synth.SpeakAsync("Game Over: Too many mistakes.\r\n\nMemorise correct spelling for these words and start again.");

            var words = string.Join("\r\n    ", _db.Auditions.Where(r => !r.IsCorrect).OrderByDescending(r => r.DoneAt).Select(r => r.Problem.ProblemText).Take(_maxErrors).ToArray());

            MessageBox.Show($"Too many mistakes.\r\n\nMemorise correct spelling for these words and start again:\r\n\n    {words}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            Close(); Application.Current.Shutdown();
          }
          else
          {
            Answer = "";
            _synth.SpeakAsync(_curWord);
            _startedThinkAt = DateTime.Now;
            rereads = 0;
          }
        }

        var days = (int)(DateTime.Today - _time0).TotalDays;

        var todo = days * _dailyQuota - _doneSinceTime0 - ttlCorrect;
        if (todo < 0) // if entered over the _hitsPerDay yesterday.
          todo = _dailyQuota - ttlCorrect;

        InfoMsg = $"{ttlCorrect,3}    {todo,3}    {((ttlAnswers == 0 ? 0 : 100 * (ttlCorrect) / ttlAnswers)),4:N0} ";
      }
      catch (Exception ex) { ex.Log(); MessageBox.Show(ex.Message, "Exception"); }
    }
    void btnBadSay_Click_1(object s, RoutedEventArgs e)
    {
      try
      {
        btnBadSay.Visibility = Visibility.Collapsed;

        if (btnBadSay.Tag != null && btnBadSay.Tag is Audition)
        {
          var uad = btnBadSay.Tag as Audition;
          //			var dba1 = _db.Auditions.Where(d => Math.Abs((int)System.Data.Objects.EntityFunctions.DiffSeconds(d.DoneAt, uad.DoneAt)) < 2.0).FirstOrDefault();
          var dba1 = _db.Auditions.Where(d => Math.Abs((d.DoneAt - uad.DoneAt).TotalSeconds) < 2.0).FirstOrDefault();
          dba1.IsBadSaying = true;
          _db.SaveChanges();
        }
      }
      catch (Exception ex) { ex.Log(); MessageBox.Show(ex.Message, "Exception"); }
    }
    void Button_Click_2(object s, RoutedEventArgs e)
    {
      _synth.Speak("Bye-bye.");
      Close();
    }
    void Button_Click_3(object s, RoutedEventArgs e)
    {
      try
      {
        CurUser = NewUser;
        if (_db.TombStones.FirstOrDefault() == null)
          _db.TombStones.Add(new TombStone { ID = "1", User = CurUser, Player = new Player { ID = CurUser, FullName = CurUser, AddedAt = DateTime.Now, AddedBy = EnvUser } });
        else
          _db.TombStones.FirstOrDefault().User = CurUser;
        _db.SaveChanges();
      }
      catch (Exception ex) { ex.Log(); MessageBox.Show(ex.Message, "Exception"); }

      load(1);

      NewUser = "";
    }
    void onLoadClipboardIntoDb(object s, RoutedEventArgs e) => runThroughPopup(Clipboard.GetText());
    void mw_Drop_1(object s, DragEventArgs e)
    {
      try
      {
        if (e.Data.GetDataPresent(DataFormats.Text))
        {
          var test = (string)e.Data.GetData(DataFormats.Text);
          runThroughPopup(test);
        }
        else if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
          openTextFileIntoTheRichTextbox(e);
        }
      }
      catch (Exception ex) { ex.Log(); MessageBox.Show(ex.Message, "Exception"); }
    }
    void Button_Click_1(object s, RoutedEventArgs e)
    {
      try
      {
        foreach (var voice in _synth.GetInstalledVoices())
        {
          var info = voice.VoiceInfo;
          var AudioFormats = "";
          foreach (var fmt in info.SupportedAudioFormats)
          {
            AudioFormats += string.Format("{0}\n", fmt.EncodingFormat.ToString());
          }

          Debug.WriteLine(" Name:          " + info.Name);
          Debug.WriteLine(" Culture:       " + info.Culture);
          Debug.WriteLine(" Age:           " + info.Age);
          Debug.WriteLine(" Gender:        " + info.Gender);
          Debug.WriteLine(" Description:   " + info.Description);
          Debug.WriteLine(" ID:            " + info.Id);
          Debug.WriteLine(" Enabled:       " + voice.Enabled);
          if (info.SupportedAudioFormats.Count != 0)
          {
            Debug.WriteLine(" Audio formats: " + AudioFormats);
          }
          else
          {
            Debug.WriteLine(" No supported audio formats found");
          }

          var AdditionalInfo = "";
          foreach (var key in info.AdditionalInfo.Keys)
          {
            AdditionalInfo += string.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
          }

          Debug.WriteLine(" Additional Info - " + AdditionalInfo);
        }


        _synth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior, 1);
        //synth.Volume
        _synth.Rate = -7;
        _synth.Speak(_probList[curIdx].ProblemText);
        _synth.Rate = 0;
      }
      catch (Exception ex) { ex.Log(); MessageBox.Show(ex.Message, "Exception"); }
    }
    void OnTest(object s, RoutedEventArgs e) => TTS_Console_Sample_1.xPoc.M1();
    void lkuL_SelectionChanged(object s, SelectionChangedEventArgs e) { if (_ready) onCbxChange(); }
    void lkuB_SelectionChanged(object s, SelectionChangedEventArgs e) { if (_ready) onCbxChange(); }

    #region SQL

    const string sqlProblemsOrdered = @"
SELECT        ID, ProblemText, SolutionText, HintMessage, BatchSource, Notes, Grade, AddedAt, AddedBy, DeletedAt, DeletedBy, Level_ID, Language_ID,
														 (SELECT        COUNT(*) AS Expr1
															 FROM            SpB.Audition AS Audition_1
															 WHERE        (Problem_ID = Problem.ID) AND (DoneBy = @User) AND (IsCorrect = 0)) -
														 (SELECT        COUNT(*) AS Expr1
															 FROM            SpB.Audition AS Audition_2
															 WHERE        (Problem_ID = Problem.ID) AND (DoneBy = @User) AND (IsCorrect = 1)) AS WrongsMinusRights,
														 (SELECT        COUNT(*) AS Expr1
															 FROM            SpB.Audition AS Audition_2
															 WHERE        (Problem_ID = Problem.ID) AND (DoneBy = @User) AND (IsCorrect = 1)) AS Rights,
														 (SELECT        COUNT(*) AS Expr1
															 FROM            SpB.Audition AS Audition_1
															 WHERE        (Problem_ID = Problem.ID) AND (DoneBy = @User) AND (IsCorrect = 0)) AS Wrongs,
														 (SELECT        COUNT(*) AS Expr1
															 FROM            SpB.Audition
															 WHERE        (Problem_ID = Problem.ID) AND (DoneBy = @User)) AS Ttl,
														 (SELECT        MAX(DoneAt) AS Expr1
															 FROM            SpB.Audition AS Audition_3
															 WHERE        (Problem_ID = Problem.ID) AND (DoneBy = @User)) AS LastAt,
														 (SELECT        COUNT(*) AS Expr1
															 FROM            SpB.Audition AS Audition_4
															 WHERE        (Problem_ID = Problem.ID) AND (IsBadSaying = 1)) AS TtlMarkedBadSaid
FROM            SpB.Problem
WHERE        Language_ID = @LangID AND (@BatchSource = '' OR BatchSource = @BatchSource)
ORDER BY TtlMarkedBadSaid, WrongsMinusRights DESC, LastAt",

 sqlScore = @"
	SELECT        DoneBy,
			(SELECT        COUNT(*) AS Nailed
				FROM            SpB.Audition AS t2
				WHERE        (IsCorrect = 1) AND (t1.DoneBy = DoneBy)
				GROUP BY DoneBy) AS Nailed,
			(SELECT        COUNT(*) AS Missed
				FROM            SpB.Audition AS t2
				WHERE        (IsCorrect = 0) AND (t1.DoneBy = DoneBy)
				GROUP BY DoneBy) AS Missed
	FROM            SpB.Audition AS t1
	GROUP BY DoneBy ";

    #endregion
  }

  public class MyData : ObservableCollection<string>
  {
    public MyData()
    {
      //Add("Item 1");
      //Add("Item 2");
      //Add("Item 3");
    }
  }
}

/// use Stamina sounds
/// publish to Azure
/// 

///F-keys: read again, read my version, mark as bad pronaunsciatoin'
///speak slower
///dispute, mark as bad pronounse button
///

/*var result = (
	 from p in ctx.People
	 where p.Id == 1
	 select new {
			Person = p, 
			Cars = p.Cars.Count(), 
			Houses = p.Houses.Count()
	 }).FirstOrDefault();
 
 

down voteaccepted
 


Here is a LINQ to Entities query, for example: 
var q = from role in context.Roles
	select new {
		role.RoleID,
		role.RoleName,
		UserCount = role.UserRoles.Count()
	}; 
 * 
 * 
 * 
 * students = students.OrderByDescending(s => s.LastName); 
						break; 

 * 
 * 
 */


public static class DbContext_Ext // fragment from C:\g\TypeCatch\Src\sln\TypeCatch\AsLink\Ext.DbContext.cs
{
  public static string ServerDatabase(this DbContext db)
  {
    var kvpList = db.Database.Connection.ConnectionString.Split(';').ToList();
    var ds = getConStrValue(kvpList, "data source") + "\\";
    return $"{(ds.Equals(@"(localdb)\MSSQLLocalDB") ? "" : ds.Contains("database.windows.net") ? "Azure\\" : ds)}{getConStrValue(kvpList, "AttachDbFilename")}{getConStrValue(kvpList, "initial catalog")}";
  }
  static string getConStrValue(System.Collections.Generic.List<string> lst, string ss) => lst.FirstOrDefault(r => r.Split('=')[0].Equals(ss, StringComparison.OrdinalIgnoreCase))?.Split('=')[1];
}
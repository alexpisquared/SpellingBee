using SpellingBee.Db.DbModel;

using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Media;

namespace SpellingBee.DbIni
{
	public class MyDbCreatorSame : DropCreateDatabaseIfModelChanges<A0DbContext>
	{
		public static void CreateDbIfNotThere()
		{
			try
			{
				setDbInitializer();

        var source = A0DbContext.Create().Problems.ToList(); // var db = A0DbContext.Create();				db.Problems.Load();				foreach (var mi in db.Problems.Local) Debug.WriteLine(mi);
			}
			catch (Exception ex) { SystemSounds.Exclamation.Play(); Console.WriteLine(ex); throw; }
		}
		public static void setDbInitializer()
		{
			try
			{
				Database.SetInitializer(new CreateDatabaseIfNotExists<A0DbContext>());
				Database.SetInitializer(new DropCreateDatabaseIfModelChanges<A0DbContext>());
				Database.SetInitializer(new MyDbCreatorSame());
			}
			catch (Exception ex) { new System.Speech.Synthesis.SpeechSynthesizer().SpeakAsync("Oh Shit!" + ex.Message); }
		}

		protected override void Seed(A0DbContext x)
		{
			new System.Speech.Synthesis.SpeechSynthesizer().Speak("Wait! New database is been generated...");

			base.Seed(x);

			var now = DateTime.Now;

			var Level0 = x.LkuLevels.Add(new LkuLevel { Name = "0", Desc = "Unknown" });
			var Level1 = x.LkuLevels.Add(new LkuLevel { Name = "1", Desc = "Super Easy" });
			var Level2 = x.LkuLevels.Add(new LkuLevel { Name = "2", Desc = "Easy" });
			var Level3 = x.LkuLevels.Add(new LkuLevel { Name = "3", Desc = "Moderate" });
			var Level4 = x.LkuLevels.Add(new LkuLevel { Name = "4", Desc = "Difficult" });
			var Level5 = x.LkuLevels.Add(new LkuLevel { Name = "5", Desc = "Super Difficult" });
			var Level6 = x.LkuLevels.Add(new LkuLevel { Name = "6", Desc = "Impossible" });

			var l1 = x.LkuLanguages.Add(new LkuLanguage { Name = "En", Desc = "English" });
			var l2 = x.LkuLanguages.Add(new LkuLanguage { Name = "Fr", Desc = "French" });


			string[] words = new string[] { "Abandon", "Ornament", "Tendency", "Specifications", "Ascertain", "Affidavit", "Hysterical", "Supplemental", "Cerebral", "Dismal", "Perpetual", "Upheaval", "Pollen", "Succulent", "Immigration", "Perpendicular", "Glacier", "Occasionally", "Acquitted", "Forbidden", "Deterring", "Monotone", "Semiprecious", "Decimeter", "Milliliter", "Milligram", "Monolith", "Protrude", "Preamble", "Antecedent", "Commencement", "Euphoria", "Benefactor", "Contraband", "Benign", "Deplete", "Maladjusted", "Metamorphosis", "Dissatisfaction", "Malevolent", "Metaphor", "Descendant", "Metabolism", "Aquarium", "Quadrilateral", "Simultaneous", "Hospice", "Mortgage", "Inexplicable", "Suspended", "Duplicate", "Perplexing", "Pendulum", "Advocate", "Provoke", "Variation", "Concentric", "Exterminate", "Terrain", "Vagabond", "Eccentric", "Inspiration", "Expectation", "Perspective", "Transpired", "Anxiety", "Eminent", "Sophomore", "Eloquent", "Equally", "Fallacy", "Courtesy", "Controversial", "Syllabus", "Cooperative", "Synthesis", "Coherent", "Collateral", "Synchronize", "Symptom", "Anemia", "Invalid", "Negligent", "Impersonate", "Anesthesia", "Nonchalant", "Immobilize", "Incognito", "Impatiently", "Anonymous", "Maximum", "Juvenile", "Opulent", "Quarantine", "Exhilarated", "Ordinance", "Exorbitant", "Molecular", "Regime", "Elastic", "Aeronautics", "Architect", "Theology", "Aristocrat", "Thermostat", "Parasite", "Enthusiasm", "Barometer", "Itemize", "Jubilant", "Compromise", "Permanent", "Capability", "Generosity", "Complimentary", "Infirmary", "Revolutionary", "Protege", "Opaque", "Resume", "Etiquette", "Fiancee", "Dilapidated", "Tyranny", "Disastrous", "Indigestion", "Perennial", "Iodine", "Larynx", "Administrator", "Manicurist", "Veterinarian", "Arbitrator", "Personification", "Allegory", "Simile", "Alliteration", "Pseudonym", "Exposition", "Stanza" };
			foreach (var w in words)
				x.Problems.Add(new Problem { ProblemText = w, Notes = "Db Init-er added", AddedAt = now, LkuLevel = Level0, BatchSource = "DbIni", LkuLanguage = l1 });

			//x.Problems.Add(new Problem { ProblemText = "1sec", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level1 });
			//x.Problems.Add(new Problem { ProblemText = "2sec", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level1 });
			//x.Problems.Add(new Problem { ProblemText = "3sec", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level1 });
			//x.Problems.Add(new Problem { ProblemText = "4sec", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level2 });
			//x.Problems.Add(new Problem { ProblemText = "5sec", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level2 });
			//var g1 = x.Problems.Add(new Problem { ProblemText = "SleAway", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level3 });
			//var g2 = x.Problems.Add(new Problem { ProblemText = "Kalimba", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level3 });
			//var g3 = x.Problems.Add(new Problem { ProblemText = "Maid wi", Notes = "Db Init-er added", AddedAt = DateTime.Now, LkuLevel = Level3 });

			//x.Auditions.Add(new Audition { Problem = g1, IsCorrect = false, DoneAt = DateTime.Now });
			//x.Auditions.Add(new Audition { Problem = g2, IsCorrect = false, DoneAt = DateTime.Now });

			x.SaveChanges();

			new System.Speech.Synthesis.SpeechSynthesizer().SpeakAsync("New database has been generated and initialized with sample data.");
		}
	}
}

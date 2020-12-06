using SpellingBee.DbIni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpellingBee
{
	/// <summary>
	/// Interaction logic for zBak.xaml
	/// </summary>
	public partial class zBak : Window
	{
		SpbDbContext_FOR_DB_INI_ONLY _db = new SpbDbContext_FOR_DB_INI_ONLY();

		public zBak()
		{
			InitializeComponent();

			//MyDbCreatorSame.setDbInitializer();

			//var dd = _db.Problems.Include("Auditions").Include("Player"); //tu: interesting...

			//var app = _db.Auditions.Include(p => p.Problem).Include(p => p.Player);
			////var ww = _db.Problems.Include(p => p.au).Include(p => p.);

			//var srt = from p in _db.Problems
			//					select new 
			//					{
			//						Word = p.ProblemText,
			//						Coun = p.

			//dataGrid2.DataContext = from w in _db.Problems//.Local
			//												select new
			//												{
			//													Word = w
			//													//,
			//													//Count = w.Auditions.Count()
			//												};

			//dataGrid2.DataContext = _db.Auditions.OrderBy(x => x.Problem.Count());
			//dataGrid2.DataContext = _db.Problems.OrderBy(x => x.Audition.Count());

			//dataGrid2.DataContext = _db.Problems.Local;
			dataGrid3.DataContext = _db.Auditions.Local;
		}
	}
}

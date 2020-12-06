using AAV.Sys.Helpers;
using System;
using System.Diagnostics;
using System.Windows;

namespace SpellingBee
{
  public partial class App : Application
	{
		public static DateTime Today = DateTime.Today;
		public static DateTime AppStartAt = DateTime.Now;
    public static TraceSwitch // copy for orgl in C:\C\Lgc\ScrSvrs\AAV.SS\App.xaml.cs
      AppTraceLevel_Config = new TraceSwitch("CfgTraceLevelSwitch", "Switch in config file:  <system.diagnostics><switches><!--0-off, 1-error, 2-warn, 3-info, 4-verbose. --><add name='CfgTraceLevelSwitch' value='3' /> "),
      AppTraceLevel_inCode = new TraceSwitch("Verbose________Trace", "This is the trace for all               messages.") { Level = TraceLevel.Verbose },
      AppTraceLevel_Warnng = new TraceSwitch("ErrorAndWarningTrace", "This is the trace for Error and Warning messages.") { Level = TraceLevel.Warning };
    
    public static string Dbfn => 
#if DEBUG
      OneDrive.Folder($@"Public\AppData\SpellBeeDb4.dbg.mdf");
#else
      OneDrive.Folder($@"Public\AppData\SpellBeeDb4.mdf");
#endif

    protected override void OnStartup(StartupEventArgs e)
		{
			//todo: Application.Current.DispatcherUnhandledException += OnCurrentDispatcherUnhandledException;
      Tracer.SetupTracingOptions("SpellingBee", AppTraceLevel_Warnng);

      base.OnStartup(e);      //CreateDbIfNotThere: //DBInitializer.SetDbInitializer();				//test: var _db = new MediaQADB();				_db.MediaInfos.Load();				foreach (var mi in _db.MediaInfos.Local) Console.WriteLine(mi); 

#if DEBUG__
      TTS_Console_Sample_1.xPoc.M1();
#endif

			new MainWindow().ShowDialog();

			Application.Current.Shutdown();
		}
	}
}
/*
Aug 2019:

--USE Master -- You don't want to be in the database you are trying to detach
--GO
--ALTER DATABASE SpellBeeDb4     SET SINGLE_USER WITH ROLLBACK IMMEDIATE -- Optional step to drop all active connections and roll back their work
--GO
--EXEC sp_detach_db 'SpellBeeDb4'
--GO

--USE [master]
--GO
--CREATE DATABASE [SpellBeeDb4] ON 
--( FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SpellBeeDb4.mdf' ),
--( FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SpellBeeDb4_log.ldf' )
-- FOR ATTACH
--GO
  
IF (NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'SpB')) 
BEGIN
    EXEC ('CREATE SCHEMA [SpB] AUTHORIZATION [dbo]')
END
go
ALTER SCHEMA SpB     TRANSFER dbo.Audition
ALTER SCHEMA SpB     TRANSFER dbo.LkuLanguage
ALTER SCHEMA SpB     TRANSFER dbo.LkuLevel
ALTER SCHEMA SpB     TRANSFER dbo.LkuSubject
ALTER SCHEMA SpB     TRANSFER dbo.Player
ALTER SCHEMA SpB     TRANSFER dbo.Problem
ALTER SCHEMA SpB     TRANSFER dbo.TombStone 
*/

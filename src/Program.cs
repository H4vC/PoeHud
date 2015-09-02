using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Hud;
using PoeHUD.Poe;
using System.IO;

using Tools;

namespace PoeHUD
{
	public class Program
	{
		private static int FindPoeProcess(out Offsets offs)
		{
			var clients = Process.GetProcessesByName(Offsets.Regular.ExeName).Select(p => Tuple.Create(p, Offsets.Regular)).ToList();
			clients.AddRange(Process.GetProcessesByName(Offsets.Steam.ExeName).Select(p => Tuple.Create(p, Offsets.Steam)));
			int ixChosen = clients.Count > 1 ? chooseSingleProcess(clients) : 0;
			if (clients.Count > 0 && ixChosen >= 0)
			{
				offs = clients[ixChosen].Item2;
				return clients[ixChosen].Item1.Id;
			}
		    offs = null;
		    return 0;
		}

	    private static int chooseSingleProcess(List<Tuple<Process, Offsets>> clients)
	    {
	        String o1 = $"Yes - process #{clients[0].Item1.Id}, started at {clients[0].Item1.StartTime.ToLongTimeString()}";
	        String o2 = $"No - process #{clients[1].Item1.Id}, started at {clients[1].Item1.StartTime.ToLongTimeString()}";
	        const string o3 = "Cancel - quit this application";
	        var answer = MessageBox.Show(null, String.Join(Environment.NewLine, o1, o2, o3),
	            "Choose a PoE instance to attach to", MessageBoxButtons.YesNoCancel);
	        return answer == DialogResult.Cancel ? -1 : answer == DialogResult.Yes ? 0 : 1;
	    }

	    [STAThread]
		public static void Main(string[] args)
		{
            AppDomain.CurrentDomain.UnhandledException += (sender, exceptionArgs) =>
            {
                var errorText = "Program exited with message:\n " + exceptionArgs.ExceptionObject;
                File.AppendAllText("Error.log", $"{DateTime.Now.ToString("g")} {errorText}\r\n{new string('-', 30)}\r\n");
                MessageBox.Show(errorText);
                Environment.Exit(1);
            };

#if !DEBUG
            MemoryControl.Start();
            if (Scrambler.Scramble(args.Length > 0 ? args[0] : null))
	        {
	            return;
	        }
#endif

            Offsets offs;
			int pid = FindPoeProcess(out offs);

			if (pid == 0)
			{
				MessageBox.Show("Path of Exile is not running!"); 
				return;
			}

			Sounds.LoadSounds();

			using (var memory = new Memory(offs, pid))
			{
				offs.DoPatternScans(memory);
				var gameController = new GameController(memory);
				gameController.RefreshState();

                var overlay = new ExternalOverlay(gameController, memory.IsInvalid);
                Application.Run(overlay);
			}
		}
	}
}

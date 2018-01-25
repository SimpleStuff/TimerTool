/*
 * Created by SharpDevelop.
 * User: Tebjan Halm
 * Date: 21.01.2014
 * Time: 16:55
 * 
 * 
 */
using System;
using System.Globalization;
using System.Windows.Forms;

namespace TimerTool
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
            bool useWindow = true;
            bool takeNext = false;
            uint timeoutFromConsole = 0;
            foreach(string arg in args)
            {
                if (arg == "-c")
                {
                    useWindow = false;
                }
                else if(arg == "-t")
                {
                    takeNext = true;
                } 
                else if(takeNext)
                {
                    takeNext = false;
                    double val;
                    if (double.TryParse(arg, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out val))
                    {
                        timeoutFromConsole = (uint)(val * 10000);
                    }
                }
            }

            if (useWindow)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(args));
            }
            else
            {
                if (timeoutFromConsole == 0)
                {
                    Console.WriteLine("resetting timer");
                    WinApiCalls.SetTimerResolution(0, false);
                }
                else
                {
                    Console.WriteLine("setting timer to {0}", timeoutFromConsole);
                    WinApiCalls.SetTimerResolution(timeoutFromConsole);
                }

                while (true)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
		}
	}
}

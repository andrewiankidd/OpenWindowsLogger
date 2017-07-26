using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWindowsLogger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int i = 0;
            foreach (string arg in args)
            {
              string command = arg.ToUpper();
              string value = (i+1) >= args.Length ? null : args[i + 1];
              switch (command)
              {     
                case "/T":
                  OWLmain.timeoutLimit = Convert.ToInt32(value) ;
                  break;
                case "/F":
                  OWLmain.logPath = value;
                  break;
                default:
                  break;
              }
              i++;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OWLmain());

            
        }
    }
}

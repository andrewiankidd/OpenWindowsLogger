using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace OpenWindowsLogger
{
  public partial class OWLmain : Form
  {
    public static bool logEnabled = false;
    public static string logPath = Environment.CurrentDirectory + "\\"+ DateTime.Now.ToString("yyyyMMddHHmmss") + "output.txt";
    StreamWriter logFilew = new StreamWriter(logPath);

    public OWLmain()
    {
      InitializeComponent();
    }

    private void OWLmain_Load(object sender, EventArgs e)
    {
      txtLogPath.Text = logPath;
      if (!File.Exists(logPath)) { File.CreateText(logPath); }
    }
    
    private void logWindows()
    {
      new Thread(() =>
      {
        Thread.CurrentThread.IsBackground = true;
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
          IntPtr handle = window.Key;
          string title = window.Value;
          rwlog("w", "[" + DateTime.Now + "]" + "/" + handle + "/" + title);
        }
        if (!logEnabled) { return; } else { logWindows(); }
      }).Start();
    }

    private void btnEnableLogging_Click(object sender, EventArgs e)
    {
      if (logEnabled)
      {
        logEnabled = false;
        btnEnableLogging.Text = "Logging Disabled";
        btnEnableLogging.ForeColor = Color.Red;
        rwlog("w", "Logging Disabled - " + DateTime.Now);
      }
      else if (!logEnabled)
      {
        logEnabled = true;
        btnEnableLogging.Text = "Logging Enabled";
        btnEnableLogging.ForeColor = Color.Green;
        rwlog("w", "Logging Enabled - " + DateTime.Now);
        logWindows();
      }
    }

    private void rwlog(string rw, string contents)
    {

      if (rw == "w")
      {
        logFilew.WriteLine(contents);
        logFilew.Flush();
      }

      if (rw == "r")
      {
        //scrapped feature, live log, will redo
      }
    }

   
  }
}

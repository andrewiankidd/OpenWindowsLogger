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
    public static string logPath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "output.txt";
    public static logger log = new logger();

    public OWLmain()
    {
      InitializeComponent();
    }

    private void OWLmain_Load(object sender, EventArgs e)
    {
      //set logpath and create log file if it doesnt exist
      txtLogPath.Text = logPath;
      if (!File.Exists(logPath)) { File.CreateText(logPath); }
      
      //start a thread for watching mouseclicks
      new Thread(() =>
      {
        mouseListener.mouseListenerMain();
      }).Start();

    }

    private void logWindows()
    {
      //Thread will log open windows
      new Thread(() =>
      {
        Thread.CurrentThread.IsBackground = true;
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
          IntPtr handle = window.Key;
          string title = window.Value;
          log.rwlog("w", "[" + DateTime.Now + "]" + "/" + handle + "/" + title);
        }

        if (!logEnabled) { return; } else { logWindows(); }
      }).Start();

    }

    private void btnEnableLogging_Click(object sender, EventArgs e)
    {
      //if logging is already enabled then disable
      if (logEnabled)
      {
        logEnabled = false;
        btnEnableLogging.Text = "Logging Disabled";
        btnEnableLogging.ForeColor = Color.Red;
        log.rwlog("w", "Logging Disabled - " + DateTime.Now);
      }
      //otherwise, enable
      else if (!logEnabled)
      {
        logEnabled = true;
        btnEnableLogging.Text = "Logging Enabled";
        btnEnableLogging.ForeColor = Color.Green;
        log.rwlog("w", "Logging Enabled - " + DateTime.Now);
        logWindows();
      }
    }

    private void OWLmain_FormClosed(object sender, FormClosedEventArgs e)
    {
      //when the form closes stop all threads and exit app
      Application.Exit();
    }
  }
}

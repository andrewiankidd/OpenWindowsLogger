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
using System.Globalization;
using System.Timers;

namespace OpenWindowsLogger
{
  public partial class OWLmain : Form
  {
    public static bool logEnabled = false;
    public static List<string> existingWindows = new List<string>();
    public static List<string> filteredWindows = new List<string>();
    public static int timeoutLimit = 0;
    public static string logPath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "output.txt";
    public static logger log;

    public OWLmain()
    {
      InitializeComponent();
      log = new logger();
  }

    private void OWLmain_Load(object sender, EventArgs e)
    {
      //set logpath and create log file if it doesnt exist
      txtLogPath.Text = logPath;
      if (!File.Exists(logPath)) { File.CreateText(logPath); }

      //check for filter list and read into filteredWindows list
      if (File.Exists(Environment.CurrentDirectory + "\\filter.txt"))
      {
         filteredWindows = new List<string>(File.ReadAllLines(Environment.CurrentDirectory + "\\filter.txt"));
      }

      //start a thread for watching mouseclicks
      new Thread(() =>
      {
        mouseListener.mouseListenerMain();
      }).Start();

      if (timeoutLimit > 0)
      {
        System.Timers.Timer timer = new System.Timers.Timer(timeoutLimit * 1000);
        timer.Elapsed += Timer_Elapsed;
        log.rwlog("w", "[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "]" + " Logging for " + timeoutLimit + " seconds.");
        toggleLogging();
        timer.Start();
      }

    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      log.rwlog("w", "[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "]" + " Timer up!");
      Application.Exit();
    }

    private void logWindows()
    {
      //Thread will log open windows
      new Thread(() =>
      {
        //set as background thread
        Thread.CurrentThread.IsBackground = true;
        //run OpenWindowGetter and iterate through results
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
          IntPtr handle = window.Key;
          string title = window.Value;
          string windowKey = "/" + handle + "/" + title;

          //if user has chosen to filter the existing windows, check current iteration result against list of known windows to ignore
          if (chkFilterExisting.Checked && existingWindows.Contains(windowKey))
          {
            //it's on the existing window list, so ignore it
          }
          else if (chkExtFilterList.Checked && filteredWindows.Contains(title) || chkExtFilterList.Checked && filteredWindows.Contains(windowKey))
          {
            //it's on the filter list, so ignore it
          }
          //it's not on the filter list, so print it to log
          else {
            
            //if we wanna suppress dupes then add this entry to the filter before printing it out, it'll be ignored next time
            if (chkSuppressDupes.Checked)
            {
              filteredWindows.Add(windowKey);
            }

            log.rwlog("w", "[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "]" + windowKey);
          }
        }
        //check if logging is still enabled, if not then close off, if so then run logger again
        if (!logEnabled) { return; } else { logWindows(); }
      }).Start();

    }

    private void getExistingWindows()
    {
      //Thread will log open windows
      new Thread(() =>
      {
        //set as background thread
        Thread.CurrentThread.IsBackground = true;
        //run OpenWindowGetter and iterate through results
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
          IntPtr handle = window.Key;
          string title = window.Value;
          string windowKey = "/" + handle + "/" + title;
          //add window key and title to list of existing windows
          existingWindows.Add(windowKey);
        }
      }).Start();

    }

    private void toggleLogging()
    {
      //if user wants to filter existing windows, run getExistingWindows, which will create list of running windows we can compare against later
      if (chkFilterExisting.Checked) { getExistingWindows(); }

      //if logging is already enabled then disable
      if (logEnabled)
      {
        logEnabled = false;
        btnEnableLogging.Text = "Logging Disabled";
        btnEnableLogging.ForeColor = Color.Red;
        //write to log that logging has been disabled, and the datetime it was disabled
        log.rwlog("w", "[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "]" + btnEnableLogging.Text);
        //if user has chosen to automatically open the log file, open it in default text editor now
        if (chkAutoOpenLog.Checked) { System.Diagnostics.Process.Start(logPath); }
      }
      //otherwise, enable
      else if (!logEnabled)
      {
        logEnabled = true;
        btnEnableLogging.Text = "Logging Enabled";
        btnEnableLogging.ForeColor = Color.Green;
        //write to log that logging has been enabled, and the datetime it was enabled
        log.rwlog("w", "[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "]" + btnEnableLogging.Text);
        logWindows();
      }
    }

    private void btnEnableLogging_Click(object sender, EventArgs e)
    {
      toggleLogging();
    }

    private void OWLmain_FormClosed(object sender, FormClosedEventArgs e)
    {
      //when the form closes stop all threads and exit app
      Application.Exit();
    }

  }
}

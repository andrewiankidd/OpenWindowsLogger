﻿using System;
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
    public static bool filterExisting = true;
    public static List<string> existingWindows = new List<string>();
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
        //set as background thread
        Thread.CurrentThread.IsBackground = true;
        //run OpenWindowGetter and iterate through results
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
          IntPtr handle = window.Key;
          string title = window.Value;
          //if user has chosen to filter the existing windows, check current iteration result against list of known windows
          if (filterExisting && existingWindows.Contains("/" + handle + "/" + title))
          {
            //it's on the filter list, so ignore it
          }
          //it's not on the filter list, so print it to log
          else { log.rwlog("w", "[" + DateTime.Now + "]" + "/" + handle + "/" + title); }
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
          //add window key and title to list of existing windows
          existingWindows.Add("/" + handle + "/" + title);
        }
      }).Start();

    }

    private void btnEnableLogging_Click(object sender, EventArgs e)
    {
      //if user wants to filter existing windows, run getExistingWindows, which will create list of running windows we can compare against later
      if (filterExisting) { getExistingWindows(); }
      
      //if logging is already enabled then disable
      if (logEnabled)
      {
        logEnabled = false;
        btnEnableLogging.Text = "Logging Disabled";
        btnEnableLogging.ForeColor = Color.Red;
        //write to log that logging has been disabled, and the datetime it was disabled
        log.rwlog("w", "Logging Disabled - " + DateTime.Now);
      }
      //otherwise, enable
      else if (!logEnabled)
      {
        logEnabled = true;
        btnEnableLogging.Text = "Logging Enabled";
        btnEnableLogging.ForeColor = Color.Green;
        //write to log that logging has been enabled, and the datetime it was enabled
        log.rwlog("w", "Logging Enabled - " + DateTime.Now);
        logWindows();
      }
    }

    private void OWLmain_FormClosed(object sender, FormClosedEventArgs e)
    {
      //when the form closes stop all threads and exit app
      Application.Exit();
    }

    private void chkFilterExisting_CheckedChanged(object sender, EventArgs e)
    {
      if (chkFilterExisting.Checked)
      { filterExisting = true; }
      else
      { filterExisting = false; }
    }
  }
}

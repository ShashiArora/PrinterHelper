// Decompiled with JetBrains decompiler
// Type: PrinterHelper.frmMain
// Assembly: PrinterHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DDC417CF-4400-4A62-855A-45727A385E86
// Assembly location: C:\AllProject\PRINTERhELPER\PrinterHelper.exe

using Microsoft.Win32;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PrinterHelper
{
  public class frmMain : Form
  {
    private NancyHost host;
    private IContainer components;
    private CheckBox chkRunAtStartup;
    private NotifyIcon PHnotifyIcon;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem exitToolStripMenuItem;
    private Label label1;

    public frmMain() => this.InitializeComponent();

    private void frmMain_Load(object sender, EventArgs e)
    {
      try
      {
        this.host = new NancyHost(new Uri("http://localhost:9876"), (INancyBootstrapper) new PrinterHelper.Bootstrapper(), new HostConfiguration()
        {
          RewriteLocalhost = false
        });
        this.host.Start();
      }
      catch (Exception ex)
      {
      }
      if (this.WindowState != FormWindowState.Normal)
        return;
      this.WindowState = FormWindowState.Minimized;
      this.ShowInTaskbar = false;
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        this.host.Stop();
      }
      catch (Exception ex)
      {
      }
    }

    private void frmMain_Resize(object sender, EventArgs e)
    {
      if (FormWindowState.Minimized == this.WindowState)
      {
        this.PHnotifyIcon.Visible = true;
        this.PHnotifyIcon.ShowBalloonTip(500);
        this.Hide();
      }
      else
      {
        if (this.WindowState != FormWindowState.Normal)
          return;
        this.PHnotifyIcon.Visible = false;
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

    private void chkRunAtStartup_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        string name = "PrinterHelper";
        if (this.chkRunAtStartup.Checked)
          registryKey.SetValue(name, (object) Application.ExecutablePath);
        else
          registryKey.DeleteValue(name, false);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    private void PHnotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.WindowState != FormWindowState.Minimized)
        return;
      this.WindowState = FormWindowState.Normal;
      this.ShowInTaskbar = true;
      this.Show();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmMain));
      this.chkRunAtStartup = new CheckBox();
      this.PHnotifyIcon = new NotifyIcon(this.components);
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.label1 = new Label();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.chkRunAtStartup.AutoSize = true;
      this.chkRunAtStartup.Location = new Point(12, 12);
      this.chkRunAtStartup.Name = "chkRunAtStartup";
      this.chkRunAtStartup.Size = new Size(110, 20);
      this.chkRunAtStartup.TabIndex = 0;
      this.chkRunAtStartup.Text = "Run at startup";
      this.chkRunAtStartup.UseVisualStyleBackColor = true;
      this.chkRunAtStartup.CheckedChanged += new EventHandler(this.chkRunAtStartup_CheckedChanged);
      this.PHnotifyIcon.BalloonTipText = "Printer helper is running";
      this.PHnotifyIcon.BalloonTipTitle = "Printer Helper";
      this.PHnotifyIcon.ContextMenuStrip = this.contextMenuStrip1;
      this.PHnotifyIcon.Icon = (Icon) componentResourceManager.GetObject("PHnotifyIcon.Icon");
      this.PHnotifyIcon.Text = "PrinterHelper";
      this.PHnotifyIcon.Visible = true;
      this.PHnotifyIcon.MouseDoubleClick += new MouseEventHandler(this.PHnotifyIcon_MouseDoubleClick);
      this.contextMenuStrip1.ImageScalingSize = new Size(20, 20);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.exitToolStripMenuItem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(103, 28);
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new Size(102, 24);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(81, 62);
      this.label1.Name = "label1";
      this.label1.Size = new Size(319, 29);
      this.label1.TabIndex = 2;
      this.label1.Text = "Program to host print service";
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(475, 185);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.chkRunAtStartup);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (frmMain);
      this.Text = "Printer Helper";
      this.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
      this.Load += new EventHandler(this.frmMain_Load);
      this.Resize += new EventHandler(this.frmMain_Resize);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}

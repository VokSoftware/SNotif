using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Configuration;

namespace SNotif
{
    public partial class Form1 : Form
    {
        IntPtr hIcon;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                fUpdateInfo();
                Hide();
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;

                //Zakazani povoleni Quit
                if (ConfigurationManager.AppSettings["AllowQuit"]=="0")
                {
                    quitToolStripMenuItem.Visible = false;
                }

                notifyIcon1.ShowBalloonTip(1000);
                timer1.Enabled = true;
            }
            catch(Exception ex)
            {

            }
        }

        private void fUpdateInfo()
        {
            try
            {
                String hostName = string.Empty;
                string sDum = "";
                hostName = Dns.GetHostName();
                lblComputer.Text = hostName.ToString();

                IPHostEntry myIP = Dns.GetHostEntry(hostName);
                IPAddress[] address = myIP.AddressList;

                for (int i = 0; i < address.Length; i++)
                {
                    sDum = address[i].ToString();
                    if (sDum.Contains(ConfigurationManager.AppSettings["IPMask"]) == true)
                    {
                        lblIp.Text = sDum;
                    }
                }
                notifyIcon1.Text = "Computer Name: " + lblComputer.Text + Environment.NewLine + "Ip Address: " + lblIp.Text;
                notifyIcon1.BalloonTipText = "Computer Name: " + lblComputer.Text + Environment.NewLine + "Ip Address: " + lblIp.Text;
                sDum = lblIp.Text;
                sDum = sDum.Substring(sDum.Length - 3);
                sDum.Replace(".", "");
                CreateTextIcon(sDum);
            }
            catch(Exception ex)
            {

            }
        }

        public void CreateTextIcon(string str)
        {
            try
            {
                Font fontToUse = new Font(ConfigurationManager.AppSettings["IconFont"], Convert.ToInt16(ConfigurationManager.AppSettings["IconFontSize"]), FontStyle.Regular, GraphicsUnit.Pixel);
                Brush brushToUse = new SolidBrush(Color.FromName(ConfigurationManager.AppSettings["IconColor"].ToString()));
                Bitmap bitmapText = new Bitmap(16, 16);
                Graphics g = System.Drawing.Graphics.FromImage(bitmapText);
                IntPtr hIcon;

                g.Clear(Color.Transparent);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                g.DrawString(str, fontToUse, brushToUse, -4, -2);
                hIcon = (bitmapText.GetHicon());
                notifyIcon1.Icon = System.Drawing.Icon.FromHandle(hIcon);
            }
            catch(Exception ex)
            {

            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Hide();
                    notifyIcon1.Visible = true;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            { 
            }
            catch(Exception ex)
            {
            }
        }

        private void notifyIcon1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                fUpdateInfo();
            }
            catch(Exception ex)
            {

            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
            catch(Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Hide();
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;
            }
            catch(Exception ex)
            {

            }
        }

        private void remoteSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Spusteni aplikace
                System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["RSRun"]);
            }
            catch(Exception)
            {

            }
        }
    }
}

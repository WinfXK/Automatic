using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Automatic
{
    public partial class Automatic : Form
    {
        public Automatic()
        {
            InitializeComponent();
        }
        private void startCommand(String s)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(s);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            if (s == null || s.Length <= 0)
            {
                MessageBox.Show("请输入想要设置的倒计时时长！");
                return;
            }
            if (!VldInt(s))
            {
                MessageBox.Show("倒计时仅支持纯数字！");
                return;
            }
            double i = 0;
            double.TryParse(s, out i);
            if (i <= 0)
            {
                MessageBox.Show("倒计时长必须大于0！");
                return;
            }
            startCommand("shutdown -s -t "+ (i*3600));
            DialogResult dr1 = MessageBox.Show("已设置在"+s+"时("+(i*3600)+"秒)后关机！\n需要为您锁定计算机吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dr1 == DialogResult.Yes)
            {
                startCommand("rundll32.exe user32.dll,LockWorkStation");
            }
            Process.GetCurrentProcess().Kill();
        }
        private bool VldInt(string num)
        {
            int ResultNum;
            return int.TryParse(num, out ResultNum);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            startCommand("shutdown -a");
            DialogResult dr1 = MessageBox.Show("已取消自动关机！\n退出程序吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dr1 == DialogResult.Yes)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

        }
    }
}

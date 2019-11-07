using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBK_UTF8_Char
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //汉字转为Unicode编码：
            string hz = textBox1.Text.ToString();
            byte[] b = Encoding.Unicode.GetBytes(hz);
            string o = "";
            foreach (var x in b)
            {
                o += string.Format("{0:X2}", x) + " ";
            }
            textBox2.Text = o;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //Unicode编码转为汉字：
            string cd = textBox2.Text.ToString();
            string cd2 = cd.Replace(" ", "");
            cd2 = cd2.Replace("\r", "");
            cd2 = cd2.Replace("\n", "");
            cd2 = cd2.Replace("\r\n", "");
            cd2 = cd2.Replace("\t", "");
            if (cd2.Length % 4 != 0)
            {
                MessageBox.Show("Unicode编码为双字节，请删多或补少！确保是二的倍数。");
            }
            else
            {
                int len = cd2.Length / 2;
                byte[] b = new byte[len];
                for (int i = 0; i < cd2.Length; i += 2)
                {
                    string bi = cd2.Substring(i, 2);
                    b[i / 2] = (byte)Convert.ToInt32(bi, 16);
                }
                string o = Encoding.Unicode.GetString(b);
                textBox1.Text = o;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //汉字转成GBK十六进制码：
            string hz = textBox3.Text.ToString();
            byte[] gbk = Encoding.GetEncoding("GBK").GetBytes(hz);
            string sGbk = ""; string s1d = "";
            foreach (byte b in gbk)
            {
                //s1 += Convert.ToString(b, 16)+" ";
                sGbk += string.Format("{0:X2}", b) + " ";
                s1d += b + " ";
                toolTip1.SetToolTip(textBox4, s1d);
            }
            textBox4.Text = sGbk;
            toolTip1.SetToolTip(textBox4, s1d);
            //汉字转成Unicode十六进制码：
            byte[] uc = Encoding.Unicode.GetBytes(hz);
            string s2 = ""; string s2d = "";
            foreach (byte b in uc)
            {
                //s2 += Convert.ToString(b, 16) + " ";
                s2 += string.Format("{0:X2}", b) + " ";
                s2d += b + " ";
                toolTip1.SetToolTip(textBox5, s2d);
            }
            textBox5.Text = s2;
            toolTip1.SetToolTip(textBox5, s2d);
            //汉字转成UTF-8十六进制码：
            byte[] utf8 = Encoding.UTF8.GetBytes(hz);
            string s3 = ""; string s3d = "";
            foreach (byte b in utf8)
            {
                //s3 += Convert.ToString(b, 16) + " ";
                s3 += string.Format("{0:X2}", b) + " ";
                s3d += b + " ";
                toolTip1.SetToolTip(textBox6, s3d);
            }
            textBox6.Text = s3;
            toolTip1.SetToolTip(textBox6, s3d);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //GBK十六进制码转成汉字：
            textBox3.Text = "";

            string cd = textBox4.Text.ToString().Trim();
            cd = cd.Replace("0x", "");
            string[] b4 = cd.Split(' ');
            if(b4.Length==1)
            {
               cd = cd.Replace("\r","");
                b4 = cd.Split('\n');
            }

            byte[] bys = new byte[b4.Length];
            int i = 0;
            foreach (var item in b4)
            {
                if (!Byte.TryParse(item, System.Globalization.NumberStyles.HexNumber, null, out bys[i++]))
                {
                    textBox3.Text += "字符[" + item + "]无法转换！";
                }
            }
            textBox3.Text += Encoding.GetEncoding("GBK").GetString(bys);


            //while (p<b4.Length-1)
            //{
            //    byte[] bs = new byte[3];
            //    p++;
            //    if (!Byte.TryParse( b4[p], System.Globalization.NumberStyles.HexNumber, null, out bs[0])) continue;
            //    if (bs[0]<0xA1)
            //    {
            //        textBox3.Text += Encoding.GetEncoding("GBK").GetString(bs);
            //    }
            //    else
            //    {
            //        p++;
            //        if (!Byte.TryParse(b4[p], System.Globalization.NumberStyles.HexNumber, null, out bs[1])) continue;
            //        textBox3.Text += Encoding.GetEncoding("GBK").GetString(bs);
            //    }
            //}

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            //Unicode十六进制码转成汉字：
            string cd = textBox5.Text.ToString();
            string[] b5 = cd.Split(' ');
            byte[] bs = new byte[2];
            bs[0] = (byte)Convert.ToByte(b5[0], 16);
            bs[1] = (byte)Convert.ToByte(b5[1], 16);
            textBox3.Text = Encoding.GetEncoding("Unicode").GetString(bs);

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            //UTF-8十六进制码转成汉字：
            string cd = textBox6.Text.ToString();
            string[] b6 = cd.Split(' ');
            byte[] bs = new byte[b6.Length];
            int i = 0;
            foreach (var item in b6)
            {
                if (!Byte.TryParse(item, System.Globalization.NumberStyles.HexNumber, null, out bs[i++])) continue;
            }

            textBox3.Text = Encoding.GetEncoding("UTF-8").GetString(bs);

        }
        int add(int a,int b)
        {
            return a + b;
        }

        private void button7_Click(object sender, EventArgs e)
        {
           textBox4.Text= Clipboard.GetText();
           Button4_Click(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            textTime.Text = epoch.ToString();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            tStamp.Text = Clipboard.GetText();
            Button1_Click(sender, e);

        }

        private void tStamp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tToTime.Text = GetTime(tStamp.Text).ToString();

            }
            catch
            { }
        }
        /// <summary>  
        /// Unix时间戳转为C#格式时间  
        /// </summary>  
        /// <param name="timeStamp">Unix时间戳格式,例如1482115779</param>  
        /// <returns>C#格式时间</returns>  
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }


        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static long ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds;
        }

        private void tTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tToStamp.Text = ConvertDateTimeInt(Convert.ToDateTime(tTime.Text)).ToString();

            }
            catch
            { }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int page =Properties.Settings.Default.SelectTab;
            tabControl1.SelectTab(page);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SelectTab = tabControl1.SelectedIndex;
            Properties.Settings.Default.Save();
        }
    }
}

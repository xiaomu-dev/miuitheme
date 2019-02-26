using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace 小米主题下载器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //默认填写的主题链接
            this.dz1.Text = "http://zhuti.xiaomi.com/detail/18528ddf-90b0-40cb-b538-71c3768d4f29";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //调用系统默认的浏览器 
            System.Diagnostics.Process.Start("http://wpa.qq.com/msgrd?v=3&uin=451508532&site=qq&menu=yes");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://zhuti.xiaomi.com/detail/18528ddf-90b0-40cb-b538-71c3768d4f29");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取主题链接
            string dz1 = this.dz1.Text;
            //定义新旧字符串
            string oldtext = "http://zhuti.xiaomi.com/detail/";
            string newtext = "http://thm.market.xiaomi.com/thm/download/v2/";
            //修改主题链接
            string dz = string.Empty;
            dz = dz1.Replace(oldtext,newtext);
            //指定请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dz);
            //得到返回
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //得到流
            Stream recStream = response.GetResponseStream();
            //编码方式
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            //指定转换为gb2312编码
            StreamReader sr = new StreamReader(recStream, gb2312);
            //以字符串方式得到网页内容
            String content = sr.ReadToEnd();
            //将网页内容进行过滤
            JObject obj = Newtonsoft.Json.Linq.JObject.Parse(content);
            //判断是否获取到正确下载地址
            if (obj["apiCode"].ToString() == "0")
            {
                string dz2 = obj["apiData"]["downloadUrl"].ToString();
                this.dz2.Text = dz2;
            }
            else
            {
                this.dz2.Text = "请检查网络是否连接或者主题链接是否正确";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
            //判断地址解析是否正确
            if (this.dz2.Text == "请检查网络是否连接或者主题链接是否正确")
            {
                MessageBox.Show("无法解析到正确的地址,不能直接下载,请检查网络或者输入是否有误");
            }
            else
            {
                System.Diagnostics.Process.Start(this.dz2.Text);
            }
        }
    }
}

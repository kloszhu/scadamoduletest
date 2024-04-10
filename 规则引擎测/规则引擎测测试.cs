using NVelocity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 规则引擎测
{
    public partial class 规则引擎测测试 : Form
    {
        private int runcount = 0;
        private List<string> arrayString;
        private List<string> eventString;

        public 规则引擎测测试()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            arrayString= textBianLiang.Text.Trim().Split(',').ToList();
            eventString = textShiJian.Text.Trim().Split(',').ToList();
            timer1.Interval = Convert.ToInt32(textZhouQi.Text);
            timer1.Start();
            label10.Text = "启动";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label10.Text = "停止";
            timer1.Stop();
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            CoreDao();
        }
        List<Dictionary<string, object>> keys = new List<Dictionary<string, object>>();
        private void CoreDao() {
            Stopwatch swatch = new Stopwatch();
            swatch.Start(); //计时开始
            keys.Clear();
            for (int i = 0; i < Convert.ToInt32( textBiShu.Text); i++)
            {
                Dictionary<string, object> key = new Dictionary<string, object>();
                for (int j = 0; j < arrayString.Count; j++)
                {
                    if (!key.ContainsKey(arrayString[j]))
                    { 
                        Random random = new Random();
                        key.Add(arrayString[j], random.NextDouble());
                    }
                }
                for (int k = 0; k < eventString.Count; k++)
                {
                    if (!key.ContainsKey(eventString[k]))
                    {
                        key.Add(eventString[k], "");
                    }
                }
                NVelocityHelper nVelocity = new NVelocityHelper();
                nVelocity.Put(key);
               var p=  nVelocity.GetResult( i.ToString(),textGuiZe.Text);       
                keys.Add(key);
            }
            WriteFile(runcount.ToString() + ".output.json",Newtonsoft.Json.JsonConvert.SerializeObject(keys));



           
            outputTime.Text = swatch.ElapsedMilliseconds.ToString(); //获取代码段执行时间
            WriteFile( runcount.ToString() + ".duration.txt", outputTime.Text);
            swatch.Stop();///计时结束
            swatch.Reset(); //第二次计时时进行重置
            runcount++;
            label12.Text = runcount.ToString();

        }

        public async void WriteFile(string path,string content) {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"data")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"));
            }
            string paths = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", path);
            using (StreamWriter stream = new StreamWriter(paths, true, Encoding.UTF8)) {
               await stream.WriteAsync(content);
               await stream.FlushAsync();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data")))
            {
                Directory.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"), true);
            }
        }
    }
}

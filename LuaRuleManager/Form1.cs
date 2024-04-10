using LuaRuleManager.Mode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuaRuleManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            builder =new StringBuilder( File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ScriptTemp", "Script1.txt")));
        }
        TreeNode Root = new TreeNode("根节点");
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1000";
            timer1.Enabled = false;
            label2.Text = "未启动";
            treeView1.Nodes.Add(Root);
            dataGridView1.DataSource = Scripts.Instance.GetData();

        }

        bool switchbtn = false;
        int num = 0;
        void buttonSwitch()
        {
            num = 0;
            if (switchbtn == true)
            {
                timer1.Interval = Convert.ToInt32(textBox1.Text);
                timer1.Start();
                button1.Text = "关闭模拟";
                switchbtn = false;
            }
            else
            {
                button1.Text = "启动模拟";
                timer1.Interval = Convert.ToInt32(textBox1.Text);
                timer1.Stop();
                switchbtn = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            buttonSwitch();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = $"{num}次";
            num++;
            Random random = new Random();

            Scripts.Instance.ReplaceData(Keys.Select(a => new EntityModel { Node = a.NodeName, RandomValue = random.NextDouble() }).ToList());
            dataGridView1.DataSource = Scripts.Instance.GetData();
            if (!string.IsNullOrEmpty(richTextBox1.Text)&&!richTextBox1.Text.StartsWith("error"))
            {
                try
                {
                    Scripts.Instance.Excute(richTextBox1.Text);
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = "error" + ex.Message+"\n"+richTextBox1.Text;
                    
                }
            }

        }
        List<UIMode> Keys = new List<UIMode>();
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("节点名为空");
                return;
            }

            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请选择树节点");
                return;
            }
            if (Keys.Exists(a => a.NodeName == textBox2.Text))
            {
                MessageBox.Show("不能添加同名");
                return;
            }
            var node = new TreeNode(textBox2.Text);
            Keys.Add(new UIMode { NodeName = textBox2.Text, Node = node, Value = 0 });

            treeView1.SelectedNode.Nodes.Add(node);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Root = new TreeNode("根节点");
            for (int i = 1; i <= 10; i++)
            {
                var node = new TreeNode($"a{i}");
                Root.Nodes.Add(node);
                Keys.Add(new UIMode { NodeName = $"a{i}", Node = node, Value = 0 });
            }
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(Root);
        }
        StringBuilder builder;
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = builder.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double dd = 150.000d;
            double ee = 150.0d;
            MessageBox.Show((dd == ee).ToString());
        }
    }
}

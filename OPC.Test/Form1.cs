using Newtonsoft.Json;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPC.Test
{

    public partial class Form1 : Form
    {
    

        EasyDAClient client ;
        IContainer container;
       
        public Form1()
        {
            client = new EasyDAClient();

            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            
        }
        OpcLabs.EasyOpc.ServerElement serverelemnt1;
        OpcLabs.EasyOpc.ServerElement serverelemnt2;
        /// <summary>
        /// 读取OPC服务地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            OpcLabs.EasyOpc.ServerElementCollection list = EasyDAClient.SharedInstance.BrowseServers("localhost", OpcLabs.EasyOpc.OpcTechnologies.All);
            foreach (ServerElement item in list)
            {

                textBox1.Text +="OPC服务地址："+ System.Net.WebUtility.UrlDecode(item)+ "\n";
                textBox1.Text += String.Format($"\t ProgId:{item.ProgId}\t\n Location:{item.Location}\t\nDescription:{item.Description}\t\n Clsid:{item.Clsid}\t\n ServerClass:{item.ServerClass}\t\n Technology:{item.Technology}\t\n Vendor:{item.Vendor}\t\n");
            }
            serverelemnt1 = list[0];
            serverelemnt2 = list[1];
            //var array = new OpcLabs.EasyOpc.DataAccess.OperationModel.DAItemVtqArguments[] {
            //  new OpcLabs.EasyOpc.DataAccess.OperationModel.DAItemVtqArguments{   ,ItemDescriptor=new DAItemDescriptor{ AccessPath="f.c.c", ItemId="f.c.c" } },
            //  new OpcLabs.EasyOpc.DataAccess.OperationModel.DAItemVtqArguments{ ItemDescriptor=new DAItemDescriptor{ AccessPath="f.c.a", ItemId="f.c.a" } },
            //};
            //EasyDAClient.SharedInstance.WriteMultipleItems(array);
        }
       /// <summary>
       /// 写入数据
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
     
            string value = "zhoujielundaociyiyou";
            //int handle = client.SubscribeItem("", serverelemnt1.ProgId, "a.a.", 100);
            //int handle = client.("", serverelemnt1.ProgId, "Simulation.Random", 100);
            //client.WriteItem("", serverelemnt1.ProgId, "f", "zhoujielundaociyiyou",DAVtq.DefaultTimestamp,DAVtq.DefaultQuality, VarTypes.BStr);
            //client.WriteItem("", serverelemnt1.ProgId, "f.a.g",Convert.ToDouble(0.01), DateTime.UtcNow, DAVtq.DefaultQuality, VarTypes.R8);
            client.WriteItemValue("", serverelemnt1.ProgId, "a.a.g", Convert.ToDouble(0.01),VarTypes.R8);
            var q = client.ReadItemValue("", serverelemnt1.ProgId, "a.a.g",VarTypes.R8,100);
            if (value.Equals(q))
            {
                textBox1.Text += $"a.a.g:0.01 ->{q},类型和值完全相同\n\t";
            }
            else
            {
                textBox1.Text += $"a.a.g:0.01->{q},类型和值不相同\n\t";
            }

        }
        /// <summary>
        /// 读取a.a.a 节点值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            int i=0, j = 0;
            i++; j++;
            
            var p = client.ReadItemValue("", serverelemnt1.ProgId, "a.a.a",VarTypes.Bool, 100);
            textBox1.Text += String.Format("\na.a.a 节点值\t\n" + p);
            var q = client.ReadItemValue("", serverelemnt1.ProgId, "a.a.g",VarTypes.R8, 100);
            textBox1.Text += String.Format("\na.a.q 节点值\t\n" + q);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OptNetApiTest frm = new OptNetApiTest();
            frm.Show();
        }
    }
}

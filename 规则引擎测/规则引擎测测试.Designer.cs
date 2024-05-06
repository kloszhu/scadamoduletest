namespace 规则引擎测
{
    partial class 规则引擎测测试
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textBianLiang = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textShiJian = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textGuiZe = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBiShu = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textZhouQi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.outputTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "变量";
            // 
            // textBianLiang
            // 
            this.textBianLiang.Location = new System.Drawing.Point(78, 12);
            this.textBianLiang.Multiline = true;
            this.textBianLiang.Name = "textBianLiang";
            this.textBianLiang.Size = new System.Drawing.Size(904, 73);
            this.textBianLiang.TabIndex = 1;
            this.textBianLiang.Text = "a1,a2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "事件";
            // 
            // textShiJian
            // 
            this.textShiJian.Location = new System.Drawing.Point(78, 172);
            this.textShiJian.Multiline = true;
            this.textShiJian.Name = "textShiJian";
            this.textShiJian.Size = new System.Drawing.Size(904, 71);
            this.textShiJian.TabIndex = 3;
            this.textShiJian.Text = "t1,t2";
            this.textShiJian.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(3, 647);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1322, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "说明：变量和事件，都和隔开代表定义Tag.经过规则引擎处理后生成新的结果，可查看bin/input bin/output 文件夹查看。生成规则将按照变量随机生成f" +
    "loat数据。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "规则引擎：";
            // 
            // textGuiZe
            // 
            this.textGuiZe.Location = new System.Drawing.Point(125, 382);
            this.textGuiZe.Multiline = true;
            this.textGuiZe.Name = "textGuiZe";
            this.textGuiZe.Size = new System.Drawing.Size(824, 125);
            this.textGuiZe.TabIndex = 6;
            this.textGuiZe.Text = "#if(${a1} > 0.01f )\r\n#set($t1=0)\r\n#end";
            this.textGuiZe.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBiShu);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textZhouQi);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(1005, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 123);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据生成器";
            // 
            // textBiShu
            // 
            this.textBiShu.Location = new System.Drawing.Point(111, 68);
            this.textBiShu.Name = "textBiShu";
            this.textBiShu.Size = new System.Drawing.Size(405, 28);
            this.textBiShu.TabIndex = 3;
            this.textBiShu.Text = "1000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "笔数";
            // 
            // textZhouQi
            // 
            this.textZhouQi.Location = new System.Drawing.Point(111, 21);
            this.textZhouQi.Name = "textZhouQi";
            this.textZhouQi.Size = new System.Drawing.Size(405, 28);
            this.textZhouQi.TabIndex = 1;
            this.textZhouQi.Text = "100";
            this.textZhouQi.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-3, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "周期（毫秒）";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(1005, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(522, 143);
            this.button1.TabIndex = 8;
            this.button1.Text = "运行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.SaddleBrown;
            this.button2.Location = new System.Drawing.Point(1005, 353);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(522, 57);
            this.button2.TabIndex = 9;
            this.button2.Text = "停止";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 582);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "可用作执行脚本";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 549);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(773, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "nvelocity规则引擎测试，测试结果：仅能处理字符串，对逻辑代码中产生的值，无法传回原对象";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(1000, 521);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(313, 30);
            this.label9.TabIndex = 12;
            this.label9.Text = "每秒执行时间(毫秒)：";
            // 
            // outputTime
            // 
            this.outputTime.AutoSize = true;
            this.outputTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outputTime.Location = new System.Drawing.Point(1334, 526);
            this.outputTime.Name = "outputTime";
            this.outputTime.Size = new System.Drawing.Size(34, 24);
            this.outputTime.TabIndex = 13;
            this.outputTime.Text = "无";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1002, 571);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 18);
            this.label10.TabIndex = 14;
            this.label10.Text = "状态：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1002, 611);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 18);
            this.label11.TabIndex = 15;
            this.label11.Text = "累计执行次数：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1149, 611);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 18);
            this.label12.TabIndex = 16;
            this.label12.Text = "无";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1005, 430);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(475, 49);
            this.button3.TabIndex = 17;
            this.button3.Text = "清理缓存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // 规则引擎测测试
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1539, 986);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.outputTime);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textGuiZe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textShiJian);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBianLiang);
            this.Controls.Add(this.label1);
            this.Name = "规则引擎测测试";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBianLiang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textShiJian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textGuiZe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textZhouQi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBiShu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label outputTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button3;
    }
}


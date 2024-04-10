using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PythonRule
{
    public class Student
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return string.Format("{0} is {1} years old", this.Name, this.Age);
        }
    }
    public partial class Form1 : Form
    {
        string python = String.Empty;
        public Form1()
        {
            //python = "import datetime\t\n";
            //python += "print(\"current datetiem is:\"+ stuObj.Name)";
            python = "def getname()";
            InitializeComponent();
        }
      


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.CreateScope();
                Student stu = new Student { Name = "Wilber", Age = 28 };
                scope.SetVariable("stuObj", stu);
               
                var script = engine.CreateScriptSourceFromFile("Script.txt");
               

                var result = script.Execute(scope);
                 var p= result.GetName();

                MessageBox.Show(p);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }
    }
}

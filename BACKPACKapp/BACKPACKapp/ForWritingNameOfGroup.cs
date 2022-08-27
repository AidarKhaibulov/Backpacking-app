using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BACKPACKapp
{
    
    public partial class ForWritingNameOfGroup : Form
    {
        public bool Result;
        private static Button[] _buttons = new Button[10];
        public ForWritingNameOfGroup(string action)
        {
            for(int i=0;i<_buttons.Length;i++)
            {
                _buttons[i] = new Button();
            }
            InitializeComponent();
            if (action == "SaveGroup")
                label1.Text = "Write the name of this list";
            if (action == "LoadGroup")
            {
                Text = "Load data";
                label1.Text = "Write the name of the list you wish to open";
            }
            
            int counter = 0;
            foreach (var file in Directory.GetDirectories(Environment.CurrentDirectory + @"\Saves\"))
            {
                counter++;
                int i=file.Length-1;
                string FileName="";
                string b = "";
                while (!file[i].Equals('\\'))
                {
                    b += file[i];
                    i--;
                }
                for (int j = b.Length - 1; j >= 0; j--)
                    FileName+=b[j];
                
                _buttons[counter-1].Location = new Point(40,120+counter*20 );
                _buttons[counter-1].Name = "button" + (counter-1);
                _buttons[counter-1].Text = FileName;
                _buttons[counter-1].Size = new Size(240, 20);
                Controls.Add(_buttons[counter-1]);
                _buttons[counter-1].Click += FastLoadButtonAction;
            }

            
        }
        public void FastLoadButtonAction(object sender, EventArgs e)
        {
            textBox1.Text=((Button)sender).Text;
            Result = true;
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Result = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = false;
            Close();
        }
    }
}
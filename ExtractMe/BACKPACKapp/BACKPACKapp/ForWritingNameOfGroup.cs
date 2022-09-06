using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BACKPACKapp
{
    public partial class ForWritingNameOfGroup : Form
    {
        public bool Result;
        private Label[] l;
        private static Button[] _buttons = new Button[10];
        public ForWritingNameOfGroup(string action,Label[] labels=null)
        {
            
            
            InitializeComponent();
            l = labels;
            for (int i = 0; i < 6; i++)
                if (l != null && l[i] == null)
                {
                    l[i] = new Label();
                    l[i].Text = "";
                }
            

            for(int i=0;i<_buttons.Length;i++)
                _buttons[i] = new Button();
            BackColor = Color.FromArgb(187,198,200);
            textBox1.BackColor=Color.FromArgb(229,227,228);
            button1.BackColor=Color.FromArgb(229,227,228);
            button2.BackColor=Color.FromArgb(229,227,228);
            if (action == "SaveGroup")
            {
                label1.Text = "Write the name of this list";
                label1.Location = new Point(95, 10);
                label1.Size = new Size(215, 15);
                Location = new Point(100,75);
            }

            if (action == "LoadGroup")
            {
                Text = "Load data";
                Location = new Point(590,260);
                Size = new Size(321,475);
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                button2.Location = new Point(40, 10);
                button2.Size = new Size(240, 20);
                button2.Text = "Create a new list";
                int counter = 0;
                foreach (var file in Directory.GetDirectories(Environment.CurrentDirectory + @"\Saves\"))
                {
                    counter++;
                    int i = file.Length - 1;
                    string FileName = "";
                    string b = "";
                    while (!file[i].Equals('\\'))
                    {
                        b += file[i];
                        i--;
                    }

                    for (int j = b.Length - 1; j >= 0; j--)
                        FileName += b[j];

                    _buttons[counter - 1].Location = new Point(40, 58 + counter * 20);
                    _buttons[counter - 1].Name = "button" + (counter - 1);
                    _buttons[counter - 1].Text = FileName;
                    _buttons[counter - 1].Size = new Size(240, 20);
                    _buttons[counter - 1].BackColor=Color.FromArgb(229,227,228);
                    Controls.Add(_buttons[counter - 1]);
                    _buttons[counter - 1].Click += FastLoadButtonAction;
                    label2.Visible = true;
                }
            }
            if (action == "OpenGroup")
            {
                Text = "Open file";
                Location = new Point(135,75);
                label2.Location = new Point(81,21);
                label2.Text = "Select the file you want to open";
                Size = new Size(321,475);
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                button2.Location = new Point(300, 18);
                button2.Size = new Size(20, 20);
                button2.Text = "X";
                button2.Visible = true;
                int counter = 0;
                foreach (var file in Directory.GetDirectories(Environment.CurrentDirectory + @"\Saves\"))
                {
                    counter++;
                    int i = file.Length - 1;
                    string FileName = "";
                    string b = "";
                    while (!file[i].Equals('\\'))
                    {
                        b += file[i];
                        i--;
                    }

                    for (int j = b.Length - 1; j >= 0; j--)
                        FileName += b[j];

                    _buttons[counter - 1].Location = new Point(40, 58 + counter * 20);
                    _buttons[counter - 1].Name = "button" + (counter - 1);
                    _buttons[counter - 1].Text = FileName;
                    _buttons[counter - 1].Size = new Size(240, 20);
                    _buttons[counter - 1].BackColor=Color.FromArgb(229,227,228);
                    Controls.Add(_buttons[counter - 1]);
                    _buttons[counter - 1].Click += FastLoadButtonAction;
                    label2.Visible = true;
                }
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
            if (textBox1.Text != "" && l.All(s=> textBox1.Text!=s.Text) )
            {
                Result = true;
                Close();
            }
            else    
            if(textBox1.Text != "")
                MessageBox.Show("This name already exists!");
            else 
                MessageBox.Show("Write the name of group!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = false;
            Close();
        }
    }
}
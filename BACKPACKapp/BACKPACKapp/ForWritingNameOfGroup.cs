using System;
using System.Windows.Forms;

namespace BACKPACKapp
{
    
    public partial class ForWritingNameOfGroup : Form
    {
        public bool Result;
        public ForWritingNameOfGroup(string action)
        {
            InitializeComponent();
            if (action == "SaveGroup")
                label1.Text = "Write the name of this list";
            if (action == "LoadGroup")
                label1.Text = "Write the name of the list you wish to open";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = false;
            Close();
        }
    }
}
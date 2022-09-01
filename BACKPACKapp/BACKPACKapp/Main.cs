using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace BACKPACKapp
{
    public partial class Main : Form
    {
        private static int Groups_Amount_Limit = 6;
        private DataGridView[] DataGridViews = new DataGridView[Groups_Amount_Limit];
        private Button[] Buttons = new Button[Groups_Amount_Limit];
        public Label[] Labels = new Label[Groups_Amount_Limit];
        private Label[] Weights = new Label[Groups_Amount_Limit];
        private int CurrentGroupsID = 0;
        AddNewGroupClass addNewGroupClass= new AddNewGroupClass();
        private bool[] PositionStatus =  {true, true, true, true, true, true};
        private bool[] ID = {true, true, true, true, true, true};
        private int[] WeightsOfGroups = {0, 0, 0, 0, 0, 0, 0};
        private string comboBoxPreviousValue;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
 
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        public Main()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(70, 149, 151);
            AddGroupButton.BackColor = Color.FromArgb(229,227,228);
            SaveButton.BackColor = Color.FromArgb(229, 227, 228);
            totalWeightTextBox.Text = 0.ToString();
            comboBox1.Items.Add("kg");
            comboBox1.Items.Add("lb");
            comboBox1.SelectedItem = "kg";
            

        }

        private void Main_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(SaveButton, "Save file");
            t.SetToolTip(AddGroupButton, "Add new group");
            t.SetToolTip(OpenFileButton, "Open file");
            ForWritingNameOfGroup nameOfGroup = new ForWritingNameOfGroup("LoadGroup");
            nameOfGroup.ShowDialog();
            string name =nameOfGroup.textBox1.Text;
            if (LoadSaveClass.CheckLoadData(name) != 0)
            {
                label2.Text = name;
                for (CurrentGroupsID = 0; CurrentGroupsID < LoadSaveClass.CheckLoadData(name);)
                {
                    int j;
                    DataGridView[] dataGridView= new DataGridView[6];
                    Button[] button = new Button[6];
                    addNewGroupClass.AddNewGroup("12",Labels,Weights,ID,DataGridViews, Buttons, CurrentGroupsID, out dataGridView[0], out button[0],
                        out PositionStatus,out ID,out j);
                    Controls.Add(DataGridViews[j]);
                    dataGridView[0] = dataGridView[j];
                    button[0] = button[j];
                    Controls.Add(Buttons[j]);
                    Controls.Add(Labels[j]);
                    Controls.Add(Weights[j]);
                    Buttons[j].Click += DeleteGroupButtonClick;
                    DataGridViews[j].CellValueChanged += CalculatingWeight;
                    ID[j] = false;
                    CurrentGroupsID++;
                    
                    {
                        if (DataGridViews[j] != null)
                        {
                            // Selection Color
                            DataGridViews[j].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                            //dgv.DefaultCellStyle.SelectionForeColor = Color.Transparent;

                            // Default Color
                            DataGridViews[j].DefaultCellStyle.BackColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].DefaultCellStyle.ForeColor = Color.Black;
                    
                            DataGridViews[j].EnableHeadersVisualStyles = false;
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.BackColor =
                                DataGridViews[j].RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.ForeColor =
                                DataGridViews[j].RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.Font =
                                new Font("Times New Roman", 10,FontStyle.Bold);
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    
                            DataGridViews[j].BackgroundColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].RowHeadersVisible = false;
                            DataGridViews[j].ClearSelection();
                        }
                    }
                }
            }
            
            if (nameOfGroup.Result)
                LoadSaveClass.LoadData(DataGridViews,Buttons,Labels,name,WeightsOfGroups);
            CalculatingSummaryWeight(WeightsOfGroups);
        }

        public void DeleteGroupButtonClick(object sender, EventArgs e)
        {
            int i=Convert.ToInt32(((Control) sender).Name.Replace("button", ""));
            UpdatingGroupsLocationClass.Update(Labels,Weights,ID,CurrentGroupsID,i,PositionStatus,DataGridViews,Buttons,out PositionStatus,out DataGridViews, out Buttons,out CurrentGroupsID);
            WeightsOfGroups[i] = 0;
            CalculatingSummaryWeight(WeightsOfGroups);
        }
        public void AddGroupButton_Click(object sender, EventArgs e)
        {
            ForWritingNameOfGroup nameOfGroup = new ForWritingNameOfGroup("",Labels);
            Graphics g = CreateGraphics();
            Point[] points = new Point[3];
            points[0].X = 52; points[0].Y = 41;
            points[1].X = 182; points[1].Y = 80;
            points[2].X = 82; points[2].Y = 150;
            g.FillPolygon(Brushes.Silver, points);
            nameOfGroup.ShowDialog();
            g.Clear(Color.FromArgb(70, 149, 151));
            
            string name =nameOfGroup.textBox1.Text;
            if (nameOfGroup.Result)
            {
                int j;
                if (CurrentGroupsID < 6)
                {
                    DataGridView[] dataGridView = new DataGridView[6];
                    Button[] button = new Button[6];
                    addNewGroupClass.AddNewGroup(name, Labels,Weights, ID, DataGridViews, Buttons, CurrentGroupsID,
                        out dataGridView[0], out button[0],
                        out PositionStatus, out ID, out j);
                    Controls.Add(DataGridViews[j]);
                    dataGridView[0] = dataGridView[j];
                    button[0] = button[j];
                    Controls.Add(Buttons[j]);
                    Controls.Add(Labels[j]);
                    Controls.Add(Weights[j]);
                    Buttons[j].Click += DeleteGroupButtonClick;
                    DataGridViews[j].CellValueChanged += CalculatingWeight;
                    ID[j] = false;
                    CurrentGroupsID++;
                }
                else
                    MessageBox.Show("Group's limit has been reached");
            }
            foreach (DataGridView dgv in DataGridViews)
            {
                if (dgv != null)
                {
                    // Selection Color
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229,227,228);
                    dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(229,227,228);

                    // Default Color
                    dgv.DefaultCellStyle.BackColor = Color.FromArgb(91, 161, 153);
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    
                    dgv.EnableHeadersVisualStyles = false;
                        dgv.ColumnHeadersDefaultCellStyle.BackColor =
                      dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 161, 153);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                        dgv.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(91, 161, 153);
                    dgv.ColumnHeadersDefaultCellStyle.Font =
                        new Font("Times New Roman", 10,FontStyle.Bold);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    
                    dgv.BackgroundColor = Color.FromArgb(91, 161, 153);
                    dgv.ClearSelection();
                    dgv.RowHeadersVisible = false;
                }
            }
        }

        private void CalculatingSummaryWeight(int[] weights)
        {
            int sum = 0;
            int i = 0;
            foreach (var VARIABLE in weights)
            {
                sum += VARIABLE;
                if (VARIABLE != 0 && Weights[i] != null)
                {
                    Weights[i].Text = VARIABLE.ToString();
                    i++;
                }
            }
            totalWeightTextBox.Text = sum.ToString();
            
        }

        private void CalculatingWeight(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView) sender).CurrentCell.ColumnIndex==2)
            {
                int sum = 0;
                for (int i=0;i<((DataGridView) sender).Rows.Count;i++)
                    if ((string) (((DataGridView) sender).Rows[((DataGridView) sender).CurrentCell.RowIndex].Cells[2].Value) != string.Empty)
                        sum += Convert.ToInt32(((DataGridView) sender)[2,i].Value); 
                WeightsOfGroups[Convert.ToInt32(((DataGridView) sender).Name.Replace("DataGridView", ""))] = sum;
                CalculatingSummaryWeight(WeightsOfGroups);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            Point[] points = new Point[3];
            points[0].X = 89; points[0].Y = 41;
            points[1].X = 220; points[1].Y = 80;
            points[2].X = 120; points[2].Y = 150;
            g.FillPolygon(Brushes.Silver, points);
            ForWritingNameOfGroup nameOfGroup = new ForWritingNameOfGroup("SaveGroup");
            nameOfGroup.ShowDialog();
            g.Clear(Color.FromArgb(70, 149, 151));
            string name =nameOfGroup.textBox1.Text;
            if (nameOfGroup.Result)
                LoadSaveClass.SaveData(DataGridViews,Buttons,PositionStatus,Labels,name,WeightsOfGroups);
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            
            Graphics g = CreateGraphics();
            Point[] points = new Point[3];
            points[0].X = 123; points[0].Y = 41;
            points[1].X = 254; points[1].Y = 80;
            points[2].X = 154; points[2].Y = 150;
            g.FillPolygon(Brushes.Silver, points);
            ForWritingNameOfGroup nameOfGroup = new ForWritingNameOfGroup("OpenGroup");
            nameOfGroup.ShowDialog();
            
            for(int i =0;i<6;i++)
                if(Buttons[i]!=null)
                    Buttons[i].PerformClick();
            g.Clear(Color.FromArgb(70, 149, 151));
            string name =nameOfGroup.textBox1.Text;
            if (LoadSaveClass.CheckLoadData(name) != 0)
            {
                label2.Text = name;
                for (CurrentGroupsID = 0; CurrentGroupsID < LoadSaveClass.CheckLoadData(name);)
                {
                    int j;
                    DataGridView[] dataGridView= new DataGridView[6];
                    Button[] button = new Button[6];
                    addNewGroupClass.AddNewGroup("12",Labels,Weights,ID,DataGridViews, Buttons, CurrentGroupsID, out dataGridView[0], out button[0],
                        out PositionStatus,out ID,out j);
                    Controls.Add(DataGridViews[j]);
                    dataGridView[0] = dataGridView[j];
                    button[0] = button[j];
                    Controls.Add(Buttons[j]);
                    Controls.Add(Labels[j]);
                    Controls.Add(Weights[j]);
                    Buttons[j].Click += DeleteGroupButtonClick;
                    DataGridViews[j].CellValueChanged += CalculatingWeight;
                    ID[j] = false;
                    CurrentGroupsID++;
                    
                    {
                        if (DataGridViews[j] != null)
                        {
                            // Selection Color
                            DataGridViews[j].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                            //dgv.DefaultCellStyle.SelectionForeColor = Color.Transparent;

                            // Default Color
                            DataGridViews[j].DefaultCellStyle.BackColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].DefaultCellStyle.ForeColor = Color.Black;
                    
                            DataGridViews[j].EnableHeadersVisualStyles = false;
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.BackColor =
                                DataGridViews[j].RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.ForeColor =
                                DataGridViews[j].RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.Font =
                                new Font("Times New Roman", 10,FontStyle.Bold);
                            DataGridViews[j].ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    
                            DataGridViews[j].BackgroundColor = Color.FromArgb(91, 161, 153);
                            DataGridViews[j].RowHeadersVisible = false;
                            DataGridViews[j].ClearSelection();
                        }
                    }
                }
            }
            if (nameOfGroup.Result)
                LoadSaveClass.LoadData(DataGridViews,Buttons,Labels,name,WeightsOfGroups);
            CalculatingSummaryWeight(WeightsOfGroups);
            
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            comboBoxPreviousValue = comboBox1.Text;
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(WeightsOfGroups.Any(x => x != 0))
            switch (comboBox1.SelectedItem)
            {
                case "kg":
                    switch (comboBoxPreviousValue)
                    {
                        case "lb":
                            for(int i=0;i<WeightsOfGroups.Length;i++)
                                WeightsOfGroups[i]= (int) (WeightsOfGroups[i] * 0.45359237);
                            CalculatingSummaryWeight(WeightsOfGroups);
                            LeftClick();
                            break;
                        case "kg":
                            break;
                    }
                    break;
                
                case "lb":
                    switch (comboBoxPreviousValue)
                    {
                        case "kg":
                            for(int i=0;i<WeightsOfGroups.Length;i++)
                                WeightsOfGroups[i]= (int) (WeightsOfGroups[i] * 2.204622);
                            CalculatingSummaryWeight(WeightsOfGroups);
                            LeftClick();
                            break;
                        case "lb":
                            break;
                    }
                    break;
            }
        }
        
        private static void LeftClick()
        { 
            Cursor.Position = new Point(1400, 52);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);

        }
        
    }
}
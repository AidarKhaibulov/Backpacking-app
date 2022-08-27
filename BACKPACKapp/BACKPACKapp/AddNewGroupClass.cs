using System;
using System.Drawing;
using System.Windows.Forms;

namespace BACKPACKapp
{
    internal class AddNewGroupClass 
    {
        private static int[,] GroupsLocationsCoordinatesForDataGridView = { {45,545,1045,45,545,1045},{300,300,300,550,550,550} };
        private static int[,] GroupsLocationsCoordinatesForButtons = { {435,935,1435,435,935,1435},{280,280,280,530,530,530} };
        private static int[,] GroupsLocationsCoordinatesForLabels = { {335,835,1335,335,835,1335},{280,280,280,530,530,530} };
        private bool[] LocationOfPositionStatus = {true, true, true, true, true, true};
        private int j = 0;
        public void AddNewGroup(string name,Label[] labels,bool[] ID,DataGridView[] DataGridViews,Button[] Buttons, int CurrentGroupsID, out DataGridView dataGridViewReturn,out Button button,out bool[] locationOfPositionStatusReturn,out bool[] IDReturn,out int l)
        {
            
            l = 0;
            for (int CurrentLocationOfPosition = 0; CurrentLocationOfPosition < 6; CurrentLocationOfPosition++)
            {
                string LabelText = name;
                bool ChangeChecker = false;
                for(j=0;j<6;j++)
                    if (LocationOfPositionStatus[CurrentLocationOfPosition] && ID[j])
                    {
                        l = j;
                        DataGridViews[j] = new DataGridView();
                        Buttons[j] = new Button();
                        labels[j] = new Label();
                    ChangeChecker = true;
                    DataGridViews[j].Location = new Point(45 + CurrentGroupsID * 500, 300);
                    DataGridViews[j].Location = new Point(GroupsLocationsCoordinatesForDataGridView[0,CurrentLocationOfPosition],GroupsLocationsCoordinatesForDataGridView[1,CurrentLocationOfPosition] );
                    DataGridViews[j].Name = "DataGridView" + j;
                    DataGridViews[j].Size = new Size(450, 200);
                    DataGridViews[j].Columns.Add("_name", "Name");
                    DataGridViews[j].Columns.Add("_description", "Description");
                    DataGridViews[j].Columns.Add("_weight", "Weight");
                    DataGridViews[j].Columns[1].Width = 247;
                    Buttons[j].Location = new Point(GroupsLocationsCoordinatesForButtons[0,CurrentLocationOfPosition],GroupsLocationsCoordinatesForButtons[1,CurrentLocationOfPosition] );
                    Buttons[j].Name = "button" + j;
                    Buttons[j].Text = @"Delete";
                    Buttons[j].Size = new Size(60, 20);
                    labels[j].Location = new Point(GroupsLocationsCoordinatesForLabels[0,CurrentLocationOfPosition],GroupsLocationsCoordinatesForLabels[1,CurrentLocationOfPosition] );
                    labels[j].Name = "label" + j;
                    labels[j].Text = LabelText;
                    labels[j].Size = new Size(100, 20);
                    LocationOfPositionStatus[CurrentLocationOfPosition] = false;
                    ID[j] = false;
                    Buttons[j].BackColor =Color.FromArgb(229,227,228) ;
                    
                    }
                if (ChangeChecker)
                    break;
            }
            
            dataGridViewReturn = DataGridViews[j-1];
            button= Buttons[j-1];
            locationOfPositionStatusReturn = LocationOfPositionStatus;
            IDReturn = ID;
        }


    }
}
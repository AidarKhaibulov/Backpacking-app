using System.Drawing;
using System.Windows.Forms;

namespace BACKPACKapp
{
    public static class UpdatingGroupsLocationClass
    {
        public static void Update(
            Label[] labels,
            Label[] weight,
            bool[] ID,
            int CurrentGroupsID,
            int p,
            bool[] PositionStatusR,
            DataGridView[] dataGridView,
            Button[] button,out bool[] PosStat,
            out DataGridView[] dataGridViewsReturn,
            out Button[] buttonsReturn,
            out int currentGroupsIDReturn)
        {
            bool[] PositionStatus = PositionStatusR;
            int Coordinates = dataGridView[p].Location.X+dataGridView[p].Location.Y;
            int position = DeterminePosition(Coordinates);
            dataGridView[p].Dispose();
            dataGridView[p]=null;
            button[p].Dispose();
            button[p]=null;
            labels[p].Dispose();
            labels[p]=null;
            weight[p].Dispose();
            weight[p]=null;
            ID[p] = true;
            CurrentGroupsID--;
            for (int j = 0; j < 6; j++)
            {
                if( j!=p &&  dataGridView[j]!=null)
                {
                    var CoordinatesOfCurrent = dataGridView[j].Location.X+dataGridView[j].Location.Y;
                    var positionOfCurrent = DeterminePosition(CoordinatesOfCurrent);

                    if (positionOfCurrent > position)
                    {
                        if (dataGridView[j].Location.X == 45 &&
                            dataGridView[j].Location.Y == 550)
                        {
                            dataGridView[j].Location = new Point(1045,300);
                            button[j].Location = new Point(1476,280);
                            labels[j].Location = new Point(1045,280);
                            weight[j].Location = new Point(1245,280);
                        }
                        else
                        {
                            dataGridView[j].Location = new Point(
                                dataGridView[j].Location.X - 500, dataGridView[j].Location.Y);
                            button[j].Location = new Point(button[j].Location.X - 500,
                                button[j].Location.Y);
                            labels[j].Location = new Point(labels[j].Location.X - 500,
                                labels[j].Location.Y);
                            weight[j].Location = new Point(weight[j].Location.X - 500,
                                weight[j].Location.Y);
                            
                        }
                    }
                }
            }
            for(int j=5;j>-1;j--)
                if (PositionStatus[j] == false)
                {
                    PositionStatus[j] = true;
                    break;
                }
            dataGridViewsReturn=dataGridView;
            buttonsReturn = button;
            PosStat = PositionStatus;
            currentGroupsIDReturn = CurrentGroupsID;
        }

        private static int DeterminePosition(int Coordinates)
        {
            int position = 0;
            switch (Coordinates)
            {
                case 345:
                    position=0;
                    break;
                case 845:
                    position=1;
                    break;
                case 1345:
                    position=2;
                    break;
                case 595:
                    position=3;
                    break;
                case 1095:
                    position=4;
                    break;
                case 1595:
                    position=5;
                    break;
            }
            return position;
        }
    }
}
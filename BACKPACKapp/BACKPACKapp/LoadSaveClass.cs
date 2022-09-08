using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BACKPACKapp
{
    public static class LoadSaveClass
    {
        
        public static void SaveData(DataGridView[] dataGridViews,Button[] button, bool [] PositionStatus,Label[] labels,string name, int [] WeightsOfGroups)
        {
            if(Directory.Exists(Environment.CurrentDirectory + @"\Saves\" + name ))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\Saves\" + name);
 
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
            }
            if (PositionStatus.Any(x => x == false))
            {
                for (int i = 0; i < 6; i++)
                {
                    if (dataGridViews[i] != null)
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + @"\Saves\" + name );
                        string file = Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"DGV.txt";
                        using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
                        {
                            bw.Write(dataGridViews[i].Columns.Count);
                            bw.Write(dataGridViews[i].Rows.Count);
                            foreach (DataGridViewRow dgvR in dataGridViews[i].Rows)
                            {
                                for (int j = 0; j < dataGridViews[i].Columns.Count; ++j)
                                {
                                    object val = dgvR.Cells[j].Value;
                                    if (val == null)
                                    {
                                        bw.Write(false);
                                        bw.Write(false);
                                    }
                                    else
                                    {
                                        bw.Write(true);
                                        bw.Write(val.ToString());
                                    }
                                }
                            }
                        }

                        file = Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"DGVLocation.txt";
                        using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
                        {
                            bw.Write(dataGridViews[i].Location.X);
                            bw.Write(dataGridViews[i].Location.Y);
                        }

                        file = Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"ButtonLocation.txt";
                        using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
                        {
                            bw.Write(button[i].Location.X);
                            bw.Write(button[i].Location.Y);
                        }
                        file = Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"LabelLocation.txt";
                        using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
                        {
                            bw.Write(labels[i].Location.X);
                            bw.Write(labels[i].Location.Y);
                            bw.Write(labels[i].Text);
                        }
                        file = Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+  @"Weights.txt";
                        using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
                            foreach (var VARIABLE in WeightsOfGroups)
                                bw.Write(VARIABLE);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    System.IO.File.Delete(Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"DGV.txt");
                    System.IO.File.Delete(Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"DGVLocation.txt");
                    System.IO.File.Delete(Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"ButtonLocation.txt");
                    System.IO.File.Delete(Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ i + @"LabelLocation.txt");
                }
            }
        }

        public static int CheckLoadData(string name)
        {
            int count = 0;
            for (int k = 0; k < 10; k++)
                if (File.Exists(Environment.CurrentDirectory + @"\Saves\"+ name+@"\"+ k + @"DGV.txt"))
                    count++;
            return count;
        }
        
        public static void LoadData(DataGridView[] dataGridViews,Button[] button,Label[] labels,string name,int[] WeightsOfGroups)
        {
            int p = 0;
            for (int k = 0; k < 6; k++)
            {
                if (File.Exists(Environment.CurrentDirectory + @"\Saves\" + name + @"\" + k + @"DGV.txt"))
                {
                    string file = Environment.CurrentDirectory + @"\Saves\" + name + @"\" + k + @"DGV.txt";
                    using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        int n = bw.ReadInt32();
                        int m = bw.ReadInt32();
                        for (int i = 0; i < m; ++i)
                        {
                            dataGridViews[p].Rows.Add();
                            for (int j = 0; j < n; ++j)
                            {
                                if (bw.ReadBoolean())
                                {
                                    dataGridViews[p].Rows[i].Cells[j].Value = bw.ReadString();
                                }
                                else bw.ReadBoolean();
                            }
                        }
                    }

                    file = Environment.CurrentDirectory + @"\Saves\" + name + @"\" + k + @"DGVLocation.txt";
                    using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        int x = bw.ReadInt32();
                        int y = bw.ReadInt32();
                        dataGridViews[p].Location = new Point(x, y);
                    }

                    file = Environment.CurrentDirectory + @"\Saves\" + name + @"\" + k + @"ButtonLocation.txt";
                    using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        int x = bw.ReadInt32();
                        int y = bw.ReadInt32();
                        button[p].Location = new Point(x, y);
                    }

                    file = Environment.CurrentDirectory + @"\Saves\" + name + @"\" + k + @"LabelLocation.txt";
                    using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        int x = bw.ReadInt32();
                        int y = bw.ReadInt32();
                        string text = bw.ReadString();
                        labels[p].Location = new Point(x, y);
                        labels[p].Text = text;
                    }

                    file = Environment.CurrentDirectory + @"\Saves\" + name + @"\" + @"Weights.txt";
                    using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
                        for (int i = 0; i < 6; i++)
                            WeightsOfGroups[i] = bw.ReadInt32();
                    p++;
                }
            }
        }
    }
}
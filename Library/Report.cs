using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Project2_Library
{
    public partial class Report : Form
    {
        ArrayList list = new ArrayList();
        string temp1;


        public Report()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main m = new Main();
            m.Show();
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter("books.dat");
            list.Clear();
            for (int i = 0; i < DGVreport.Rows.Count - 1; i++)
            {
                for (int j = 0; j < DGVreport.Rows[i].Cells.Count; j++)
                {
                    temp1 += DGVreport.Rows[i].Cells[j].Value + "|";
                }
                temp1 = temp1.Substring(0, temp1.Length - 1);
                list.Add(temp1);
                temp1 = "";
            }

            foreach (var line in list)
            {
                write.WriteLine(line.ToString());
            }
            write.Close();
            FMessegeBox.FarsiMessegeBox.Show("عملیات انجام شد", "موفقیت");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(DGVreport);
        }

        private void Report_Load(object sender, EventArgs e)
        {
            StreamReader read = new StreamReader("books.dat");
            try
            {
                while (!read.EndOfStream)
                {
                    list.Add(read.ReadLine());
                }
                read.Close();
             //   MessageBox.Show(list.Count.ToString());
                for (int i = 0; i < list.Count; i++)
                {
                    string[] str = Convert.ToString(list[i]).Split('|');
                    DGVreport.Rows.Add();
                    for (int j = 0; j < str.Length; j++)
                    {
                        DGVreport.Rows[i].Cells[j].Value = str[j];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                read.Close();
            }
        }
    }
}

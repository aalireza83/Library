using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.IO;

namespace Project2_Library
{
    public partial class Main : Form
    {

        static public string address = Application.StartupPath + "/books.dat";
        static string SoundLocation = "D://005.wav";
        static SoundPlayer sp = new SoundPlayer(SoundLocation);
        static SoundPlayer alarmsp = new SoundPlayer("D://Alarm.wav");
        static Thread Alarmth = new Thread(new ThreadStart(AlarmPlay));

        public Main()
        {
            InitializeComponent();
            timer1.Enabled = true;
            if (!File.Exists(address))
            {
                Stream create = File.Create(address);
                create.Close();
            }
            CheckTimes();
        }

        static public void AlarmPlay()
        {
            alarmsp.Play();
        }

        static string booksReturn = "";

        static public void CheckTimes()
        {
            string timeNow = dtePersianCalendar.GetHour(DateTime.Now) + ":" + dtePersianCalendar.GetMinute(DateTime.Now) + ":" + dtePersianCalendar.GetSecond(DateTime.Now);
            booksReturn = "";
            StreamReader read = File.OpenText(address);
            ArrayList list = new ArrayList();
            try
            {
                while (!read.EndOfStream)
                {
                    list.Add(read.ReadLine());
                }
                read.Close();
                for (int i = 0; i < list.Count; i++)
                {
                    string[] split = Convert.ToString(list[i]).Split('|');
                    string[] splTime = split[4].Split(':');
                    if (Convert.ToInt32(splTime[0]) < Convert.ToInt32(dtePersianCalendar.GetHour(DateTime.Now)))
                    {
                        booksReturn += split[1] + "  ";
                    }
                    else if (Convert.ToInt32(splTime[0]) == Convert.ToInt32(dtePersianCalendar.GetHour(DateTime.Now)))
                    {
                        if (Convert.ToInt32(splTime[1]) < Convert.ToInt32(dtePersianCalendar.GetMinute(DateTime.Now)))
                        {
                            booksReturn += split[1] + "  ";
                        }
                        else if(Convert.ToInt32(splTime[1]) == Convert.ToInt32(dtePersianCalendar.GetMinute(DateTime.Now)))
                        {
                            if (Convert.ToInt32(splTime[2]) < Convert.ToInt32(dtePersianCalendar.GetSecond(DateTime.Now)))
                            {
                                booksReturn += split[1] + "  ";
                            }
                            else if (Convert.ToInt32(splTime[2]) == Convert.ToInt32(dtePersianCalendar.GetSecond(DateTime.Now)))
                            {
                                Alarmth.Abort();
                                Alarmth = new Thread((AlarmPlay));
                                Alarmth.Start();
                                FMessegeBox.FarsiMessegeBox.Show("زمان " + split[1] + "برای برگرداندن کتاب فرا رسید", "ارور");
                                booksReturn += split[1] + "  ";
                            }
                        }
                    }
                }

                booksReturn = " زمان عضو های:" + "\n" + booksReturn + "\n برای برگرداندن کتب امانت گرفته شده گذشته است ";
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

        private static void AplayAlarm()
        {
            alarmsp.PlayLooping();
        }

        static System.Globalization.PersianCalendar dtePersianCalendar = new System.Globalization.PersianCalendar();
        //   static string today = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();

        private void timer1_Tick(object sender, EventArgs e)
        {
            string timeNow = dtePersianCalendar.GetHour(DateTime.Now) + ":" + dtePersianCalendar.GetMinute(DateTime.Now) + ":" + dtePersianCalendar.GetSecond(DateTime.Now);
            CheckTimes();
            lblAlarm.Text = "";
            lblAlarm.Text = booksReturn;
            DGVAdd.Rows[0].Cells[3].Value = timeNow;
            for (int i = 0; i < DGVAdd.Rows.Count - 1; i++)
            {
                DGVAdd.Rows[i].Cells[3].Value = timeNow;
            }

            Persia.SolarDate solarDate = Persia.Calendar.ConvertToPersian(DateTime.Now);
            lblToday.Text = " امروز " + solarDate.ToString("N");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string temp = "";
            if (DGVAdd.Rows[0].Cells[0].Value != null && DGVAdd.Rows[0].Cells[1].Value != null)
            {
                StreamWriter sw = File.AppendText(address);

                for (int i = 0; i < DGVAdd.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < DGVAdd.Rows[i].Cells.Count; j++)
                    {
                        temp += DGVAdd.Rows[i].Cells[j].Value + "|";
                    }
                    temp = temp.Substring(0, temp.Length - 1);
                    sw.WriteLine(temp);
                    temp = "";
                }
                sw.Close();
                /////=================
                for (int i = 0; i < DGVAdd.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < DGVAdd.Rows[i].Cells.Count; j++)
                    {
                        DGVAdd.Rows[i].Cells[j].Value = null;
                    }
                }

                FMessegeBox.FarsiMessegeBox.Show("عملیات با موفقیت انجام شد", "موفقیت");
            }
            else
            {
                FMessegeBox.FarsiMessegeBox.Show("لطفا تمامی فیلد ها را تکمیل نمایید", "خطا");
            }

        }

        private void buttonX3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sp.SoundLocation = openFileDialog1.FileName;
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report r = new Report();
            r.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            About a = new About();
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report r = new Report();
            r.Show();
        }
    }
}

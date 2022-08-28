﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Project2_Library
{

    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
            System.Globalization.PersianCalendar dtePersianCalendar = new System.Globalization.PersianCalendar();
            System.String Year, Month, Day, strResult;
            DateTime Date_Now = DateTime.Now;
            //-----------------------------------------------------------------------------
            Year = dtePersianCalendar.GetYear(Date_Now).ToString();
            Month = dtePersianCalendar.GetMonth(Date_Now).ToString();
            Day = dtePersianCalendar.GetDayOfMonth(Date_Now).ToString();
            strResult = Year + "/" + Month + "/" + Day;
            buttonX1.Text = strResult;
            
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (txtUser.Text=="admin"&&txtPass.Text=="1234")
            {
                Main m = new Main();
                m.Show();
                this.Hide();
            }
            else
            {
                FMessegeBox.FarsiMessegeBox.Show("اطلاعات وارد شده نادرست است", "خطا");
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttonX2.Text = DateTime.Now.ToLongTimeString();
        }

    }
}
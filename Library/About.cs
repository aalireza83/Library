using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project2_Library
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }
        int x=100,y=1000;
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main m = new Main();
            m.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (x < 1000)
            {
                this.Location = new Point(x, 200);
                x++;

            }
            if (x == 1000)
            {
                this.Location = new Point(y, 200);
                if (y == 100)
                {
                    x = 100;
                    y = 1000;
                }
                y--;
            }
        }
    }
}

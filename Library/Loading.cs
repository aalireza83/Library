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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarX1.PerformStep();
            if (progressBarX1.Value == progressBarX1.Maximum)
            {

                timer1.Enabled = false;
                this.Hide();
                Login x = new Login();
                x.ShowDialog();
            }
        }

        private void ribbonClientPanel1_Click(object sender, EventArgs e)
        {

        }

        
    }
}

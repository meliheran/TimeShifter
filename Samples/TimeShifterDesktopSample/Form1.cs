using meliheran;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeShifterDesktopSample
{
    public partial class Form1 : Form
    {
        TimeShifter<string> shifter;

        public Form1()
        {
            InitializeComponent();
            shifter = new TimeShifter<string>(3000, 200);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            shifter.OnEndShiftingTime += Shifter_OnEndShiftingTime;
        }

        private void Shifter_OnEndShiftingTime(string obj)
        {
            listBox1.Items.Add($"Time to go db !");
            listBox1.Items.Add($"Search: {obj}");
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Add($"No need to DB Call on text change..");
            await shifter.Shift(txtSearch.Text);
        }
    }
}

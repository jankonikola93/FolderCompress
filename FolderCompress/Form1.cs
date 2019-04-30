using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderCompress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Pomocna.BrowseFolderPath();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Pomocna.BrowseFolderPath();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {
                Pomocna.PrikaziPoruku(Pomocna.CompressAllFiles(textBox1.Text, textBox2.Text, textBox3.Text));
            }
            else if (radioButton2.Checked) {
                Pomocna.PrikaziPoruku(Pomocna.CompressByDate(textBox1.Text, textBox2.Text, textBox3.Text));
            }
        }
    }
}

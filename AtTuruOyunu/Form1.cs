using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtTuruOyunu
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
        private static int kareSayisi;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false && radioButton5.Checked == false)
            {
                MessageBox.Show("Kare sayısı seçmeden oyuna başlayamazsın!!!","Uyarı!!!");
            }
            else
            {
                if(radioButton1.Checked == true) {kareSayisi = 5;}
                else if(radioButton2.Checked == true) {kareSayisi = 6;}
                else if(radioButton3.Checked == true) { kareSayisi = 7;}
                else if (radioButton4.Checked == true) { kareSayisi = 8;}
                else if (radioButton5.Checked == true) { kareSayisi = 9;}
                this.Hide();
                Oyun form2 = new Oyun();
                form2.ShowDialog();
                this.Show();
            }
        }
        public static int kareSayisiDondur() { return kareSayisi; }

    }
}

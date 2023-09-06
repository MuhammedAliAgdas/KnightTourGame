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
    public partial class Oyun : Form
    {
        public Oyun()
        {
            InitializeComponent();
        }
        int x = 0, y = 0, satır = 0, sutun = 1, satırKontrol = 0;
        Button btn;
        List<String> Butonlistesi = new List<String>();
        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= Ayarlar.kareSayisiDondur()* Ayarlar.kareSayisiDondur(); i++)
            {
                satır++;
                btn = new Button();
                if (satırKontrol==0){ btn.Name = Convert.ToString(sutun) + "x" + Convert.ToString(satır); }
                else if(satırKontrol ==1) { satırKontrol = 0; satır = 1; sutun += 1; btn.Name = Convert.ToString(sutun) +"x"+Convert.ToString(satır); }
                btn.Click += new EventHandler(this.tikla);
                btn.FlatStyle=FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = Color.White;
                btn.FlatAppearance.MouseOverBackColor = Color.LightGreen;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.BackColor = Color.LightGray;
                btn.Size= new Size(50,50);
                btn.Location= new Point(x,y);
                x += 50;
                if (i % Ayarlar.kareSayisiDondur() == 0)
                {
                    y+=50;
                    x = 0;
                    satırKontrol = 1;
                }
                this.Controls.Add(btn);
            }
            
            label1.Location = new Point(Ayarlar.kareSayisiDondur()*50+100, (y / 3));
            label2.Location = new Point(Ayarlar.kareSayisiDondur() * 50 + 30, (y/ 4)-10);
            this.Size = new Size((Ayarlar.kareSayisiDondur() * 50)+270, (Ayarlar.kareSayisiDondur() * 50) + 50);
        }
        Button tikla1;
        int tiklamaSayisi;
        private void tikla(object sender, EventArgs e)
        {
            tiklamaSayisi++;
            tikla1 = (Button)sender;
            tikla1.Text = Convert.ToString(tiklamaSayisi);
            label1.Text = Convert.ToString(tiklamaSayisi);
            int[] butonIsim = ButonIsimAyarla(tikla1.Name);
            Butonlistesi.Add(tikla1.Name);
            for (int i = 1; i <=Ayarlar.kareSayisiDondur(); i++)
            {
                for (int j = 1; j <= Ayarlar.kareSayisiDondur(); j++)
                {
                    this.Controls[Convert.ToString(i) + "x" + Convert.ToString(j)].BackgroundImage = null;
                    this.Controls[Convert.ToString(i) + "x" + Convert.ToString(j)].Enabled = false;
                    if (!Butonlistesi.Contains(Convert.ToString(i) + "x" + Convert.ToString(j))) { this.Controls[Convert.ToString(i) + "x" + Convert.ToString(j)].BackColor = Color.LightGray; }
                    else { this.Controls[Convert.ToString(i) + "x" + Convert.ToString(j)].BackColor=Color.Green; }
                }
            }
            tikla1.BackgroundImage = Image.FromFile("C:\\Users\\aliag\\source\\repos\\AtTuruOyunu\\at.jpg");
            nullKontrol(Convert.ToString(butonIsim[0] - 2) + "x" + Convert.ToString(butonIsim[1] - 1));
            nullKontrol(Convert.ToString(butonIsim[0] - 2) + "x" + Convert.ToString(butonIsim[1] + 1));
            nullKontrol(Convert.ToString(butonIsim[0] - 1) + "x" + Convert.ToString(butonIsim[1] + 2));
            nullKontrol(Convert.ToString(butonIsim[0] - 1) + "x" + Convert.ToString(butonIsim[1] - 2));
            nullKontrol(Convert.ToString(butonIsim[0] + 1) + "x" + Convert.ToString(butonIsim[1] - 2));
            nullKontrol(Convert.ToString(butonIsim[0] + 1) + "x" + Convert.ToString(butonIsim[1] + 2));
            nullKontrol(Convert.ToString(butonIsim[0] + 2) + "x" + Convert.ToString(butonIsim[1] - 1));
            nullKontrol(Convert.ToString(butonIsim[0] + 2) + "x" + Convert.ToString(butonIsim[1] + 1));
            oyunSonu();
        }
        private void Form2_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (zaferSayaci != Ayarlar.kareSayisiDondur()*Ayarlar.kareSayisiDondur()&& yenilgiSayaci != Ayarlar.kareSayisiDondur() * Ayarlar.kareSayisiDondur())
            {
                DialogResult cevap = new DialogResult();
                cevap = MessageBox.Show("İlerleme silinecek. Ayarlara dönmek istediğine emin misin?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (cevap == DialogResult.Yes) { this.Dispose(); }
                else if (cevap == DialogResult.No) { e.Cancel = true; }
            }
        }
        private int[] ButonIsimAyarla(String name)
        {
            char[] isimHafleri = new char[name.Length];
            isimHafleri = name.ToCharArray();
            String sutun = "", satır = "";
            Boolean xKontrol = false;
            for (int i = 0; i < name.Length; i++)
            {
                if (isimHafleri[i]=='x'){ xKontrol = true; continue; }
                if (xKontrol) { satır += isimHafleri[i]; }
                else { sutun += isimHafleri[i]; } 
            }
            int[] butonIsim = {int.Parse(sutun),int.Parse(satır) };
            return butonIsim;
        }
        private void nullKontrol(String ButonIsim)
        {
            if (this.Controls[ButonIsim] != null && this.Controls[ButonIsim].BackColor!=Color.Green)
            {
                this.Controls[ButonIsim].BackColor = Color.LightGreen;
                this.Controls[ButonIsim].Enabled = true;
            }
        }
        int zaferSayaci, yenilgiSayaci;
        private void oyunSonu()
        {
            DialogResult tamam1 = new DialogResult();
            DialogResult tamam2 = new DialogResult();
            zaferSayaci = 0;
            yenilgiSayaci=0;
            for (int i = 1; i <= Ayarlar.kareSayisiDondur(); i++)
            {
                for (int j = 1; j <= Ayarlar.kareSayisiDondur(); j++)
                {
                    if (this.Controls[Convert.ToString(i) + "x" + Convert.ToString(j)].BackColor == Color.Green){zaferSayaci++;}
                    if (zaferSayaci == Ayarlar.kareSayisiDondur()* Ayarlar.kareSayisiDondur()){tamam1 = MessageBox.Show("KAZANDIN!","Tebrikler!"); if (tamam1 == DialogResult.OK) { this.Dispose(); } }
                    if(Butonlistesi.Count< Ayarlar.kareSayisiDondur() * Ayarlar.kareSayisiDondur() && this.Controls[Convert.ToString(i) + "x" + Convert.ToString(j)].Enabled == false) { yenilgiSayaci++;}
                    if(yenilgiSayaci== Ayarlar.kareSayisiDondur() * Ayarlar.kareSayisiDondur()) {tamam2= MessageBox.Show("Maalesef, atın gidecek bir yeri kalmadı.","Kaybettiniz!"); if (tamam2 == DialogResult.OK) { this.Dispose(); } }
                }
            }
        }
    }
}

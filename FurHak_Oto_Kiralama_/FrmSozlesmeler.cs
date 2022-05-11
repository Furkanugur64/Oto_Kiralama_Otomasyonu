using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FurHak_Oto_Kiralama_.Models;

namespace FurHak_Oto_Kiralama_
{
    public partial class FrmSozlesmeler : Form
    {
        public FrmSozlesmeler()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kadi;
        void Sozlesme_Listesi()
        {
            var liste = (from x in db.Sozlesmeler.Where(x => x.Durum == true)
                         select new
                         {
                             x.Id,
                             x.Tcno,
                             x.Ad,
                             x.Soyad,
                             Personel = x.Adminler.Ad + " " + x.Adminler.Soyad,
                             x.Cinsiyet,
                             x.Aracid,
                             x.Plaka,
                             x.Markalar.Marka,
                             x.Seriler.Seri,
                             x.Alistarihi,
                             x.Teslimtarihi,
                             x.Ucret,

                         }).ToList();
            dataGridView1.DataSource = liste;
        }
        private void FrmSozlesmeler_Load(object sender, EventArgs e)
        {
            Sozlesme_Listesi();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtPersonel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            TxtPlaka.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            TxtMarka.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            TxtSeri.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            TxtTc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TxtAlis.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            TxtIade.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            TxtUcret.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            aracid=int.Parse( dataGridView1.CurrentRow.Cells[6].Value.ToString());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TxtPerBul.Text = "";
            var liste = (from x in db.Sozlesmeler
                         select new
                         {
                             x.Id,
                             x.Tcno,
                             x.Ad,
                             x.Soyad,
                             Personel = x.Adminler.Ad + " " + x.Adminler.Soyad,
                             x.Cinsiyet,
                             x.Aracid,
                             x.Plaka,
                             x.Markalar.Marka,
                             x.Seriler.Seri,
                             x.Alistarihi,
                             x.Teslimtarihi,
                             x.Ucret,

                         }).Where(x => x.Ad.Contains(TxtAdBul.Text)).ToList();
            dataGridView1.DataSource = liste;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TxtAdBul.Text = "";
            var liste = (from x in db.Sozlesmeler
                         select new
                         {
                             x.Id,
                             x.Tcno,
                             x.Ad,
                             x.Soyad,
                             Personel = x.Adminler.Ad + " " + x.Adminler.Soyad,
                             x.Cinsiyet,
                             x.Aracid,
                             x.Plaka,
                             x.Markalar.Marka,
                             x.Seriler.Seri,
                             x.Alistarihi,
                             x.Teslimtarihi,
                             x.Ucret,

                         }).Where(x => x.Personel.Contains(TxtPerBul.Text)).ToList();
            dataGridView1.DataSource = liste;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var liste = (from x in db.Sozlesmeler
                             select new
                             {
                                 x.Id,
                                 x.Tcno,
                                 x.Ad,
                                 x.Soyad,
                                 Personel = x.Adminler.Ad + " " + x.Adminler.Soyad,
                                 x.Cinsiyet,
                                 x.Aracid,
                                 x.Plaka,
                                 x.Markalar.Marka,
                                 x.Seriler.Seri,
                                 x.Alistarihi,
                                 x.Teslimtarihi,
                                 x.Ucret,

                             }).ToList();
                dataGridView1.DataSource = liste;
            }
            else
            {
                Sozlesme_Listesi();
            }
        }
        public int aracid;
        Loglar lg = new Loglar();
        private void button1_Click(object sender, EventArgs e)
        {
            if (Txtid.Text != "")
            {
                DialogResult sonuc = MessageBox.Show("Sözleşme Bitirilsin Mi ?", "FurHak Oto Kiralama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    var soz = db.Sozlesmeler.Find(Convert.ToInt32( Txtid.Text));
                    soz.Durum = false;
                    var arac = db.Araclar.Find(aracid);
                    arac.Durum = true;
                    
                    MessageBox.Show("Sözleşme Feshedildi. Araç otoparka çekilecek", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lg.Islem = "Sistemdeki "+soz.Id+" Idli Sözleşme Feshedildi";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kadi.ToString();
                    db.Loglar.Add(lg);
                    db.SaveChanges();

                    Sozlesme_Listesi();
                    temizle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Bitirmek İstediğiniz Sözleşmeyi Seçiniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lg.Islem = "Sistedeki Sözleşme Feshedilirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TxtAdBul.Text = "";
            TxtPerBul.Text = "";
        }
        void temizle()
        {
            Txtid.Text = "";
            TxtPersonel.Text = "";
            TxtPlaka.Text = "";
            TxtMarka.Text = "";
            TxtSeri.Text = "";
            TxtTc.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtAlis.Text = "";
            TxtIade.Text = "";
            TxtUcret.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            temizle();
        
        }
    }
}

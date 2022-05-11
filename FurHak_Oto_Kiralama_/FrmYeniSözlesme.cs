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
    public partial class FrmYeniSözlesme : Form
    {
        public FrmYeniSözlesme()
        {
            InitializeComponent();
        }
        public string kadi;
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        int günlük = 0;
        int aylik = 0;
        int yillik = 0;
        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();
        }
        void Arac_Listesi()
        {


            var araclar = (from x in db.Araclar.Where(x=>x.Durum==true)
                           select new
                           {
                               x.Id,
                               x.Plaka,
                               x.Markalar.Marka,
                               x.Seriler.Seri,
                               x.Yil,
                               x.Renk,
                               x.Günlük,
                               x.Aylik,
                               x.Yillik,
                               x.Foto
                           }).ToList();            
            dataGridView1.DataSource = araclar;
        }
        void Personel_Getir()
        {
            var personeller = db.Adminler.ToList();
            CmbPersonel.DisplayMember = "Ad";
            CmbPersonel.ValueMember = "Id";
            CmbPersonel.DataSource = personeller;
           
        }
        void Musteri_Getir()
        {
            var Musteriler = db.Müsteriler.ToList();
            CmbTc.DisplayMember = "Tcno";
            CmbTc.ValueMember = "Id";
            CmbTc.DataSource = Musteriler;
           
        }
        void Marka_Getir()
        {
            var marka = db.Markalar.ToList();
            CmbMarka.DisplayMember = "Marka";
            CmbMarka.ValueMember = "Id";
            CmbMarka.DataSource = marka;
            CmbMarka.Text = "";
            CmbMarka.SelectedIndex = -1;
        }
        private void FrmYeniSözlesme_Load(object sender, EventArgs e)
        {
            Arac_Listesi();
            Personel_Getir();
            Musteri_Getir();
            Marka_Getir();
            CmbTc.SelectedIndex = -1;
            CmbPersonel.SelectedIndex = -1;
            TxtKullaniciadi.Text = "";
            TxtMail.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtCinsiyet.Text = "";
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            CmbZaman.Enabled = true;
            
            TxtBirimFiyat.Text = "";
            TxtSüre.Text = "";
            LblToplamTutar.Text = "0 TL";
            TxtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtPlaka.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            CmbMarka.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            CmbSeri.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            günlük = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value.ToString());
            aylik = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value.ToString());
            yillik = Convert.ToInt32(dataGridView1.CurrentRow.Cells[8].Value.ToString());

        }

        private void CmbPersonel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPersonel.SelectedIndex!=-1)
            {
                var personel = db.Adminler.Find(CmbPersonel.SelectedValue);
                TxtKullaniciadi.Text = personel.Kullaniciadi;
                TxtMail.Text = personel.Email;

            }
                
        }

        private void CmbTc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbTc.SelectedIndex!=-1)
            {
                var musteri = db.Müsteriler.Find(CmbTc.SelectedValue);
                TxtAd.Text = musteri.Adi;
                TxtSoyad.Text = musteri.Soyadi;
                TxtCinsiyet.Text = musteri.Cinsiyet;
            }
                
            
            
        }

        private void CmbZaman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbZaman.Text == "Gün")
            {
                TxtBirimFiyat.Text = günlük.ToString();
            }
            else if (CmbZaman.Text == "Ay")
            {
                TxtBirimFiyat.Text = aylik.ToString();
            }
            else if (CmbZaman.Text == "Yıl")
            {
                TxtBirimFiyat.Text = yillik.ToString();
            }
        }
        int tutar;
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            if (TxtBirimFiyat.Text != ""&& TxtSüre.Text!="")
            {
                tutar = Convert.ToInt32(TxtBirimFiyat.Text) * Convert.ToInt32(TxtSüre.Text);
                LblToplamTutar.Text = tutar + " TL";
            }
        }

        private void BtnSıfırla_Click(object sender, EventArgs e)
        {
            TxtBirimFiyat.Text = "";
            TxtSüre.Text = "";
            LblToplamTutar.Text = "0 TL";
            DtIadeTarihi.Value = DateTime.Parse( DateTime.Now.ToLongDateString());
        }

        private void TxtModelAra_TextChanged(object sender, EventArgs e)
        {
            TxtRenkAra.Text = "";
            var modelara = (from x in db.Araclar.Where(x=>x.Durum==true)
                            select new
                            {
                                x.Id,
                                x.Plaka,
                                x.Markalar.Marka,
                                x.Seriler.Seri,
                                x.Yil,
                                x.Renk,
                                x.Günlük,
                                x.Aylik,
                                x.Yillik,
                                x.Foto

                            }).Where(x => x.Yil.Contains(TxtModelAra.Text)).ToList();
            dataGridView1.DataSource = modelara;
        }

        private void TxtRenkAra_TextChanged(object sender, EventArgs e)
        {
            TxtModelAra.Text = "";
            var renkara = (from x in db.Araclar.Where(x => x.Durum == true)
                           select new
                           {
                               x.Id,
                               x.Plaka,
                               x.Markalar.Marka,
                               x.Seriler.Seri,
                               x.Yil,
                               x.Renk,
                               x.Günlük,
                               x.Aylik,
                               x.Yillik,
                               x.Foto

                           }).Where(x => x.Renk.Contains(TxtRenkAra.Text)).ToList();
            dataGridView1.DataSource = renkara;
        }

        private void Btniptal_Click(object sender, EventArgs e)
        {
            TxtRenkAra.Text = "";
            TxtModelAra.Text = "";
        }

        private void BtnIptalEt_Click(object sender, EventArgs e)
        {
            TxtID.Text = "";
            TxtPlaka.Text = "";
            CmbMarka.SelectedIndex = -1;
            CmbSeri.SelectedIndex = -1;
            CmbPersonel.SelectedIndex = -1;
            CmbTc.SelectedIndex = -1;
          
            pictureBox1.ImageLocation = null;
            TxtKullaniciadi.Text = "";
            TxtMail.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtCinsiyet.Text = "";
            CmbZaman.Enabled = false;
            CmbZaman.SelectedIndex = -1;          
            TxtBirimFiyat.Text = "";
            TxtSüre.Text = "";
            LblToplamTutar.Text = "0 TL";
            DtIadeTarihi.Value = DateTime.Parse(DateTime.Now.ToLongDateString());
            DtAlistarihi.Value = DateTime.Parse(DateTime.Now.ToLongDateString());
        }
        Loglar lg = new Loglar();
        private void BtnSözlesmeImzala_Click(object sender, EventArgs e)
        {
            if (TxtID.Text != "" && TxtKullaniciadi.Text != "" && TxtAd.Text != "" && TxtBirimFiyat.Text != "" && TxtSüre.Text != "" && LblToplamTutar.Text != "0 TL") 
            {
               

                Sozlesmeler soz = new Sozlesmeler();
                soz.Tcno = CmbTc.Text;
                soz.Ad = TxtAd.Text;
                soz.Soyad = TxtSoyad.Text;
                soz.Cinsiyet = TxtCinsiyet.Text;
                soz.Personel = byte.Parse(CmbPersonel.SelectedValue.ToString());
                soz.Aracid = byte.Parse(TxtID.Text);
                soz.Plaka = TxtPlaka.Text;
                soz.Marka = byte.Parse(CmbMarka.SelectedValue.ToString());
                soz.Seri = byte.Parse(CmbSeri.SelectedValue.ToString());
                soz.Alistarihi = DtAlistarihi.Value;
                soz.Teslimtarihi = DtIadeTarihi.Value;
                soz.Ucret = tutar;
                soz.Durum = true;
                int id = int.Parse(TxtID.Text);
                var gunarac = db.Araclar.Find(id);
                gunarac.Durum = false;
                db.Sozlesmeler.Add(soz);                
                MessageBox.Show("Sözleşme Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sisteme Yeni Sözleşme Eklendi.";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Lütfen verileri eksiksiz giriniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lg.Islem = "Sisteme Yeni Sözleşme Eklenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void CmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbMarka.SelectedIndex!=-1)
            {
                var marka = CmbMarka.SelectedIndex + 1;
                CmbSeri.Text = "";
                var seriler = db.Seriler.Where(x => x.Marka == marka).ToList();
                CmbSeri.DataSource = seriler;
                CmbSeri.DisplayMember = "Seri";
                CmbSeri.ValueMember = "Id";
                CmbSeri.SelectedIndex = -1;
            }
            
        }
    }
}

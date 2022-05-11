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
    public partial class FrmAnasayfa : Form
    {
        public string kullanıcıadi;
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        OtoKiralamaEntities db = new OtoKiralamaEntities();
        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
           
            
        }
        void kasa()
        {
            var toplamkasa = db.Sozlesmeler.Sum(x => x.Ucret);
            LblKasa.Text = toplamkasa + " TL";
        }

        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {
            kasa();
            var aktifkullanici = db.Adminler.Where(x => x.Kullaniciadi == kullanıcıadi).FirstOrDefault();
            LblAdSoyad.Text = aktifkullanici.Ad + " " + aktifkullanici.Soyad;
            LblKullanıcıAdı.Text = aktifkullanici.Kullaniciadi;
            pictureBox1.ImageLocation = aktifkullanici.Fotograf.ToString();
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            FrmBilgiGüncelle fr = new FrmBilgiGüncelle();
            fr.kullaniciadi = LblKullanıcıAdı.Text;
            fr.ShowDialog();
        }
        Loglar lg = new Loglar();
        private void BtnMusterıLıstesı_Click(object sender, EventArgs e)
        {
            FrmMusteriler fr = new FrmMusteriler();
            lg.Islem = "Müşteri Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            fr.kadi = kullanıcıadi;
            fr.Show();
            this.Hide();
           
        }

        private void BtnUrunLıstesı_Click(object sender, EventArgs e)
        {
            FrmAdminler ad = new FrmAdminler();
            lg.Islem = "Admin Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            
            ad.kadi = kullanıcıadi;
            ad.ShowDialog();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblTarıh.Text = DateTime.Now.ToLongDateString();
            LblSaat.Text = DateTime.Now.ToLongTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmMarkalar m = new FrmMarkalar();
            lg.Islem = "Marka Listesi Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            m.kadi = kullanıcıadi;
            m.ShowDialog();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmModeller model = new FrmModeller();
            lg.Islem = "Model Listesi Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            model.kadi = kullanıcıadi;
            model.ShowDialog();
            
        }

        private void BtnMarkaListesi_Click(object sender, EventArgs e)
        {
            FrmAraclar arac = new FrmAraclar();
            lg.Islem ="Araç Listesi Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            arac.kadi = kullanıcıadi;
            arac.Show();
            this.Hide();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmYeniArac yeni = new FrmYeniArac();
            lg.Islem = "Yeni Araç Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            yeni.kadi = kullanıcıadi;
            yeni.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmYeniSözlesme yeni = new FrmYeniSözlesme();
            lg.Islem = "Yeni Sözleşme Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            yeni.kadi = kullanıcıadi;
            yeni.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmSozlesmeler soz = new FrmSozlesmeler();
            lg.Islem = "Sözleşme Listesi Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            soz.kadi = kullanıcıadi;
            soz.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmLoglar lgm = new FrmLoglar();
            lgm.kadi = kullanıcıadi;
            lg.Islem = "Log Listesi Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            lgm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmIstatistik fr = new FrmIstatistik();
            lg.Islem = "İstatistik Formu Görüntülendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullanıcıadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();
            fr.kadi = kullanıcıadi;
            fr.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmGirisYap fr = new FrmGirisYap();
            this.Hide();
            fr.Show();
        }
    }
}

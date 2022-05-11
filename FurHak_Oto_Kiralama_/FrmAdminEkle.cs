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
    public partial class FrmAdminEkle : Form
    {
        public FrmAdminEkle()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kullanici;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Bir Resim Seçiniz";
            openFileDialog1.InitialDirectory = @"C:\Users\ASUS\source\repos\FurHak_Oto_Kiralama_\FurHak_Oto_Kiralama_\bin\Debug\Resim";
            openFileDialog1.DefaultExt = ".jpg";
            DialogResult sonuc = openFileDialog1.ShowDialog();

            if (sonuc == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                TxtFotograf.Text = openFileDialog1.FileName;
            }
        }
        Loglar lg = new Loglar();
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text!=""&& TxtSoyad.Text != "" && Txtkullaniciadi.Text != "" && TxtSifre.Text != "" && TxtMail.Text != "" )
            {
                Adminler a = new Adminler();
                a.Ad = TxtAd.Text;
                a.Soyad = TxtSoyad.Text;
                a.Kullaniciadi = Txtkullaniciadi.Text;
                a.Sifre = TxtSifre.Text;
                a.Email = TxtMail.Text;
                if (TxtFotograf.Text=="")
                {
                    a.Fotograf = @"C:\Users\ASUS\source\repos\FurHak_Oto_Kiralama_\FurHak_Oto_Kiralama_\bin\Debug\Resim\Personel.png";
                }
                else
                {
                    a.Fotograf = TxtFotograf.Text;
                }
                db.Adminler.Add(a);                
                MessageBox.Show("Admin Ekleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sisteme Yeni Admin Eklendi.";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kullanici.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lg.Islem = "Sisteme Yeni Admin Eklenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kullanici.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

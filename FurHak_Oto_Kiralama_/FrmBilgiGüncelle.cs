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
    public partial class FrmBilgiGüncelle : Form
    {
        public FrmBilgiGüncelle()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        
        public string kullaniciadi;
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

        

        private void FrmBilgiGüncelle_Load(object sender, EventArgs e)
        {
            var admin = db.Adminler.Where(z => z.Kullaniciadi == kullaniciadi).FirstOrDefault();
            Txtİd.Text = admin.Id.ToString();
            TxtAd.Text = admin.Ad;
            TxtSoyad.Text = admin.Soyad;
            Txtkullaniciadi.Text = admin.Kullaniciadi;
            TxtSifre.Text = admin.Sifre;
            TxtMail.Text = admin.Email;
            TxtFotograf.Text = admin.Fotograf;
            pictureBox1.ImageLocation = admin.Fotograf;


        }
        Loglar lg = new Loglar();
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id =Convert.ToInt16( Txtİd.Text);
            var admn = db.Adminler.Find(id);
            admn.Ad = TxtAd.Text;
            admn.Soyad = TxtSoyad.Text;
            admn.Kullaniciadi = Txtkullaniciadi.Text;
            admn.Sifre = TxtSifre.Text;
            admn.Email = TxtMail.Text;
            admn.Fotograf = TxtFotograf.Text;
            
            MessageBox.Show("Admin Kaydı Başarıyla Güncellendi", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lg.Islem = "Sistemdeki" + admn.Ad + " Adlı Admin Bilgisini Güncellendi";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = kullaniciadi.ToString();
            db.Loglar.Add(lg);
            db.SaveChanges();

        }

        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

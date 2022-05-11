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
    public partial class FrmGirisYap : Form
    {
        public FrmGirisYap()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSifremiUnuttum sifre = new FrmSifremiUnuttum();
            sifre.Show();
            this.Hide();
        }

        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
            lg.Islem = " Sistemden Çıkış Yapıldı";
            lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
            lg.Yetkili = "admin";
            db.Loglar.Add(lg);
            db.SaveChanges();
            Application.Exit();
        }

        

       

        private void TxtKullaniciAdi_Click(object sender, EventArgs e)
        {
            TxtKullaniciAdi.Text = "";
        }

        private void TxtSıfre_Click(object sender, EventArgs e)
        {
            TxtSıfre.Text = "";
        }
        Loglar lg = new Loglar();
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        private void BtnGırısYap_Click(object sender, EventArgs e)
        {
            if (TxtKullaniciAdi.Text!="" && TxtSıfre.Text!="" && TxtKullaniciAdi.Text!= "Kullanıcı Adı:")
            {
                var kullanici = db.Adminler.FirstOrDefault(x => x.Kullaniciadi == TxtKullaniciAdi.Text && x.Sifre == TxtSıfre.Text);
                if (kullanici!=null)
                {
                    FrmAnasayfa fr = new FrmAnasayfa();
                    fr.kullanıcıadi = kullanici.Kullaniciadi;
                    lg.Islem = kullanici.Ad + " "+kullanici.Soyad+" Adlı Admin Sisteme Giriş Yaptı";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kullanici.Kullaniciadi.ToString();
                    db.Loglar.Add(lg);
                    db.SaveChanges();
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış !!"); 
                    lg.Islem = "Giriş Yapılırken Hata Oluştu";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kullanici.Kullaniciadi.ToString();
                    db.Loglar.Add(lg);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Giriniz !!!");
                lg.Islem = "Kullanıcı Adı veya Şifre Boş Geçildi";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = "Admin";
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void FrmGirisYap_Load(object sender, EventArgs e)
        {
            
        }
    }
}

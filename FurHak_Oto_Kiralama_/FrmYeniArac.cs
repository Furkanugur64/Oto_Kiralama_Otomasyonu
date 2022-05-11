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
    public partial class FrmYeniArac : Form
    {
        public FrmYeniArac()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kadi;
        void Marka_Getir()
        {
            var marka = db.Markalar.ToList();
            CmbMarka.DisplayMember = "Marka";
            CmbMarka.ValueMember = "Id";
            CmbMarka.DataSource = marka;
            CmbMarka.Text = "";
        }
        
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();
        }

        private void FrmYeniArac_Load(object sender, EventArgs e)
        {
            Marka_Getir();
        }

        private void CmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            var marka = CmbMarka.SelectedIndex+1;
            CmbSeri.Text = "";           
            var seriler = db.Seriler.Where(x => x.Marka == marka).ToList();
            CmbSeri.DataSource = seriler;
            CmbSeri.DisplayMember = "Seri";
            CmbSeri.ValueMember = "Id";
            CmbSeri.SelectedIndex = -1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Bir Resim Seçiniz";
            openFileDialog1.InitialDirectory = @"C:\Users\ASUS\source\repos\FurHak_Oto_Kiralama_\FurHak_Oto_Kiralama_\bin\Debug\Arabalar";
            openFileDialog1.DefaultExt = ".jpg";
            DialogResult sonuc = openFileDialog1.ShowDialog();

            if (sonuc == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                TxtFoto.Text = openFileDialog1.FileName;
            }
        }
        Loglar lg = new Loglar();
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtPlaka.Text != "" && CmbMarka.SelectedIndex != -1 && CmbSeri.SelectedIndex != -1 && TxtModelYil.Text != "" && TxtMotor.Text != "" && TxtKm.Text != "" && TxtRenk.Text != "" && CmbYakıtTip.Text != "" && CmbVites.Text != "" && TxtMotorGücü.Text != "" && TxtGün.Text != "" && TxtAy.Text != "" && TxtYil.Text != ""&&TxtFoto.Text!="")
            {
                Araclar araba = new Araclar();
                araba.Plaka = TxtPlaka.Text;
                araba.Marka =byte.Parse( CmbMarka.SelectedValue.ToString());
                araba.Seri = byte.Parse(CmbSeri.SelectedValue.ToString());
                araba.Yil = TxtModelYil.Text;
                araba.Motor = TxtMotor.Text;
                araba.KM = TxtKm.Text;
                araba.Renk = TxtRenk.Text;
                araba.Yakittipi = CmbYakıtTip.Text;
                araba.Vites = CmbVites.Text;
                araba.Motorgucu = TxtMotorGücü.Text;
                araba.Foto = TxtFoto.Text;
                araba.Günlük = Convert.ToInt16(TxtGün.Text);
                araba.Aylik = Convert.ToInt16(TxtAy.Text);
                araba.Yillik = Convert.ToInt16(TxtYil.Text);
                araba.Aciklama = TxtAciklama.Text;
                araba.Durum = true;
                db.Araclar.Add(araba);
                MessageBox.Show("Araç Ekleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sisteme Yeni Araç Eklendi.";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
                FrmAnasayfa ana = new FrmAnasayfa();
                ana.kullanıcıadi = kadi;
                ana.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen verileri eksiksiz giriniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lg.Islem = "Sisteme Yeni Araç Eklenirken Hata Oluştu.";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }
    }
}

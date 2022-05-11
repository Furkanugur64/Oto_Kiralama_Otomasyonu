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
    public partial class FrmYeniModel : Form
    {
        public FrmYeniModel()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kullanici;
        void Marka_Getir()
        {
            var marka = db.Markalar.ToList();
            CmbMarka.DisplayMember = "Marka";
            CmbMarka.ValueMember = "Id";
            CmbMarka.DataSource = marka;
            CmbMarka.SelectedIndex = -1;
        }
        private void FrmYeniModel_Load(object sender, EventArgs e)
        {
            Marka_Getir();
        }
        Loglar lg = new Loglar();
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtModel.Text!="" && CmbMarka.SelectedIndex!=-1)
            {
                Seriler s = new Seriler();
                var markaid = CmbMarka.SelectedValue;
                s.Marka = byte.Parse(markaid.ToString());
                s.Seri = TxtModel.Text;
                db.Seriler.Add(s);
                
                MessageBox.Show("Seri Ekleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sisteme Yeni Seri Eklendi.";
                lg.Zaman =DateTime.Parse( DateTime.Now.ToString());
                lg.Yetkili = kullanici.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bilgileri kontrol edin !!"); Loglar lg = new Loglar();
                lg.Islem = "Sisteme Seri Girilirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kullanici.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

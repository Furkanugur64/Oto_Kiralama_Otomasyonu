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
    public partial class FrmModeller : Form
    {
        public FrmModeller()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kadi;
        void temizle()
        {
            TxtId.Text = "";
            TxtModel.Text = "";
            CmbMarka.SelectedIndex = -1;
        }
        void ModelListesi()
        {
            var seriler = (from x in db.Seriler
                           select new
                           {
                               x.Id,
                               x.Markalar.Marka,
                               x.Seri
                           }).ToList();
            dataGridView1.DataSource = seriler;
        }
        void Marka_Getir()
        {
            var marka = db.Markalar.ToList();
            CmbMarka.DisplayMember = "Marka";
            CmbMarka.ValueMember = "Id";
            CmbMarka.DataSource = marka;
            CmbMarka.SelectedIndex = -1;

        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void FrmModeller_Load(object sender, EventArgs e)
        {
            ModelListesi();
            Marka_Getir();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            TxtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtModel.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            CmbMarka.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();
        }
        Loglar lg = new Loglar();
        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {
                DialogResult sonuc = MessageBox.Show("Seri Bilgisi Silinsin Mi ?", "FurHak Oto Kiralama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(TxtId.Text);
                    var silseri = db.Seriler.Find(id);
                    db.Seriler.Remove(silseri);
                    
                    MessageBox.Show("Seri Silme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lg.Islem = "Sistemdeki" + silseri.Seri + " Adlı Seri Silindi";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kadi.ToString();
                    db.Loglar.Add(lg);
                    db.SaveChanges();
                    ModelListesi();
                    temizle();
                }
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz seriyi seçiniz !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki Seri Silinirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();

            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {
                int id = Convert.ToInt32(TxtId.Text);
                var ser = db.Seriler.Find(id);
                var markaid = CmbMarka.SelectedValue;
                ser.Marka = byte.Parse(markaid.ToString());
                ser.Seri = TxtModel.Text;
               
                ModelListesi();
                MessageBox.Show("Seri Güncelleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki" + ser.Seri + " Adlı Seri Güncellendi";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Güncellemek istediğiniz seriyi seçiniz !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki Seri Güncellenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void BtnYeniMarka_Click(object sender, EventArgs e)
        {
            FrmYeniModel y = new FrmYeniModel();
            y.kullanici = kadi;
            y.ShowDialog();
            ModelListesi();
        }
    }
}

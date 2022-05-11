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
    public partial class FrmMarkalar : Form
    {
        public FrmMarkalar()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        
        void MarkaListesi()
        {
            var markalar = (from x in db.Markalar
                            select new
                            {
                                x.Id,
                                x.Marka
                            }).ToList();
            dataGridView1.DataSource = markalar;
        }
        private void FrmMarka_Load(object sender, EventArgs e)
        {
            MarkaListesi();
        }

        public string kadi;
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            TxtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
                DialogResult sonuc = MessageBox.Show("Marka Bilgisi Silinsin Mi ?", "FurHak Oto Kiralama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(TxtId.Text);
                    var silmarka = db.Markalar.Find(id);
                    db.Markalar.Remove(silmarka);
                    
                    MessageBox.Show("Marka Silme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lg.Islem = "Sisteme Yeni Marka Eklendi";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kadi.ToString();
                    db.Loglar.Add(lg);
                    db.SaveChanges();
                    MarkaListesi();
                    temizle();
                }
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz markayı seçiniz !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sisteme Yeni Marka Eklenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (TxtId.Text!="")
            {
                int id = Convert.ToInt32(TxtId.Text);
                var gunmarka = db.Markalar.Find(id);
                gunmarka.Marka = TxtAd.Text;
                
                db.SaveChanges();
                MarkaListesi();
             
                MessageBox.Show("Marka Güncelleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki"+gunmarka.Marka+ " Adlı Marka Güncellendi";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
                temizle();
            }
            else
            {
                MessageBox.Show("Güncellemek istediğiniz markayı seçiniz !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki Marka Güncellenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void BtnYeniMarka_Click(object sender, EventArgs e)
        {
            FrmYeniMarka yeni = new FrmYeniMarka();
            yeni.kullaninim = kadi;
            yeni.ShowDialog();
            MarkaListesi();
        }
        void temizle()
        {
            TxtAd.Text = "";
            TxtId.Text = "";
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}

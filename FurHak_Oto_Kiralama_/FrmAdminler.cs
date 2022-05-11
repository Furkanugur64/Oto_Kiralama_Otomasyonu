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
    public partial class FrmAdminler : Form
    {
        public FrmAdminler()
        {
            InitializeComponent();
        }
        public string kadi;
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();
        }

        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            FrmAdminEkle fr = new FrmAdminEkle();
            fr.kullanici = kadi;
            fr.ShowDialog();
            AdminListesi();
        }
        void AdminListesi()
        {
            var deger =( from x in db.Adminler
                        select new
                        {
                            x.Id,
                            x.Ad,
                            x.Soyad,
                            x.Kullaniciadi,
                            x.Email,
                            x.Fotograf,
                        }).ToList();
            dataGridView1.DataSource = deger;
        }
        private void FrmAdminler_Load(object sender, EventArgs e)
        {
            AdminListesi();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Txtkullaniciadi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TxtMail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            TxtFotograf.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            pictureBox1.ImageLocation= dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            Txtkullaniciadi.Text = "";
            TxtMail.Text = "";
            TxtFotograf.Text = "";
            pictureBox1.ImageLocation = null;
        }

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
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text != "" && TxtSoyad.Text != "" && Txtkullaniciadi.Text != "" && TxtMail.Text != "")
            {
                int id = Convert.ToInt16(Txtid.Text);
                var adm = db.Adminler.Find(id);
                adm.Ad = TxtAd.Text;
                adm.Soyad = TxtSoyad.Text;
                adm.Kullaniciadi = Txtkullaniciadi.Text;
                adm.Email = TxtMail.Text;
                adm.Fotograf = TxtFotograf.Text;
                MessageBox.Show("Admin Güncelleme İşlemi Başarıyla Gerçekleştirildi", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki" + adm.Ad + " Adlı Admin Güncellendi.";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
                AdminListesi();
            }
            else
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lg.Islem = "Sistemdeki Admin Güncellenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }
        Loglar lg = new Loglar();
        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (Txtid.Text != "")
            {
                DialogResult sonuc = MessageBox.Show("Admin Bilgisi Silinsin Mi ?", "FurHak Oto Kiralama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt16(Txtid.Text);
                    var sil = db.Adminler.Find(id);
                    db.Adminler.Remove(sil);
                    MessageBox.Show("Admin Silme İşlemi Başarıyla Gerçekleştirildi", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lg.Islem = "Sistemden  Admin Silindi.";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kadi.ToString();
                    db.Loglar.Add(lg);
                    db.SaveChanges();
                    AdminListesi();
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kaydı seçin !", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lg.Islem = "Sistemden Admin Silme İşleminde Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }
    }
}

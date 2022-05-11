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
    public partial class FrmAraclar : Form
    {
        public FrmAraclar()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kadi;

        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();
        }
        void Arac_Listesi()
        {
            var araclar = (from x in db.Araclar
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
        void Temizle()
        {
            Txtid.Text = "";
            TxtPlaka.Text = "";
            TxtMarka.Text = "";
            TxtSeri.Text = "";
            TxtModelYılı.Text = "";
            TxtRenk.Text = "";
            TxtGünlük.Text = "";
            TxtAylık.Text = "";
            TxtYıl.Text = "";
            TxtFoto.Text = "";
            pictureBox1.ImageLocation = null;
        }
        private void FrmAraclar_Load(object sender, EventArgs e)
        {
            Arac_Listesi();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtPlaka.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtMarka.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtSeri.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TxtModelYılı.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            TxtRenk.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            TxtGünlük.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            TxtAylık.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            TxtYıl.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            TxtFoto.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                
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

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (Txtid.Text != "")
            {
                DialogResult sonuc = MessageBox.Show("Araç Bilgisi Silinsin Mi ?", "FurHak Oto Kiralama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt16(Txtid.Text);
                    var sil = db.Araclar.Find(id);
                    db.Araclar.Remove(sil);
                    
                    MessageBox.Show("Araç Silme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lg.Islem = "Sistemden"+sil.Seri+" Adlı Araç Silindi";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kadi.ToString();
                    db.Loglar.Add(lg);
                    db.SaveChanges();
                    Arac_Listesi();
                    Temizle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Silmek İstediğiniz Aracı Seçiniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lg.Islem = "Sistemden Araç Silinirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        

        private void TxtPlakaAra_TextChanged(object sender, EventArgs e)
        {
            TxtRenkAra.Text = "";
            TxtModelAra.Text = "";
            var plakaara = (from x in db.Araclar
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

                            }).Where(x => x.Plaka.Contains(TxtPlakaAra.Text)).ToList();
            dataGridView1.DataSource = plakaara;
        }

        

        private void TxtModelAra_TextChanged(object sender, EventArgs e)
        {
            TxtRenkAra.Text = "";
            TxtPlakaAra.Text = "";
            var modelara = (from x in db.Araclar
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

        private void button2_Click(object sender, EventArgs e)
        {
            TxtModelAra.Text = "";
            TxtPlakaAra.Text = "";
            TxtRenkAra.Text = "";
        }

        private void TxtRenkAra_TextChanged(object sender, EventArgs e)
        {
            TxtPlakaAra.Text = "";
            TxtModelAra.Text = "";
            var renkara = (from x in db.Araclar
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var araclar = (from x in db.Araclar.Where(x => x.Durum == true)
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
            else
            {
                Arac_Listesi();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (Txtid.Text != "")
            {
                FrmAracDetay ardet = new FrmAracDetay();
                ardet.kullanici = kadi;
                ardet.aracid = Convert.ToInt32(Txtid.Text);
                ardet.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen Detayını Görüntülemek İstediğiniz Aracı Seçiniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (Txtid.Text != "")
            {
                int id = Convert.ToInt16(Txtid.Text);
                var guncar = db.Araclar.Find(id);
                guncar.Plaka = TxtPlaka.Text;
                guncar.Yil = TxtModelYılı.Text;
                guncar.Renk = TxtRenk.Text;
                guncar.Foto = TxtFoto.Text;
                guncar.Günlük = Convert.ToInt16(TxtGünlük.Text);
                guncar.Aylik = Convert.ToInt16(TxtAylık.Text);
                guncar.Yillik = Convert.ToInt16(TxtYıl.Text);
                MessageBox.Show("Araç Güncelleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki" + guncar.Seriler.Seri + " Adlı Araç Güncellendi";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
                Arac_Listesi();
            }
            else
            {
                MessageBox.Show("Lütfen Güncellemek İstediğiniz Aracı Seçiniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lg.Islem = "Sistemdeki Aracı Güncellerken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }
        Loglar lg = new Loglar();
        private void BtnAracDetay_Click(object sender, EventArgs e)
        {
            if (Txtid.Text != "")
            {
                FrmAracDetay ardet = new FrmAracDetay();
                ardet.kullanici = kadi;
                ardet.aracid = Convert.ToInt32(Txtid.Text);
                ardet.Show();
                this.Hide();
                lg.Islem = "Araç Detayları Görüntülendi";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Lütfen Detayını Görüntülemek İstediğiniz Aracı Seçiniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lg.Islem = "Araç Detayları Görüntülenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }
    }
}

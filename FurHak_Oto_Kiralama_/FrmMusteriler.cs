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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        
        public string kadi;
        void MusteriListesi()
        {
            var musteriler =(from x in db.Müsteriler select new
            {   x.Id,
                x.Tcno,
                x.Adi,
                x.Soyadi,
                x.Cinsiyet,
                x.Email,
                x.Telefon,
                x.EhliyetNo,
                x.Adres
            }).ToList();
            dataGridView1.DataSource = musteriler;
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            MusteriListesi();
        }

        private void Pictureboxexit_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            
            this.Hide();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtTcno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[4].Value.ToString()=="BAY")
            {
                RadioBay.Checked = true;
            }
            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "BAYAN")
            {
                RadioBayan.Checked = true;
            }
            TxtMail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            MskTelefon.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            TxtEhliyetno.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            
        }
        void Temizle()
        {
            Txtid.Text = "";
            TxtTcno.Text = "";
            TxtAd.Text = "";
            TxtEhliyetno.Text = "";
            TxtMail.Text = "";
            TxtSoyad.Text = "";
            MskTelefon.Clear();
            richTextBox1.Text = "";
            RadioBay.Checked = false;
            RadioBayan.Checked = false;
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        Loglar lg = new Loglar();
        private void Btnkaydet_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text!="" && TxtEhliyetno.Text != "" && TxtMail.Text != "" && TxtSoyad.Text != "" && TxtTcno.Text != ""  && RadioBay.Checked  || RadioBayan.Checked)
            {
                Müsteriler m = new Müsteriler();
                m.Tcno = TxtTcno.Text;
                m.Adi = TxtAd.Text;
                m.Soyadi = TxtSoyad.Text;
                m.Telefon = MskTelefon.Text;
                m.Email = TxtMail.Text;
                m.EhliyetNo = TxtEhliyetno.Text;
                m.Adres = richTextBox1.Text;
                if (RadioBay.Checked)
                {
                    m.Cinsiyet = "BAY";
                }
                if (RadioBayan.Checked)
                {
                    m.Cinsiyet = "BAYAN";
                }
                db.Müsteriler.Add(m);
                
                MessageBox.Show("Müşteri Ekleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lg.Islem = "Sisteme Yeni Müşteri Eklendi";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
                Temizle();
                MusteriListesi();
            }
            else
            {
                MessageBox.Show("Lütfen verileri eksiksiz giriniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lg.Islem = "Sisteme Müşteri Eklenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (Txtid.Text!="")
            {
                DialogResult sonuc = MessageBox.Show("Müşteri Bilgisi Silinsin Mi ?", "FurHak Oto Kiralama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt16(Txtid.Text);
                    var sil = db.Müsteriler.Find(id);
                    db.Müsteriler.Remove(sil);
                    
                    MessageBox.Show("Müşteri Silme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lg.Islem = "Sistemdeki "+sil.Adi+" "+sil.Soyadi+" Adlı Müşteri Silindi";
                    lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                    lg.Yetkili = kadi.ToString();
                    db.Loglar.Add(lg);
                    Temizle();
                    db.SaveChanges();
                    MusteriListesi();
                    
                }
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz müşteriyi seçiniz !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lg.Islem = "Sistemdeki Müşteri Silinirken Hata oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }

        private void BtnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (TxtTcno.Text != "")
            {
                int id = int.Parse(Txtid.Text);
                var mus = db.Müsteriler.Find(id);
                mus.Adi = TxtAd.Text;
                mus.Soyadi = TxtSoyad.Text;
                mus.Telefon = MskTelefon.Text;
                mus.Email = TxtMail.Text;
                mus.EhliyetNo = TxtEhliyetno.Text;
                mus.Adres = richTextBox1.Text;
                if (RadioBay.Checked)
                {
                    mus.Cinsiyet = "BAY";
                }
                if (RadioBayan.Checked)
                {
                    mus.Cinsiyet = "BAYAN";
                }

                MessageBox.Show("Müşteri Güncelleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sistemdeki"+mus.Adi+" "+mus.Soyadi+" Adlı Müşteri Güncellendi";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                Temizle();
                db.SaveChanges();
                MusteriListesi();
            }
            else
            {
                MessageBox.Show("Lütfen Güncellemek İstediğiniz Müşteriyi Seçiniz", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lg.Islem = "Sistemdeki Müşteri Güncellenirken Hata Oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kadi.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }
    }
}

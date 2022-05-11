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
    public partial class FrmYeniMarka : Form
    {
        public FrmYeniMarka()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kullaninim;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        Loglar lg = new Loglar();
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtMarkaadi.Text!="")
            {
                Markalar m = new Markalar();
                m.Marka = TxtMarkaadi.Text;
                db.Markalar.Add(m);
                db.SaveChanges();
                MessageBox.Show("Marka Ekleme İşlemi Başarıyla Gerçekleşti", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lg.Islem = "Sisteme Yeni Marka Eklendi.";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kullaninim.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Marka alanı boş geçilemez !!", "FurHak Oto Kiralama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lg.Islem = "Sisteme Yeni Marka Eklenirken Hata oluştu";
                lg.Zaman = DateTime.Parse(DateTime.Now.ToString());
                lg.Yetkili = kullaninim.ToString();
                db.Loglar.Add(lg);
                db.SaveChanges();
            }
        }
    }
}

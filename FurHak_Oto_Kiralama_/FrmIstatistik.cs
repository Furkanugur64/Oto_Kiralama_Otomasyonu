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
    public partial class FrmIstatistik : Form
    {
        public FrmIstatistik()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kadi;
        private void FrmIstatistik_Load(object sender, EventArgs e)
        {
            int toplamarac = db.Araclar.Count();
            int toplampersonel = db.Adminler.Count();
            int toplammüsteri = db.Müsteriler.Count();
            int toplammarka = db.Markalar.Count();
            int toplamseri = db.Seriler.Count();
            int toplamsozlesme = db.Sozlesmeler.Count();
            var enucuzgün = (from x in db.Araclar
                             orderby x.Günlük
                             select x.Seriler.Seri).First();
            var enucuzay = (from x in db.Araclar
                             orderby x.Aylik
                             select x.Seriler.Seri).First();
            var enucuzyil = (from x in db.Araclar
                            orderby x.Yillik 
                            select x.Seriler.Seri).First();
            var enpahalıgün = (from x in db.Araclar
                             orderby x.Günlük descending
                               select x.Seriler.Seri).First();
            var enpahalıay = (from x in db.Araclar
                            orderby x.Aylik descending
                              select x.Seriler.Seri).First();
            var enpahalıyil = (from x in db.Araclar
                             orderby x.Yillik descending
                               select x.Seriler.Seri).First();

            var aktifarac = db.Araclar.Where(x => x.Durum == true).Count();
            var aktifsözlesme = db.Sozlesmeler.Where(x => x.Durum == true).Count();
            var kasa = db.Sozlesmeler.Sum(x => x.Ucret);

            //enucuz
            LblEnUcuzaracg.Text = enucuzgün.ToString();
            Lblenucuzaraca.Text = enucuzay.ToString();
            Lblenucuzaracyil.Text = enucuzyil.ToString();
            LblEnpahalıgün.Text = enpahalıgün.ToString();
            LblEnpahalıay.Text = enpahalıay.ToString();
            LblEnpahalıyil.Text = enpahalıyil.ToString();
            LblAktifarac.Text = aktifarac.ToString();
            LblAktifSözlesme.Text = aktifsözlesme.ToString();
            LblKasa.Text = kasa + " TL";
            //toplam
            LblToplamarac.Text = toplamarac.ToString();
            LblToplamPersonel.Text = toplampersonel.ToString();
            LblToplamMusteri.Text = toplammüsteri.ToString();
            LblToplamMarka.Text = toplammarka.ToString();
            LblToplamSeri.Text = toplamseri.ToString();
            LblToplamSözlesme.Text = toplamsozlesme.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();
        }
    }
}

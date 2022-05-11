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
    public partial class FrmAracDetay : Form
    {
        public FrmAracDetay()
        {
            InitializeComponent();
        }
        public int aracid;
        public string kullanici;
        OtoKiralamaEntities db = new OtoKiralamaEntities();

        private void FrmAracDetay_Load(object sender, EventArgs e)
        {
            var arac = db.Araclar.Find(aracid);
            LblPlaka.Text = arac.Plaka;
            LblMarka.Text = arac.Markalar.Marka;
            LblSeri.Text = arac.Seriler.Seri;
            LblModelYil.Text = arac.Yil;
            LblMotor.Text = arac.Motor;
            LblKm.Text = arac.KM;
            LblRenk.Text = arac.Renk;
            LblYakıttip.Text = arac.Yakittipi;
            LblVites.Text = arac.Vites;
            LblMotorGücü.Text = arac.Motorgucu;
            LblGünlük.Text = arac.Günlük.ToString();
            LblAylik.Text = arac.Aylik.ToString();
            LblYillik.Text = arac.Yillik.ToString();
            TxtAciklama.Text = arac.Aciklama;
            pictureBox1.ImageLocation = arac.Foto;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmAraclar arac = new FrmAraclar();
            arac.kadi = kullanici;
            arac.Show();
            this.Hide();
        
        }
    }
}

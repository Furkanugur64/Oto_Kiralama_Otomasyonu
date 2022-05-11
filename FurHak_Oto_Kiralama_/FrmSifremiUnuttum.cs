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
    public partial class FrmSifremiUnuttum : Form
    {
        public FrmSifremiUnuttum()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmGirisYap gr = new FrmGirisYap();
            gr.Show();
            this.Close();
        }

        private void FrmSifremiUnuttum_Load(object sender, EventArgs e)
        {

        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        int i = 0;
        bool x = false;
        private void BtnKontrolEt_Click(object sender, EventArgs e)
        {
            if (Txtkullaniciadi.Text != "" && TxtMailadresi.Text != "")
            {

                timer1.Start();


            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ve Mail Adresi Giriniz !!");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            
            progressBar1.Visible = true;
           
            if (i == 1)
            {

                progressBar1.Value = 20;
            }
            if (i == 2)
            {
                progressBar1.Value = 40;
            }
            if (i == 3)
            {
                progressBar1.Value = 60;
            }
            if (i == 4)
            {
                progressBar1.Value = 80;

            }
            if (i == 5)
            {
                progressBar1.Value = 100;
            }
            if (i == 6)
            {
                x = true;
                i = 0;
                timer1.Stop();
                progressBar1.Visible = false;
                
                var deger = db.Adminler.FirstOrDefault(x => x.Kullaniciadi == Txtkullaniciadi.Text && x.Email == TxtMailadresi.Text);
                if (deger != null)
                {
                    MessageBox.Show(deger.Kullaniciadi + " adlı kullanıcının şifresi: " + deger.Sifre);
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı Veya Mail Adresi Hatalı !!");
                }
            }
        }
    }
}

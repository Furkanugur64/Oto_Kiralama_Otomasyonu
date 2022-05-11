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
    public partial class FrmLoglar : Form
    {
        public FrmLoglar()
        {
            InitializeComponent();
        }
        OtoKiralamaEntities db = new OtoKiralamaEntities();
        public string kadi;
        void Log_Listesi()
        {
            var logg = db.Loglar.OrderByDescending(x => x.Id).ToList();
            dataGridView1.DataSource = logg;
        }
        private void FrmLoglar_Load(object sender, EventArgs e)
        {
            Log_Listesi();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmAnasayfa ana = new FrmAnasayfa();
            ana.kullanıcıadi = kadi;
            ana.Show();
            this.Hide();
        }

        private void TxtIslemAra_TextChanged(object sender, EventArgs e)
        {
            TxtAdminAra.Text = "";
            var logg = db.Loglar.Where(x => x.Islem.Contains(TxtIslemAra.Text)).OrderByDescending(x => x.Id).ToList();
            dataGridView1.DataSource = logg;
        }

        private void TxtAdminAra_TextChanged(object sender, EventArgs e)
        {
            TxtIslemAra.Text = "";
            var logg = db.Loglar.Where(x => x.Yetkili.Contains(TxtAdminAra.Text)).OrderByDescending(x => x.Id).ToList();
            dataGridView1.DataSource = logg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TxtIslemAra.Text = "";
            TxtAdminAra.Text = "";
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

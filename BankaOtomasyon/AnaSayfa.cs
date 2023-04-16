using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }
        Banka banka = new Banka();
        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            GirisSayfa formGiris = new GirisSayfa(banka);
            formGiris.TopLevel = false;
            panel1.Controls.Add(formGiris);
            formGiris.Show();
            formGiris.Dock = DockStyle.Fill;
            string Baslangıc=DateTime.Now.ToString();
            BankaOzellikler b = new BankaOzellikler(Baslangıc);
        }
    }
}

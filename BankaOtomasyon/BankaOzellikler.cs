using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    public class BankaOzellikler
    {
        public static string BankaAdı= "CRAZY PROJECT BANKASI";
        public static string ProgramınAcılıs;
        public BankaOzellikler()
        {}
       public BankaOzellikler(string t)
        {
            ProgramınAcılıs = t;
        }
        public virtual void Bilgi()
        {
            MessageBox.Show("Bankamızı göremediniz");
        }
    }
}

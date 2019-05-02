using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAM_Yontemi
{
    public partial class Form1 : Form
    {
        VAM yontem;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            int satir, sutun;
            if (int.TryParse(txtSatir.Text, out satir) && int.TryParse(txtSutun.Text, out sutun))
            {
                if (satir > 0 && sutun > 0)
                {
                    Matris mtr = new Matris(int.Parse(txtSatir.Text), int.Parse(txtSutun.Text));
                    mtr.ShowDialog();
                }
                else
                    MessageBox.Show("Sıfırdan büyük olmak zorundalar");
            }
            else
                MessageBox.Show("Tamsayı değer girmelisiniz");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        internal void SetMatrix(int[,] matrisDeger, int[] arrTalep, int[] arrArz)
        {

            Console.WriteLine("Gonderdi");
            yontem = new VAM(matrisDeger, arrTalep, arrArz);
            btnGorsel.Enabled = true;
            btnHesapla.Enabled = true;
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            yontem.Hesapla();
            MessageBox.Show(yontem.sHesapla());
        }

        private void btnGorsel_Click(object sender, EventArgs e)
        {
            Gorsel_Cozum grsl = new Gorsel_Cozum(yontem);
            grsl.ShowDialog();

        }
    }
}

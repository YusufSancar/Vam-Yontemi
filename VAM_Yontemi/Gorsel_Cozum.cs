using System;
using System.Collections;
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
    public partial class Gorsel_Cozum : Form
    {
        VAM yontem;
        public Gorsel_Cozum()
        {
            InitializeComponent();
        }
        
        public Gorsel_Cozum(VAM yontem)
        {

            this.yontem = yontem;
            ArrayList aciklama, arzlar, talepler, cezalar, atamaIndexler, atamaDegerler;
            int[,] arrMatris = yontem.getMatris();
            int matrisSatir = arrMatris.GetLength(0);
            int matrisSutun = arrMatris.GetLength(1);

            yontem.Hesapla(out aciklama, out arzlar, out talepler, out cezalar, out atamaIndexler, out atamaDegerler);

            InitializeComponent();
            int x = arzlar.Count;
            TextBox[,,] txtMatrisGoster = new TextBox[arzlar.Count, matrisSatir, matrisSutun];
            Label[,] lblArzGoster = new Label[arzlar.Count, matrisSatir];
            Label[,] lblTalepGoster = new Label[arzlar.Count, matrisSutun];

            TextBox[,] txtArzGoster = new TextBox[arzlar.Count, matrisSatir];
            TextBox[,] txtTalepGoster = new TextBox[arzlar.Count, matrisSutun];

            TextBox[,] txtSutunCezalari = new TextBox[arzlar.Count, matrisSutun];
            TextBox[,] txtSatirCezalari = new TextBox[arzlar.Count, matrisSatir];

            Label[] lblKalanArzGoster = new Label[arzlar.Count];
            Label[] lblKalanTalepGoster = new Label[arzlar.Count];

            Label[] lblSatirCezalariGoster = new Label[arzlar.Count];
            Label[] lblSutunCezalariGoster = new Label[arzlar.Count];

            RichTextBox[] txtAcikla = new RichTextBox[aciklama.Count];
            int yatayMesafe = 60;
            int yatayMesafeLbl = 30;
            int dikeyMesafe = 35;
            int dikeyMesafeLbl = 15;
            int aradakiBosluk = 350;
            int aciklamaMesafe = dikeyMesafe * matrisSatir + 30 * (matrisSatir - 1);
            for (int i = 0; i < arzlar.Count; i++)
            {
                for (int j = 0; j < matrisSatir; j++)
                {
                    for (int k = 0; k < matrisSutun; k++)
                    {
                        bool underline = (arzlar[i] as int[])[j] == 0 || (talepler[i] as int[])[k] == 0 ? true : false;

                        txtMatrisGoster[i, j, k] = new TextBox
                        {
                            Size = new Size(40, 30),
                            Location = new Point(yatayMesafe + 45 * k, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * j),
                            ReadOnly = true,
                            Enabled = false,
                            Text = arrMatris[j, k].ToString(),
                            Font = new Font(Font, FontStyle.Bold),
                            TabStop = false
                            
                        };
                        if (underline)
                            txtMatrisGoster[i, j, k].Font = new Font(Font, FontStyle.Strikeout);
                        Console.WriteLine("Arama Sonucu: " + ArraylistAra(atamaIndexler, new int[] { j, k }));
                        if (ArraylistAra(atamaIndexler, new int[] { j, k }) < i && ArraylistAra(atamaIndexler, new int[] { j, k }) != -1)
                        {
                            int dx = ArraylistAra(atamaIndexler, new int[] { j, k });
                            txtMatrisGoster[i, j, k].Text += "("+atamaDegerler[dx]+")";
                        }
                        this.panel1.Controls.Add(txtMatrisGoster[i, j, k]);

                        if (j == 0)
                        {
                            lblTalepGoster[i, k] = new Label
                            {
                                Size = new Size(30, 20),
                                Text = "D" + (k + 1),
                                Location = new Point(yatayMesafe + 45 * k, aradakiBosluk * i + dikeyMesafeLbl + dikeyMesafeLbl * j)
                            };
                            this.panel1.Controls.Add(lblTalepGoster[i, k]);

                            txtTalepGoster[i, k] = new TextBox
                            {
                                Size = new Size(30, 30),
                                Location = new Point(yatayMesafe + 45 * k, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * matrisSatir),
                                ReadOnly = true,
                                Text = (talepler[i] as int[])[k].ToString(),
                                TabStop = false
                            };
                            this.panel1.Controls.Add(txtTalepGoster[i, k]);

                            txtSutunCezalari[i, k] = new TextBox
                            {
                                Size = new Size(30, 30),
                                Location = new Point(yatayMesafe + 45 * k, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * (matrisSatir + 1)),
                                ReadOnly = true,
                                Text = (cezalar[i] as int[])[matrisSatir + k].ToString(),
                                TabStop = false
                            };
                            if ((talepler[i] as int[])[k] == 0)
                            {
                                txtSutunCezalari[i, k].Text = "--";
                                txtSutunCezalari[i, k].TextAlign = HorizontalAlignment.Center;
                            }

                            this.panel1.Controls.Add(txtSutunCezalari[i, k]);
                        }



                    }
                    lblArzGoster[i, j] = new Label
                    {
                        Size = new Size(20, 22),
                        Text = "S" + (j + 1),
                        Location = new Point(yatayMesafeLbl, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * j + 2)
                    };
                    this.panel1.Controls.Add(lblArzGoster[i, j]);


                    txtArzGoster[i, j] = new TextBox
                    {
                        Size = new Size(30, 30),
                        Location = new Point(yatayMesafe + 45 * matrisSutun, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * j),
                        Text = (arzlar[i] as int[])[j].ToString(),
                        ReadOnly = true,
                        TabStop = false
                    };
                    this.panel1.Controls.Add(txtArzGoster[i, j]);

                    
                    txtSatirCezalari[i, j] = new TextBox
                    {
                        Size = new Size(30, 30),
                        Location = new Point(yatayMesafe + 45 * (matrisSutun + 1), aradakiBosluk * i + dikeyMesafe + dikeyMesafe * j),
                        Text = (cezalar[i] as int[])[j].ToString(),
                        ReadOnly = true,
                        TabStop = false
                    };
                    if ((arzlar[i] as int[])[j] == 0)
                    {
                        txtSatirCezalari[i, j].Text = "--";
                        txtSatirCezalari[i, j].TextAlign = HorizontalAlignment.Center;
                    }
                    this.panel1.Controls.Add(txtSatirCezalari[i, j]);


                }


                lblKalanTalepGoster[i] = new Label
                {
                    Size = new Size(40, 20),
                    Text = "Talep",
                    Location = new Point(yatayMesafeLbl - 10, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * matrisSatir + 2)
                };
                this.panel1.Controls.Add(lblKalanTalepGoster[i]);

                lblSutunCezalariGoster[i] = new Label
                {
                    Size = new Size(50, 60),
                    Text = "Sütun \nCezaları",
                    Location = new Point(yatayMesafeLbl - 20, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * (matrisSatir + 1))
                };
                this.panel1.Controls.Add(lblSutunCezalariGoster[i]);

                lblKalanArzGoster[i] = new Label
                {
                    Size = new Size(30, 20),
                    Text = "Arz",
                    Location = new Point(yatayMesafe + 45 * matrisSutun, aradakiBosluk * i + dikeyMesafeLbl)
                };
                this.panel1.Controls.Add(lblKalanArzGoster[i]);

                lblSatirCezalariGoster[i] = new Label
                {
                    Size = new Size(50, 60),
                    Text = "Satır \nCezaları",
                    Location = new Point(yatayMesafe + 45 * (matrisSutun + 1), aradakiBosluk * i + dikeyMesafeLbl - 10)
                };
                this.panel1.Controls.Add(lblSatirCezalariGoster[i]);

                txtAcikla[i] = new RichTextBox();
                txtAcikla[i].Multiline = true;
                txtAcikla[i].Size = new Size(600, 17 * aciklama[i].ToString().Count(c => c == '\n') <= 17 ? 40 : 17 * aciklama[i].ToString().Count(c => c == '\n'));
                txtAcikla[i].Location = new Point(yatayMesafe, aradakiBosluk * i + dikeyMesafe + dikeyMesafe * (matrisSatir + 2));
                txtAcikla[i].Text = aciklama[i].ToString();
                txtAcikla[i].ReadOnly = true;
                txtAcikla[i].TabStop = false;
                this.panel1.Controls.Add(txtAcikla[i]);
            }

            

        }
        private int ArraylistAra(ArrayList arr, int[] dizi)
        {
            int index = -1;

            for (int i = 0; i < arr.Count; i++)
            {
                int[] temp = arr[i] as int[];
                if (temp[0] == dizi[0] && temp[1] == dizi[1])
                    return i;
            }

            return index;
        }
        private void Gorsel_Cozum_Load(object sender, EventArgs e)
        {
            
        }
    }
}

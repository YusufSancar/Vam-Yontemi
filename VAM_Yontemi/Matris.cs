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
    public partial class Matris : Form
    {
        private int satir, sutun;
        private TextBox[,] txtMatrisler;
        private TextBox[] txtTalep, txtArz;
        private Label[,] lblMatris;
        private Button btnOk;
        private int[,] matrisDeger;
        private int[] arrArz, arrTalep;
        public Matris()
        {
            InitializeComponent();
        }

        public Matris(int v1, int v2)
        {
            this.satir = v1;
            this.sutun = v2;
            InitializeComponent();

        }

        private void Matris_Load(object sender, EventArgs e)
        {
            //matrisDeger = new int[satir, sutun];
            txtMatrisler = new TextBox[satir, sutun];
            lblMatris = new Label[satir, sutun];

            Label[] lblArzGoster = new Label[satir];
            Label[] lblTalepGoster = new Label[sutun];

            Label lblToplamArz, lblToplamTalep;
            

            int tabIndex = 0;
            for(int i = 0; i < satir; i++)
            {
               /* int j = 0;
                lblMatris[i, j] = new Label();
                lblMatris[i, j].Size = new Size(20, 20);
                lblMatris[i, j].Text = "A" + (i + 1);
                lblMatris[i, j].Location = new Point(20 + 35 * j, 37 + 35 * i);
                this.panel1.Controls.Add(lblMatris[i, j]);*/

                for(int j = 0; j < sutun; j++)
                {
                    txtMatrisler[i, j] = new TextBox
                    {
                        Size = new Size(30, 30),
                        Location = new Point(50 + 35 * j, 35 + 35 * i),
                        TabIndex = tabIndex
                    };
                    tabIndex++;
                    this.panel1.Controls.Add(txtMatrisler[i, j]);

                    if(i == 0)
                    {
                        lblTalepGoster[j] = new Label
                        {
                            Size = new Size(30, 20),
                            Text = "D" + (j + 1),
                            Location = new Point(50 + 35 * j, 10 + 35 * i)
                        };
                        this.panel1.Controls.Add(lblTalepGoster[j]);
                    }
                }
                lblArzGoster[i] = new Label
                {
                    Size = new Size(20, 22),
                    Text = "S" + (i + 1),
                    Location = new Point(20, 37 + 35 * i)
                };
                this.panel1.Controls.Add(lblArzGoster[i]);
            }

            txtArz = new TextBox[sutun];
            for (int i = 0; i < sutun; i++)
            {
                txtArz[i] = new TextBox
                {
                    Size = new Size(30, 30),
                    Location = new Point(50 + 35 * i, 40 * (satir + 1))
                };
                this.panel1.Controls.Add(txtArz[i]);
            }
            
            txtTalep = new TextBox[satir];
            for (int i = 0; i < satir; i++)
            {
                txtTalep[i] = new TextBox
                {
                    Size = new Size(30, 30),
                    Location = new Point(50 + 35 * (sutun + 1), 35 + 35 * i)
                };
                this.panel1.Controls.Add(txtTalep[i]);
            }


            lblToplamTalep = new Label
            {
                Size = new Size(40, 30),
                Text = "Talep",
                Location = new Point(17, 42 + 40 * (satir))
            };
            this.panel1.Controls.Add(lblToplamTalep);

            lblToplamArz = new Label
            {
                Size = new Size(40, 30),
                Text = "Arz",
                Location = new Point(50 + 35 * (sutun + 1), 10)
            };
            this.panel1.Controls.Add(lblToplamArz);

            btnOk = new Button
            {
                Size = new Size(100, 30),
                Text = "Matrisi Gönder",
                Location = new Point(200 + (35 * (sutun + 1)) / 2, 35 + (35 * (satir + 1) / 2))
            };
            btnOk.Click += BtnOk_Click;
            this.panel1.Controls.Add(btnOk);

        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            matrisDeger = new int[satir, sutun];
            for (int i = 0; i < satir; i++)
                for (int j = 0; j < sutun; j++)
                {
                    if (!int.TryParse(txtMatrisler[i, j].Text, out matrisDeger[i, j]))
                    {
                        MessageBox.Show("Değer boş veya tamsayı girilmemiş!");
                        return;
                    }
                    else if(matrisDeger[i, j] < 0)
                    {
                        MessageBox.Show("Negatif değer giremezsiniz!");
                        return;
                    }
                }

            arrArz = new int[sutun];
            for (int i = 0; i < sutun; i++)
                if(!int.TryParse(txtArz[i].Text, out arrArz[i] ))
                {
                    MessageBox.Show("Değer boş veya tamsayı girilmemiş!");
                    return;
                }
                else if(arrArz[i] < 0)
                {
                    MessageBox.Show("Negatif değer giremezsiniz!");
                    return;
                }

            arrTalep = new int[satir];
            for (int i = 0; i < satir; i++)
                if (!int.TryParse(txtTalep[i].Text, out arrTalep[i]))
                {
                    MessageBox.Show("Değer boş veya tamsayı girilmemiş!");
                    return;
                }
                else if (arrTalep[i] < 0)
                {
                    MessageBox.Show("Negatif değer giremezsiniz!");
                    return;
                }

            Form1 xd = Application.OpenForms[0] as Form1;
            xd.SetMatrix(matrisDeger, arrTalep, arrArz);

            this.Dispose();
        }
    }
}

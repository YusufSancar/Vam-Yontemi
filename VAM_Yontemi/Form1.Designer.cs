namespace VAM_Yontemi
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSatir = new System.Windows.Forms.TextBox();
            this.txtSutun = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOlustur = new System.Windows.Forms.Button();
            this.btnHesapla = new System.Windows.Forms.Button();
            this.btnGorsel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSatir
            // 
            this.txtSatir.Location = new System.Drawing.Point(62, 24);
            this.txtSatir.Name = "txtSatir";
            this.txtSatir.Size = new System.Drawing.Size(39, 20);
            this.txtSatir.TabIndex = 0;
            // 
            // txtSutun
            // 
            this.txtSutun.Location = new System.Drawing.Point(62, 63);
            this.txtSutun.Name = "txtSutun";
            this.txtSutun.Size = new System.Drawing.Size(39, 20);
            this.txtSutun.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Satır:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sütun:";
            // 
            // btnOlustur
            // 
            this.btnOlustur.Location = new System.Drawing.Point(140, 21);
            this.btnOlustur.Name = "btnOlustur";
            this.btnOlustur.Size = new System.Drawing.Size(104, 23);
            this.btnOlustur.TabIndex = 2;
            this.btnOlustur.Text = "Matris Oluştur";
            this.btnOlustur.UseVisualStyleBackColor = true;
            this.btnOlustur.Click += new System.EventHandler(this.btnOlustur_Click);
            // 
            // btnHesapla
            // 
            this.btnHesapla.Enabled = false;
            this.btnHesapla.Location = new System.Drawing.Point(140, 60);
            this.btnHesapla.Name = "btnHesapla";
            this.btnHesapla.Size = new System.Drawing.Size(104, 23);
            this.btnHesapla.TabIndex = 3;
            this.btnHesapla.Text = "Sonucu Göster";
            this.btnHesapla.UseVisualStyleBackColor = true;
            this.btnHesapla.Click += new System.EventHandler(this.btnHesapla_Click);
            // 
            // btnGorsel
            // 
            this.btnGorsel.Enabled = false;
            this.btnGorsel.Location = new System.Drawing.Point(140, 102);
            this.btnGorsel.Name = "btnGorsel";
            this.btnGorsel.Size = new System.Drawing.Size(104, 23);
            this.btnGorsel.TabIndex = 4;
            this.btnGorsel.Text = "Görselleştir";
            this.btnGorsel.UseVisualStyleBackColor = true;
            this.btnGorsel.Click += new System.EventHandler(this.btnGorsel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 137);
            this.Controls.Add(this.btnGorsel);
            this.Controls.Add(this.btnHesapla);
            this.Controls.Add(this.btnOlustur);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSutun);
            this.Controls.Add(this.txtSatir);
            this.Name = "Form1";
            this.Text = "VAM Yöntemi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSatir;
        private System.Windows.Forms.TextBox txtSutun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOlustur;
        private System.Windows.Forms.Button btnHesapla;
        private System.Windows.Forms.Button btnGorsel;
    }
}


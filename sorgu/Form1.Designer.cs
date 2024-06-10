namespace FormUygulamasi
{
    partial class AnaForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnAdSoyad = new System.Windows.Forms.Button();
            this.btnTcGsm = new System.Windows.Forms.Button();
            this.btnAile = new System.Windows.Forms.Button();
            this.btnTanidiklar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdSoyad
            // 
            this.btnAdSoyad.Location = new System.Drawing.Point(50, 50);
            this.btnAdSoyad.Name = "btnAdSoyad";
            this.btnAdSoyad.Size = new System.Drawing.Size(100, 50);
            this.btnAdSoyad.TabIndex = 0;
            this.btnAdSoyad.Text = "Ad Soyad";
            this.btnAdSoyad.Click += new System.EventHandler(this.btnAdSoyad_Click);
            // 
            // btnTcGsm
            // 
            this.btnTcGsm.Location = new System.Drawing.Point(200, 50);
            this.btnTcGsm.Name = "btnTcGsm";
            this.btnTcGsm.Size = new System.Drawing.Size(100, 50);
            this.btnTcGsm.TabIndex = 1;
            this.btnTcGsm.Text = "TC GSM";
            this.btnTcGsm.Click += new System.EventHandler(this.btnTcGsm_Click);
            // 
            // btnAile
            // 
            this.btnAile.Location = new System.Drawing.Point(50, 150);
            this.btnAile.Name = "btnAile";
            this.btnAile.Size = new System.Drawing.Size(100, 50);
            this.btnAile.TabIndex = 2;
            this.btnAile.Text = "Aile";
            this.btnAile.Click += new System.EventHandler(this.btnAile_Click);
            // 
            // btnTanidiklar
            // 
            this.btnTanidiklar.Location = new System.Drawing.Point(200, 150);
            this.btnTanidiklar.Name = "btnTanidiklar";
            this.btnTanidiklar.Size = new System.Drawing.Size(100, 50);
            this.btnTanidiklar.TabIndex = 3;
            this.btnTanidiklar.Text = "Tanidiklar";
            this.btnTanidiklar.Click += new System.EventHandler(this.btnTanidiklar_Click);
            // 
            // AnaForm
            // 
            this.ClientSize = new System.Drawing.Size(354, 262);
            this.Controls.Add(this.btnTanidiklar);
            this.Controls.Add(this.btnAdSoyad);
            this.Controls.Add(this.btnTcGsm);
            this.Controls.Add(this.btnAile);
            this.Name = "AnaForm";
            this.Text = "Ana Form";
            this.Load += new System.EventHandler(this.AnaForm_Load);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnAdSoyad;
        private System.Windows.Forms.Button btnTcGsm;
        private System.Windows.Forms.Button btnAile;
        private System.Windows.Forms.Button btnTanidiklar;
    }
}

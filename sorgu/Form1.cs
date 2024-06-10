using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TanidiklarFormNamespace;

namespace FormUygulamasi
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void btnAdSoyad_Click(object sender, EventArgs e)
        {
            AdSoyadForm adSoyadForm = new AdSoyadForm();
            adSoyadForm.Show();
        }

        private void btnTcGsm_Click(object sender, EventArgs e)
        {
            TcGsmForm tcGsmForm = new TcGsmForm();
            tcGsmForm.Show();
        }

        private void btnAile_Click(object sender, EventArgs e)
        {
            AileForm aileForm = new AileForm();
            aileForm.Show();
        }

        private void btnTanidiklar_Click(object sender, EventArgs e)
        {
            TanidiklarForm tanidiklarForm = new TanidiklarForm();
            tanidiklarForm.Show();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            // Formun arka plan rengini değiştir
            this.BackColor = Color.FromArgb(37, 37, 38);

            // Butonların arka plan rengini ve metin rengini değiştir
            foreach (Button button in this.Controls.OfType<Button>())
            {
                button.BackColor = Color.FromArgb(64, 64, 64);
                button.ForeColor = Color.White;
            }
        }
    }
}

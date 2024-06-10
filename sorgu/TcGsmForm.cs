using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FormUygulamasi
{
    public partial class TcGsmForm : Form
    {
        public TcGsmForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DataGridView için sütunları oluştur
            dataGridView1.Columns.Add("Column1", "TC");
            dataGridView1.Columns.Add("Column2", "GSM");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox'dan gelen değerleri al
            var tcgsm = textBox1.Text;

            // Veritabanı bağlantı dizesi
            string connectionString = "Server=localhost;Database=illegalplatform_hackerdede1_gsm;Uid=root;Pwd=;";

            // SQL sorgusu
            string query = $"SELECT * FROM illegalplatform_hackerdede1_gsm WHERE TC='{tcgsm}' OR GSM='{tcgsm}'";

            // MySqlConnection nesnesi oluşturulması
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Bağlantı açılır
                    connection.Open();

                    // MySqlCommand nesnesi oluşturulması
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // MySqlDataReader nesnesi ile sorgunun çalıştırılması
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int resultCount = 0;

                            // Sonuçları DataGridView'e ekle
                            dataGridView1.Rows.Clear();
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader["TC"], reader["GSM"]);
                                resultCount++;
                            }

                            // Show the result count
                            MessageBox.Show($"Bulunan Kişi Sayısı: {resultCount}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}

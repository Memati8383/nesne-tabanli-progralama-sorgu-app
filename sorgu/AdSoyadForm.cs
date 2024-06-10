using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FormUygulamasi
{
    public partial class AdSoyadForm : Form
    {
        public AdSoyadForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(AdSoyadForm_Load); // Ensure the Load event is assigned
        }

        private void AdSoyadForm_Load(object sender, EventArgs e)
        {
            // DataGridView için sütunları oluştur
            dataGridView1.Columns.Add("Column1", "ID");
            dataGridView1.Columns.Add("Column2", "TC");
            dataGridView1.Columns.Add("Column3", "ADI");
            dataGridView1.Columns.Add("Column4", "SOYADI");
            dataGridView1.Columns.Add("Column5", "NUFUSIL");
            dataGridView1.Columns.Add("Column6", "NUFUSILCE");
            dataGridView1.Columns.Add("Column7", "BABAADI");
            dataGridView1.Columns.Add("Column8", "BABATC");
            dataGridView1.Columns.Add("Column9", "ANNEADI");
            dataGridView1.Columns.Add("Column10", "ANNETC");
            dataGridView1.Columns.Add("Column11", "UYRUK");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox'lardan gelen değerleri al
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string nufusil = textBox3.Text;
            string nufusilce = textBox4.Text;

            // Veritabanı bağlantı dizesi
            string connectionString = "Server=localhost;Database=101m;Uid=root;Pwd=;";

            // SQL sorgusu
            string query = "SELECT * FROM 101m WHERE 1=1";

            if (!string.IsNullOrEmpty(ad))
            {
                query += " AND ADI=@ad";
            }

            if (!string.IsNullOrEmpty(soyad))
            {
                query += " AND SOYADI=@soyad";
            }

            if (!string.IsNullOrEmpty(nufusil))
            {
                query += " AND NUFUSIL=@nufusil";
            }

            if (!string.IsNullOrEmpty(nufusilce))
            {
                query += " AND NUFUSILCE=@nufusilce";
            }

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
                        // Parametreleri ekle
                        if (!string.IsNullOrEmpty(ad))
                        {
                            command.Parameters.AddWithValue("@ad", ad);
                        }
                        if (!string.IsNullOrEmpty(soyad))
                        {
                            command.Parameters.AddWithValue("@soyad", soyad);
                        }
                        if (!string.IsNullOrEmpty(nufusil))
                        {
                            command.Parameters.AddWithValue("@nufusil", nufusil);
                        }
                        if (!string.IsNullOrEmpty(nufusilce))
                        {
                            command.Parameters.AddWithValue("@nufusilce", nufusilce);
                        }

                        // MySqlDataReader nesnesi ile sorgunun çalıştırılması
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            int resultCount = 0;

                            // Sonuçları DataGridView'e ekle
                            dataGridView1.Rows.Clear();
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader["ID"], reader["TC"], reader["ADI"], reader["SOYADI"], reader["NUFUSIL"], reader["NUFUSILCE"], reader["BABAADI"], reader["BABATC"], reader["ANNEADI"], reader["ANNETC"], reader["UYRUK"]);
                                resultCount++;
                            }

                            // Show the result count
                            MessageBox.Show($"Bulunan Kişi Sayısı: {resultCount}");
                        }
                    }
                }
                catch (MySqlException mysqlEx)
                {
                    MessageBox.Show("MySQL Error: " + mysqlEx.Message);
                }
                catch (InvalidOperationException invOpEx)
                {
                    MessageBox.Show("Invalid Operation: " + invOpEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("General Error: " + ex.Message);
                }
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TanidiklarFormNamespace
{
    public partial class TanidiklarForm : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public TanidiklarForm()
        {
            InitializeComponent();
            InitializeDatabase();
            this.Load += new EventHandler(TanidiklarForm_Load);
        }

        private void InitializeDatabase()
        {
            server = "localhost";
            database = "tanidiklar";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        private void TanidiklarForm_Load(object sender, EventArgs e)
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
            dataGridView1.Columns.Add("Column11", "SERINO");
            dataGridView1.Columns.Add("Column12", "AILE SIRA NO");
            dataGridView1.Columns.Add("Column13", "CILT NO");
            dataGridView1.Columns.Add("Column14", "SIRA NO");
            dataGridView1.Columns.Add("Column15", "ORTAOKUL NO");
            dataGridView1.Columns.Add("Column16", "LİSE NO");
            dataGridView1.Columns.Add("Column17", "UYRUK");
            dataGridView1.Columns.Add("Column18", "ADRES");
            dataGridView1.Columns.Add("Column19", "HAYAT HİKAYESİ");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        // TextBox'lardan gelen değerleri al
        string id = textBox1.Text;
        string ad = textBox2.Text;
        string soyad = textBox3.Text;
        string nufusil = textBox4.Text;
        string nufusilce = textBox5.Text;

        // Veritabanı bağlantı dizesi
        string connectionString = "Server=localhost;Database=tanidiklar;Uid=root;Pwd=;";

        // SQL sorgusu
        string query = "SELECT * FROM tanidiklar WHERE 1=1";

        if (!string.IsNullOrEmpty(id))
        {
            query += " AND ID=@id";
        }

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
                    if (!string.IsNullOrEmpty(id))
                    {
                        command.Parameters.AddWithValue("@id", id);
                    }
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
                            dataGridView1.Rows.Add(reader["ID"], reader["TC"], reader["ADI"], reader["SOYADI"], reader["NUFUSIL"], reader["NUFUSILCE"], reader["BABAADI"], reader["BABATC"], reader["ANNEADI"], reader["ANNETC"], reader["SERINO"], reader["AILE SIRA NO"], reader["CILT NO"], reader["SIRA NO"], reader["ORTAOKUL NO"], reader["LİSE NO"], reader["UYRUK"], reader["ADRES"], reader["HAYAT HİKAYESİ"]);
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

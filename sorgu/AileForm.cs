using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FormUygulamasi
{
    public partial class AileForm : Form
    {
        public AileForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            // Clear existing columns
            dataGridView1.Columns.Clear();

            // Define initial columns for DataGridView
            dataGridView1.ColumnCount = 3;

            // Add column headers
            dataGridView1.Columns[0].Name = "Category";
            dataGridView1.Columns[1].Name = "Information";
            dataGridView1.Columns[2].Name = "Gsm1";

            // Set column widths
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 400;
            dataGridView1.Columns[2].Width = 100;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox'lardan gelen değerleri al
            string tc = textBox1.Text;

            // Veritabanı bağlantı dizesi
            string connectionString = "Server=localhost;Database=101m;Uid=root;Pwd=;";

            // Depolanan bilgileri kullanmak için değişkenler
            string mal = "";
            string babamap = "";
            string annemap = "";
            string[] kardesler = null;
            string[] cocuklar = null;

            // İlk bağlantı ve sorgu işlemi
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Bağlantı açılır
                    connection.Open();

                    // SQL sorgusu
                    string query = $"SELECT * FROM 101m WHERE TC='{tc}'";

                    // MySqlCommand nesnesi oluşturulması
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // MySqlDataReader nesnesi ile sorgunun çalıştırılması
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("TC ile eşleşen kayıt bulunamadı.");
                                return;
                            }

                            // Her bir sonuç için işlem yapma
                            while (reader.Read())
                            {
                                // Mal bilgilerini ekleme
                                mal += $"{reader["TC"]} {reader["ADI"]} {reader["SOYADI"]} {reader["DOGUMTARIHI"]} {reader["NUFUSIL"]} {reader["NUFUSILCE"]}\n";

                                // Babamap bilgilerini ekleme
                                string babatc = reader["BABATC"].ToString();
                                babamap = GetFamilyInfo(connectionString, babatc);

                                // Annemap bilgilerini ekleme
                                string annetc = reader["ANNETC"].ToString();
                                annemap = GetFamilyInfo(connectionString, annetc);

                                // Kardeşler bilgilerini ekleme
                                kardesler = GetSiblingInfo(connectionString, annetc, babatc);

                                // Çocuklar bilgilerini ekleme
                                cocuklar = GetChildrenInfo(connectionString, tc);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            // DataGridView'e eklemeler
            dataGridView1.Rows.Clear();
            InitializeDataGridView(); // Initialize the DataGridView columns

            // Mal bilgileri ekleme
            AddDataToGrid("Mal", mal);
            // Baba bilgileri ekleme
            AddDataToGrid("Baba", babamap);
            // Anne bilgileri ekleme
            AddDataToGrid("Anne", annemap);

            // Kardeşler bilgilerini ekleme
            if (kardesler != null)
            {
                foreach (string kardes in kardesler)
                {
                    if (!string.IsNullOrWhiteSpace(kardes))
                    {
                        AddDataToGrid("Kardeş", kardes);
                    }
                }
            }

            // Çocuklar bilgilerini ekleme
            if (cocuklar != null)
            {
                foreach (string cocuk in cocuklar)
                {
                    if (!string.IsNullOrWhiteSpace(cocuk))
                    {
                        AddDataToGrid("Çocuk", cocuk);
                    }
                }
            }
        }

        private string GetFamilyInfo(string connectionString, string tc)
        {
            string info = "";
            if (string.IsNullOrEmpty(tc))
            {
                return info;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM 101m WHERE TC='{tc}'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        info += $"{reader["TC"]} {reader["ADI"]} {reader["SOYADI"]} {reader["DOGUMTARIHI"]} {reader["NUFUSIL"]} {reader["NUFUSILCE"]}\n";
                    }
                }
            }

            return info;
        }

        private string[] GetSiblingInfo(string connectionString, string annetc, string babatc)
        {
            string info = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM 101m WHERE ANNETC='{annetc}' OR BABATC='{babatc}'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        info += $"{reader["TC"]} {reader["ADI"]} {reader["SOYADI"]} {reader["DOGUMTARIHI"]} {reader["NUFUSIL"]} {reader["NUFUSILCE"]}\n";
                    }
                }
            }

            return info.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] GetChildrenInfo(string connectionString, string tc)
        {
            string info = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM 101m WHERE ANNETC='{tc}' OR BABATC='{tc}'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        info += $"{reader["TC"]} {reader["ADI"]} {reader["SOYADI"]} {reader["DOGUMTARIHI"]} {reader["NUFUSIL"]} {reader["NUFUSILCE"]}\n";
                    }
                }
            }

            return info.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void AddDataToGrid(string category, string information)
        {
            string tc = information.Split(' ')[0];
            List<string> gsmNumbers = GetGsm(tc);
            int columnCount = dataGridView1.ColumnCount;

            // Ensure there are enough columns to accommodate all GSM numbers
            for (int i = columnCount; i < 3 + gsmNumbers.Count; i++)
            {
                var newColumn = new DataGridViewTextBoxColumn
                {
                    Name = $"Gsm{i - 2}",
                    HeaderText = $"Gsm{i - 2}",
                    Width = 100
                };
                dataGridView1.Columns.Add(newColumn);
            }

            // Create a new row and populate the columns
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView1);

            row.Cells[0].Value = category;
            row.Cells[1].Value = information;

            for (int i = 0; i < gsmNumbers.Count; i++)
            {
                row.Cells[2 + i].Value = gsmNumbers[i];
            }

            dataGridView1.Rows.Add(row);
        }

        private List<string> GetGsm(string tc)
        {
            List<string> gsmNumbers = new List<string>();

            string connectionString = "Server=localhost;Database=illegalplatform_hackerdede1_gsm;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM illegalplatform_hackerdede1_gsm WHERE TC='{tc}'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gsmNumbers.Add(reader["GSM"].ToString());
                    }
                }
            }

            return gsmNumbers;
        }
    }
}

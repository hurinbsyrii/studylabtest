using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studylabtest
{
    public partial class FormStock : Form
    {
        private SqlConnection connection;

        public FormStock()
        {
            InitializeComponent();
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";

            string query = "SELECT * FROM Stock";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Stock");
                dataGridView1.DataSource = dataSet.Tables["Stock"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";

                string query = "DELETE FROM Stock WHERE StockID = @StockID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StockID", textBox1.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Please enter a StockID to delete.");
            }
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormBook book = new FormBook();
            book.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAuthor author = new FormAuthor();
            author.Show();
            this.Hide();
        }
    }
}

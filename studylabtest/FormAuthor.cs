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
using System.Xml.Linq;

namespace studylabtest
{
    public partial class FormAuthor : Form
    {
        private SqlConnection connection;
        public FormAuthor()
        {
            InitializeComponent();
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        private void FormAuthor_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";

            string query = "SELECT * FROM Author";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Author");
                dataGridView1.DataSource = dataSet.Tables["Author"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                string query = "INSERT INTO Author (AuthorID, Name, BirthYear) VALUES (@AuthorID, @Name, @BirthYear)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorID", textBox1.Text);
                    command.Parameters.AddWithValue("@Name", textBox2.Text);
                    command.Parameters.AddWithValue("@BirthYear", textBox3.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                LoadData();
                ClearInputs();
            }
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormBook book = new FormBook();
            book.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormStock stock = new FormStock();
            stock.Show();
            this.Hide();
        }
    }
}

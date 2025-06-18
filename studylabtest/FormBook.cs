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
    public partial class FormBook : Form
    {
        private SqlConnection connection;
        public FormBook()
        {
            InitializeComponent();
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        private void FormBook_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";

            string query = "SELECT * FROM Book";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Book");
                dataGridView1.DataSource = dataSet.Tables["Book"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hurin\\source\\repos\\studylabtest\\studylabtest\\AdmiralBookstoreDatabase.mdf\";Integrated Security=True";

                string query = "UPDATE Book SET Title = @Title, Publisher = @Publisher, PublishDate = @PublishDate WHERE ISBN_13 = @ISBN_13";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ISBN_13", textBox1.Text);
                    command.Parameters.AddWithValue("@Title", textBox2.Text);
                    command.Parameters.AddWithValue("@Publisher", textBox3.Text);
                    command.Parameters.AddWithValue("@PublishDate", dateTimePicker1.Value);

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
                string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(dateTimePicker1.Text))
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
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAuthor author = new FormAuthor();
            author.Show();
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

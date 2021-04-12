using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsDatabase
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsDatabase.Properties.Settings.CatsAndDogsConnectionString"].ConnectionString;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopularPetsTable();
        }
        private void PopularPetsTable()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PetType", connection))
            {
                DataTable petTable = new DataTable();
                adapter.Fill(petTable);

                listPets.DisplayMember = "PetTypeName";
                listPets.ValueMember = "Id";
                listPets.DataSource = petTable;
            }

        }
        private void PopularPetNames()
        {
            string query = "Select Pet.Name from PetType JOIN Pet on Pet.TypeId = PetType.Id where PetType.Id = @TypeId";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@TypeId", listPets.SelectedValue);
                DataTable petNameTable = new DataTable();
                adapter.Fill(petNameTable);

                listPetNames.DisplayMember = "Name";
                listPetNames.ValueMember = "Id";
                listPetNames.DataSource = petNameTable;
            }
        }

        private void listPets_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            PopularPetNames();
        }
        private void listPetNames_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}

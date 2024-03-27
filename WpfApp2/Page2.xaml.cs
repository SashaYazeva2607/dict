using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class Page2 : Page
    {
        private const string ConnectionString = @"Data Source=bd-kip.fa.ru\sqlexpress;Initial Catalog=testyazeva;User ID=sa;Password=1qaz!QAZ;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Page2()
        {
            InitializeComponent();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO testyazeva.dbo.словарь (Понятие, Определение, Источник) VALUES " +
                                   $"('{TermTextBox.Text}', '{DefinitionTextBox.Text}', '{SourceTextBox.Text}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Понятие успешно добавлено");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении понятия: {ex.Message}");
            }
        }
    }
}

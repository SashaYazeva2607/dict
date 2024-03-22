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
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private const string ConnectionString = @"Data Source=bd-kip.fa.ru\sqlexpress;Initial Catalog=testyazeva;User ID=sa;Password=1qaz!QAZ;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; 
        public Page1()
        {
            InitializeComponent();
            LoadTermsFromDatabase();
        }
        private void LoadTermsFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Запрос для выборки данных из базы данных
                    string query = "SELECT id, Понятие, Определение, Источник FROM testyazeva.dbo.словарь";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        termDataGrid.Items.Clear(); // Очистка DataGrid перед загрузкой новых данных
                        while (reader.Read())
                        {
                            // Загрузка данных в DataGrid
                            termDataGrid.Items.Add(new
                            {
                                id = reader["id"],
                                Понятие = reader["Понятие"],
                                Определение = reader["Определение"],
                                Источник = reader["Источник"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных из базы данных: {ex.Message}");
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (termDataGrid.SelectedItem != null)
            {
                try
                {
                    int selectedTermid = (int)(termDataGrid.SelectedCells[0].Item as dynamic).ID;
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        // Запрос для удаления термина из базы данных
                        string query = $"DELETE FROM testyazeva.dbo.словарь WHERE id = {selectedTermid}";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                            LoadTermsFromDatabase(); // Обновление DataGrid после удаления
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении понятия: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите понятие для удаления");
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
   

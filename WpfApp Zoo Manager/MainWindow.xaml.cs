using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace WpfApp_Zoo_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connectionString = "Server=localhost;Database=udemycsharp;username=root;password=;port=3306";
        public string providerName ="MySql.Data.MySqlClient";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "udemycsharp";
            string username = "root";
            string password = "";
            string port = "3306";
            try
            {
                MySqlConnection conn = new MySqlConnection($"server={server};database={database};username={username};password={password};port={port}");
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Connected Successfully.");
                    MySqlCommand cmd = new MySqlCommand("Select * from zoo", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                    using (adp)
                    {
                        DataTable zooTable = new DataTable();
                        adp.Fill(zooTable);
                        ListZoos.DisplayMemberPath = "Location";
                        ListZoos.SelectedValuePath = "ID";
                        ListZoos.ItemsSource = zooTable.DefaultView;

                    }
                    
                }
                if (conn.State == ConnectionState.Closed)
                {

                    conn.Close();
                    MessageBox.Show("Database not connected.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

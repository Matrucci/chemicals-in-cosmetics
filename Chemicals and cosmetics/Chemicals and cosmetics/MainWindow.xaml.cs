using System;
using System.Collections.Generic;
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

namespace Chemicals_and_cosmetics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "SERVER=localhost;DATABASE=cosmetics;UID=root;PASSWORD=NewPassword;";
            this.connection = new MySqlConnection(connectionString);
            this.connection.Open();
        }

        //Company page.
        private void Continue_Company(object sender, RoutedEventArgs e)
        {
            company_product_page page = new company_product_page(connection);
            this.Content = page;
        }

        //Client page.
        private void Continue_Client(object sender, RoutedEventArgs e)
        {
            client_products page = new client_products(connection);
            this.Content = page;
        }
    }
}

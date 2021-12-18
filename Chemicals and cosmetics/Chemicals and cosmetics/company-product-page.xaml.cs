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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Chemicals_and_cosmetics
{
    /// <summary>
    /// Interaction logic for company_product_page.xaml
    /// </summary>
    public partial class company_product_page : Page
    {
        private MySqlConnection connection;
        public company_product_page(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void backToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void updateProductName_Click(object sender, RoutedEventArgs e)
        {
            int productCode = int.Parse(this.productCodeTextBox.Text);
            string newProductName = this.newNameTextBox.Text;
            //string commandString = "UPDATE product SET product_name = " + newProductName + "WHERE cdph_id = " + productCode;
            string commandString = "UPDATE product SET product_name = @new_name WHERE cdph_id = @code";
            MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
            cmd.Parameters.AddWithValue("@new_name", newProductName);
            cmd.Parameters.AddWithValue("@code", productCode);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            
        }
    }
}

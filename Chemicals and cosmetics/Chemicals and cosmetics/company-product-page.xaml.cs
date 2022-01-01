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

        /**********************************
         * Going back to the main screen.
         *********************************/
        private void backToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }

        /*************************
         * Only allowing numbers.
         *************************/
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /************************************************
         * Updating the name of the product (rebranding).
         ************************************************/
        private void updateProductName_Click(object sender, RoutedEventArgs e)
        {
            //Only allowing updates if both fields are filled.
            if (this.productCodeTextBox.Text != "" && this.newNameTextBox.Text != "")
            {
                int productCode = int.Parse(this.productCodeTextBox.Text);
                string newProductName = this.newNameTextBox.Text;
                string commandString = "UPDATE product SET product_name = @new_name WHERE cdph_id = @code";
                MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
                cmd.Parameters.AddWithValue("@new_name", newProductName);
                cmd.Parameters.AddWithValue("@code", productCode);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
            }
            
        }
    }
}

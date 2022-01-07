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
using System.Collections;

namespace Chemicals_and_cosmetics
{
    /// <summary>
    /// Interaction logic for company_product_page.xaml
    /// </summary>
    public partial class company_product_page : Page
    {
        private MySqlConnection connection;
        private List<int> productCode = new List<int>();
        //private ArrayList productCode = new ArrayList();
        bool isvalidCode = false;
        bool isTypeProdCode = false;
        bool isTypeNewName = false;

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

        /***************************************************************
         * Getting all the products that the company is responsible for.
         ***************************************************************/
        private void companyCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.companyCodeTextBox.Text != "") {
                this.productCode.Clear();
                int companyCode = int.Parse(this.companyCodeTextBox.Text);
                string commandString = "SELECT cdph_id FROM product_companies WHERE company_id = @company_code";
                MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
                cmd.Parameters.AddWithValue("@company_code", companyCode);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    this.productCode.Add(int.Parse(rdr[0].ToString()));
                }
                rdr.Close();
            }

            if (this.companyCodeTextBox.Text != "" && this.productCodeTextBox.Text != "")
            {
                //Checking if the company is responsible for that product.
                foreach (int code in this.productCode)
                {
                    if (code == int.Parse(this.productCodeTextBox.Text))
                    {
                        isvalidCode = true;
                    }
                }
                if (!isvalidCode)
                {
                    this.error.Visibility = Visibility.Visible;
                }
                else
                {
                    this.error.Visibility = Visibility.Hidden;
                    if (this.isTypeNewName)
                        updateProductName.IsEnabled = true;
                }
            }
        }
        
        /***********************************************
         * Checking if the product is under the company.
         **********************************************/
        private void productCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (productCodeTextBox.Text != "")
            {
                foreach (int code in this.productCode)
                {
                    if (code == int.Parse(this.productCodeTextBox.Text))
                    {
                        isvalidCode = true;
                    }
                }
                if (!isvalidCode)
                {
                    this.error.Visibility = Visibility.Visible;
                }
                else
                {
                    this.error.Visibility = Visibility.Hidden;
                    if (this.isTypeNewName)
                        updateProductName.IsEnabled = true;
                }
            }
        }

        private void newNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.isTypeNewName = true;
            if (isvalidCode && this.isTypeNewName)
                updateProductName.IsEnabled = true;
        }
    }
}

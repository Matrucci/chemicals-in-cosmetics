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
        private List<int> productsCode = new List<int>();
        //private ArrayList productCode = new ArrayList();
        bool isValidCode = false;
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

            if (this.companyCodeTextBox.Text != "")
            {
                if (this.isValidCode == true)
                {
                    int productCode = int.Parse(this.productCodeTextBox.Text);
                    string newProductName = this.newNameTextBox.Text;
                    string commandString = "UPDATE product SET product_name = @new_name WHERE cdph_id = @code";
                    MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
                    cmd.Parameters.AddWithValue("@new_name", newProductName);
                    cmd.Parameters.AddWithValue("@code", productCode);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Close();

                    this.successfulUpdate.Visibility = Visibility.Visible;
                }
                else
                {
                    this.error.Visibility = Visibility.Visible;
                    return;
                }
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

                foreach (int code in this.productCode)
                {
                    if (code == int.Parse(this.productCodeTextBox.Text))
                    {
                        isValidCode = true;
                    }
                }
            }

        }
        
        /***********************************************
         * Checking if the product is under the company.
         **********************************************/
        private void productCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void newNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.isTypeNewName = true;
            if (isValidCode && this.isTypeNewName)
                this.updateProductName.IsEnabled = true;
        }

        /******************
        * Resets the page. 
        *******************/
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            this.productCodeTextBox.Text = "";
            this.companyCodeTextBox.Text = "";
            this.newNameTextBox.Text = "";
            this.productCode.Clear();
            this.isTypeNewName = false;
            this.isValidCode = false;
            this.companyCodeTextBox.IsEnabled = false;
            this.updateProductName.IsEnabled = false;
            this.productCodeTextBox.IsEnabled = true;
            this.newNameTextBox.IsEnabled = true;
            this.companyCodeConfirmation.IsEnabled = true;
            this.error.Visibility = Visibility.Hidden;
            this.successfulUpdate.Visibility = Visibility.Hidden;
            this.prodCodeError.Visibility = Visibility.Hidden;
        }

        private void companyCodeConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (this.productCodeTextBox.Text != "" && this.newNameTextBox.Text != "") {

                this.productsCode.Clear();
                string commandString = "SELECT COUNT(cdph_id) FROM product_companies WHERE cdph_id = @prod_code";
                MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
                cmd.Parameters.AddWithValue("@prod_code", this.productCodeTextBox.Text);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    if (int.Parse(rdr[0].ToString()) == 1) {
                        this.prodCodeError.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        this.prodCodeError.Visibility = Visibility.Visible;
                    }
                    //this.productsCode.Add(int.Parse(rdr[0].ToString()));
                }
                rdr.Close();
                if (this.prodCodeError.Visibility == Visibility.Hidden)
                {
                    this.companyCodeTextBox.IsEnabled = true;
                    this.productCodeTextBox.IsEnabled = false;
                    this.newNameTextBox.IsEnabled = false;
                    this.updateProductName.IsEnabled = true;
                    this.companyCodeConfirmation.IsEnabled = false;
                }
               // foreach (int code in this.productCode)
               // {
              //      if (code == int.Parse(this.productCodeTextBox.Text))
              //      {
              //          isValidCode = true;
               //     }
               // }
            }
        }
    }
}

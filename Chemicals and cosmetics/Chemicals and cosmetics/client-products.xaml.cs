using MySql.Data.MySqlClient;
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
using System.Collections;
using System.Data;

namespace Chemicals_and_cosmetics
{
    /// <summary>
    /// Interaction logic for client_products.xaml
    /// </summary>
    public partial class client_products : Page
    {
        private MySqlConnection connection;
        private ArrayList primary = new ArrayList();
        private ArrayList sub = new ArrayList();
        private ArrayList chemicals = new ArrayList();
        public client_products(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            DownloadData();
            InitializeContent();
        }

        private void InitializeContent()
        {
            foreach (string chem in this.chemicals)
            {
                this.chemical_lb.Items.Add(chem);
            }
            foreach (string pc in this.primary)
            {
                this.primary_categoty_cb.Items.Add(pc);
            }
        }

        private void DownloadData()
        {
            string commandString = "SELECT primary_category FROM primary_category";
            MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.primary.Add(rdr[0].ToString());
            }
            rdr.Close();


            commandString = "SELECT DISTINCT chemical_name FROM chemical";
            cmd = new MySqlCommand(commandString, this.connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.chemicals.Add(rdr[0].ToString());
            }
            rdr.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void backToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }

        private void findProductsBtn_Click(object sender, RoutedEventArgs e)
        {

            //Initializing variables
            string primaryCategory, subCategory;
            List<string> chemicalList = new List<string>();
            List<int> productCodes = new List<int>();

            //Get selected itmes
            StringBuilder str = new StringBuilder();
            //str.Append("(");
            foreach (string item in this.chemical_lb.SelectedItems)
            {
                chemicalList.Add(item);
                str.Append("'" + item + "'");
                str.Append(",");
            }
            if (chemicalList.Count() == 0)
            {
                chemicalList.Add("");
            }
            if (str.Length > 0)
                str.Remove(str.Length - 1, 1);
            //str.Append(")");
            string selectedChemicals = str.ToString();
            Console.WriteLine(this.chemical_lb.SelectedItems.ToString());

            primaryCategory = this.primary_categoty_cb.Text;
            subCategory = this.sub_category_cb.Text;

            try
            {
                //Getting product codes
                string commandString = "SELECT DISTINCT cdph_id FROM product_categories " +
                    "WHERE primary_category_id = (SELECT primary_category_id FROM primary_category WHERE primary_category = @primary) " +
                    "AND sub_category_id = (SELECT sub_category_id FROM sub_category WHERE sub_category = @sub)";
                MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
                cmd.Parameters.AddWithValue("@primary", primaryCategory);
                cmd.Parameters.AddWithValue("@sub", subCategory);
                MySqlDataReader rdr = cmd.ExecuteReader();
                StringBuilder includeBuilder = new StringBuilder();

                while (rdr.Read())
                {
                    includeBuilder.Append(rdr[0].ToString());
                    includeBuilder.Append(",");
                }
                if (includeBuilder.Length > 0)
                    includeBuilder.Remove(includeBuilder.Length - 1, 1);
                string include = includeBuilder.ToString();
                rdr.Close();

                //Get products excluding chemicals
                if (include.Length == 0)
                {
                    include = "";
                }
                List<string> includedProducts = include.Split(',').ToList<string>();

                var parameters = new string[chemicalList.Count()];
                var prod = new string[includedProducts.Count()];
                cmd = new MySqlCommand();


                for (int i = 0; i < chemicalList.Count(); i++)
                {
                    parameters[i] = string.Format("@Chemical{0}", i);
                    cmd.Parameters.AddWithValue(parameters[i], chemicalList[i]);
                }

                for (int i = 0; i < prod.Length; i++)
                {
                    prod[i] = string.Format("@Product{0}", i);
                    cmd.Parameters.AddWithValue(prod[i], includedProducts[i]);
                }

                string chemicalString = string.Format("SELECT chemical_id FROM chemical WHERE chemical_name IN ({0})", string.Join(", ", parameters));

                commandString = string.Format("SELECT DISTINCT cdph_id FROM product_chemicals WHERE cdph_id IN ({0}) AND chemical_id IN (" + chemicalString + ")", string.Join(", ", prod));
                cmd.CommandText = commandString;
                cmd.Connection = this.connection;
                rdr = cmd.ExecuteReader();

                List<string> result = new List<string>();
                while (rdr.Read())
                {
                    result.Add(rdr[0].ToString());
                }
                rdr.Close();

                var codes = new string[result.Count()];
                cmd = new MySqlCommand();
                for (int i = 0; i < result.Count(); i++)
                {
                    codes[i] = string.Format("@ProductCode{0}", i);
                    cmd.Parameters.AddWithValue(codes[i], result[i]);
                }
                string resultCommand;
                cmd.Connection = this.connection;
                resultCommand = string.Format("SELECT COUNT(product_name) " +
                    "FROM product_companies " +
                    "JOIN product " +
                    "ON product.cdph_id=product_companies.cdph_id " +
                    "JOIN company " +
                    "ON company.company_id=product_companies.company_id " +
                    "WHERE product.cdph_id IN ({0})", string.Join(", ", codes));
                cmd.CommandText = resultCommand;
                rdr = cmd.ExecuteReader();


                rdr.Read();
                int count = int.Parse(rdr[0].ToString());
                string[,] copyRdr = new string[count, 2];
                rdr.Close();

                cmd.Connection = this.connection;
                resultCommand = string.Format("SELECT product_name, company_name " +
                    "FROM product_companies " +
                    "JOIN product " +
                    "ON product.cdph_id=product_companies.cdph_id " +
                    "JOIN company " +
                    "ON company.company_id=product_companies.company_id " +
                    "WHERE product.cdph_id IN ({0})", string.Join(", ", codes));
                cmd.CommandText = resultCommand;
                rdr = cmd.ExecuteReader();


                int row = 0;

                while (rdr.Read())
                {
                    copyRdr[row, 0] = rdr[0].ToString();
                    copyRdr[row, 1] = rdr[1].ToString();
                    row++;

                }
                rdr.Close();

                client_result resultWindow = new client_result(copyRdr, count, connection);
                resultWindow.Show();
            }
            catch
            {
                MessageBox.Show("No dangerous cosmetics were found. You are good to go!");
                return;
            }

        }

        private void primary_categoty_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.sub_category_cb.IsEnabled = true;

            String commandString = "SELECT sub_category FROM sub_category WHERE sub_category_id IN " +
                "(SELECT sub_category_id FROM product_categories WHERE primary_category_id = " +
                "(SELECT primary_category_id From primary_category Where primary_category = @primary))";
            MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
            cmd.Parameters.AddWithValue("@primary", this.primary_categoty_cb.SelectedItem.ToString());
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.sub.Add(rdr[0].ToString());
            }
            rdr.Close();

            foreach (string sc in this.sub)
            {
                this.sub_category_cb.Items.Add(sc);
            }
        }

        private void sub_category_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.findProductsBtn.IsEnabled = true;
        }
    }
}

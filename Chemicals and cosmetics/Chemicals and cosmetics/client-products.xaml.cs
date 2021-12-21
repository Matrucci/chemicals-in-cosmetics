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
           // foreach (string sc in this.sub)
           // {
           //     this.sub_category_cb.Items.Add(sc);
           // }
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
            if (str.Length > 0)
                str.Remove(str.Length - 1, 1);
            //str.Append(")");
            string selectedChemicals = str.ToString();
            Console.WriteLine(selectedChemicals);
            
            primaryCategory = this.primary_categoty_cb.Text;
            subCategory = this.sub_category_cb.Text;

            //Getting primary id
            string commandString = "SELECT primary_category_id FROM primary_category WHERE primary_category = @primary";
            MySqlCommand cmd = new MySqlCommand(commandString, this.connection);
            cmd.Parameters.AddWithValue("@primary", primaryCategory);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int primaryId = int.Parse(rdr[0].ToString());
            rdr.Close();


            //Getting sub id
            commandString = "SELECT sub_category_id FROM sub_category WHERE sub_category = @sub";
            cmd = new MySqlCommand(commandString, this.connection);
            cmd.Parameters.AddWithValue("@sub", subCategory);
            rdr = cmd.ExecuteReader();
            rdr.Read();
            int subId = int.Parse(rdr[0].ToString());
            rdr.Close();

            //Getting product codes
            commandString = "SELECT cdph_id FROM product_categories WHERE primary_category_id = @primary AND sub_category_id = @sub";
            cmd = new MySqlCommand(commandString, this.connection);
            cmd.Parameters.AddWithValue("@primary", primaryId);
            cmd.Parameters.AddWithValue("@sub", subId);
            rdr = cmd.ExecuteReader();
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
            Console.WriteLine("INCLUDE: " + include);


            //Get chemical id 
            commandString = "SELECT chemical_id FROM chemical WHERE chemical_name IN (@chemicals)";
            cmd = new MySqlCommand(commandString, this.connection);
            cmd.Parameters.AddWithValue("@chemicals", selectedChemicals);
            rdr = cmd.ExecuteReader();
            StringBuilder excludeBuilder = new StringBuilder();

            while (rdr.Read())
            {
                excludeBuilder.Append(rdr[0].ToString());
                excludeBuilder.Append(",");
            } 
            if (excludeBuilder.Length > 0)
                excludeBuilder.Remove(excludeBuilder.Length - 1, 1);
            string exclude = excludeBuilder.ToString();
            rdr.Close();
            Console.WriteLine("EXCLUDE: " + exclude);

            //Get products excluding chemicals
            commandString = "SELECT cdph_id FROM product_chemicals WHERE cdph_id IN (@products) AND chemical_id NOT IN (@chemicals)";
            cmd = new MySqlCommand(commandString, this.connection);
            cmd.Parameters.AddWithValue("@products", include);
            cmd.Parameters.AddWithValue("@chemicals", exclude);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]);
            }
            rdr.Close();


        }

        private void primary_categoty_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sub_category_cb.IsEnabled = true;

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

    }
}

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

namespace Chemicals_and_cosmetics
{
    /// <summary>
    /// Interaction logic for client_products.xaml
    /// </summary>
    public partial class client_products : Page
    {
        private MySqlConnection connection;
        public client_products(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
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

            ComboBox primary = this.primary_categoty_cb;
            ComboBox sub = this.sub_category_cb;
            ListBox chemicals = this.chemical_lb;

            //Initializing variables
            String primaryCategory, subCategory;
            List<String> chemicalList = new List<String>();

            //Get selected itmes
            foreach (ListBoxItem item in chemicals.Items)
            {
                if (item.IsSelected)
                {
                    chemicalList.Add(item.Content.ToString());
                }
            }
            primaryCategory = primary.Text;
            subCategory = sub.Text;


        }
    }
}

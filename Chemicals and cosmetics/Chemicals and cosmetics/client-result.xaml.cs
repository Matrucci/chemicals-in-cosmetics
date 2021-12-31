using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace Chemicals_and_cosmetics
{
    /// <summary>
    /// Interaction logic for client_result.xaml
    /// </summary>
    public partial class client_result : Window
    {
        private DataView dataView;
        private MySqlConnection connection;
        public client_result(string[,] rdr, int count, MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.dataView = createDataView(rdr);
            this.showResult.ItemsSource = dataView;
            this.count_products.Content = count;
        }

        public DataView createDataView(string[,] rdr)
        {

            var t = new DataTable();
            // Add columns with name "0", "1", "2", ...
            var rows = rdr.GetLength(0);
            var columns = rdr.GetLength(1);

            // Add data to DataTable
            t.Columns.Add(new DataColumn("company name"));
            t.Columns.Add(new DataColumn("product name"));
            // Add data to DataTable
            for (var r = 0; r < rows; r++)
            {
                var newRow = t.NewRow();
                for (var c = 0; c < columns; c++)
                {
                    newRow[c] = rdr[r, c];
                }
                t.Rows.Add(newRow);
            }
            return t.DefaultView;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            client_products page = new client_products(connection);
            this.Content = page;
        }
    }
}

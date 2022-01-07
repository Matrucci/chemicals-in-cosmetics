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
            //Parsing the data.
            this.dataView = createDataView(rdr);
            this.showResult.ItemsSource = dataView;
            this.count_products.Content = count;
        }

        /******************************************************
         * Creating the view from a 2D array with the results.
         ******************************************************/
        public DataView createDataView(string[,] rdr)
        {

            var t = new DataTable();
            //Add column names.
            var rows = rdr.GetLength(0);
            var columns = rdr.GetLength(1);

            t.Columns.Add(new DataColumn("company name"));
            t.Columns.Add(new DataColumn("product name"));
            //Add data from the array.
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
            this.Close();
        }
    }
}

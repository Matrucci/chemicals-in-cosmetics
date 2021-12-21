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
using System.Windows.Shapes;

namespace Chemicals_and_cosmetics
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Results : Window
    {
        public Results(MySqlDataReader rdr)
        {
            InitializeComponent();
            this.result.ItemsSource = rdr;
        }
    }
}

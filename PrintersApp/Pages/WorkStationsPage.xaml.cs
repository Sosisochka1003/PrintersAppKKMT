using PrintersApp.Windows;
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

namespace PrintersApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkStationsPage.xaml
    /// </summary>
    public partial class WorkStationsPage : Page
    {
        ContextDataBase ctx;
        public WorkStationsPage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var page = new WorkStation(ctx);
            page.Show();
        }
    }
}

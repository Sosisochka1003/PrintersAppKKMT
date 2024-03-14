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
    /// Логика взаимодействия для PrintersPage.xaml
    /// </summary>
    public partial class PrintersPage : Page
    {
        ContextDataBase ctx;
        public PrintersPage(ContextDataBase ctx)
        {
            this.ctx = ctx;
            InitializeComponent();
        }

        private void ButtonAddElement_Click(object sender, RoutedEventArgs e)
        {
            if (GridAddEditElement.Visibility == Visibility.Hidden)
            {
                GridAddEditElement.Visibility = Visibility.Visible;
            }
            else if (GridAddEditElement.Visibility == Visibility.Visible)
            {
                GridAddEditElement.Visibility = Visibility.Hidden;
            }
        }
    }
}

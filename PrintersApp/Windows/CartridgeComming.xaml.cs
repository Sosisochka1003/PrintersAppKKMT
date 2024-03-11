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
using static PrintersApp.ContextDataBase;

namespace PrintersApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для CartridgeComming.xaml
    /// </summary>
    public partial class CartridgeComming : Window
    {
        ContextDataBase ctx;
        public CartridgeComming(ContextDataBase globalctx)
        {
            InitializeComponent();
            ctx = globalctx;
            ComboBoxCartridge.ItemsSource = ctx.Cartridges.ToList();
        }

        private void ButtonComming_Click(object sender, RoutedEventArgs e)
        {
            DateTime TimeComming = DateTime.SpecifyKind((DateTime)DatePickerDateComming.SelectedDate, DateTimeKind.Utc);
            ctx.Commings.Add(new Comming
            {
                CartridgeId = ((Cartridge)ComboBoxCartridge.SelectedItem).Id,
                CartridgeObject = (Cartridge)ComboBoxCartridge.SelectedItem,
                Count = Convert.ToInt32(TextBoxCountComming.Text),
                CommingDate = TimeComming
            });
            ctx.Cartridges.FirstOrDefault(p => p.Id == ((Cartridge)ComboBoxCartridge.SelectedItem).Id).StockCount += Convert.ToInt32(TextBoxCountComming.Text);
            ctx.SaveChanges();
            this.Close();
        }
    }
}

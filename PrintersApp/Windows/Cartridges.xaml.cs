using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace PrintersApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для Cartridges.xaml
    /// </summary>
    public partial class Cartridges : Window
    {
        //ContextDataBase ctx;
        //List<Cartridge> AllCartridges;
        public Cartridges(ContextDataBase globalctx)
        {
            InitializeComponent();
            //ctx = globalctx;
            //AllCartridges = ctx.Cartridges.ToList();
            //DataGridCartridges.ItemsSource = AllCartridges;
        }

        //public ObservableCollection<Cartridge> FilteredItems { get; set; } = new ObservableCollection<Cartridge>();

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var filterText = ((TextBox)sender).Text.ToLower();

            //var filterList = new List<Cartridge>();

            //filterList.AddRange(ctx.Cartridges.Where(p => p.Name.ToLower().Contains(filterText)));

            //var isNumber = int.TryParse(filterText, out int filterNumber);

            //if (!isNumber)
            //{
            //    //FilteredItems.Clear();
            //    DataGridCartridges.ItemsSource = filterList;

            //}

            //filterList.AddRange(ctx.Cartridges.Where(p => p.StockCount == filterNumber).ToList());
            //FilteredItems.Clear();
            //DataGridCartridges.ItemsSource = filterList;
        }

        private void ButtonAddCartridge_Click(object sender, RoutedEventArgs e)
        {
            //var CartridgeInfoWindow = new CartridgeInfo(ctx);
            //CartridgeInfoWindow.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.F5)
            //{
            //    DataGridCartridges.ItemsSource = ctx.Cartridges.ToList();
            //}
        }


        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult res = MessageBox.Show("Удалить?","Подтверждение удаления",MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (res == MessageBoxResult.No)
            //{
            //    return;
            //}
            
            //ctx.Cartridges.Remove((Cartridge)DataGridCartridges.SelectedItem);
            //ctx.SaveChanges();
            //DataGridCartridges.ItemsSource = ctx.Cartridges.ToList();
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            //var CartridgeInfoWindow = new CartridgeInfo(ctx,((Cartridge)DataGridCartridges.SelectedItem));
            //CartridgeInfoWindow.Show();
        }
    }
}

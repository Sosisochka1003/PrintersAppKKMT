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

namespace PrintersApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для Printers.xaml
    /// </summary>
    public partial class Printers : Window
    {
        ContextDataBase ctx;

        List<FilterItems> test;
        public Printers(ContextDataBase globalctx)
        {
            InitializeComponent();
            ctx = globalctx;

            test = ctx.Printers.Join(ctx.PrinterInRooms,
                p => p.Id,
                pr => pr.PrinterId,
                (p, pr) => new FilterItems
                {
                    Id = p.Id,
                    Name = p.Name,
                    CartridgeName = p.CartridgeObject.Name,
                    Room = pr.Room,
                }).ToList();

           DataGridPrinters.ItemsSource = test;
        }


        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filterText = ((TextBox)sender).Text.ToLower();
            DataGridPrinters.ItemsSource = test.Where(p => p.Name.ToLower().Contains(filterText) ||
                                                p.Room.ToLower().Contains(filterText) ||
                                                p.CartridgeName.ToLower().Contains(filterText));
        }

        public class FilterItems
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CartridgeName { get; set; }
            public string Room { get; set; }
        }

        private void ButtonAddPrinter_Click(object sender, RoutedEventArgs e)
        {
            var PrinterInfoWindow = new PrinterInfo(ctx);
            PrinterInfoWindow.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                DataGridPrinters.ItemsSource = ctx.Printers.Join(ctx.PrinterInRooms,
                p => p.Id,
                pr => pr.PrinterId,
                (p, pr) => new FilterItems
                {
                    Id = p.Id,
                    Name = p.Name,
                    CartridgeName = p.CartridgeObject.Name,
                    Room = pr.Room,
                }).ToList();
            }
        }

        private void MenuItemRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Удалить?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                return;
            }
            var deleteItemId = ctx.Printers.FirstOrDefault(p => p.Id == ((FilterItems)DataGridPrinters.SelectedItem).Id);
            if (deleteItemId == null)
            {
                return;
            }
            ctx.Printers.Remove(deleteItemId);
            ctx.SaveChanges();
            DataGridPrinters.ItemsSource = ctx.Printers.Join(ctx.PrinterInRooms,
                p => p.Id,
                pr => pr.PrinterId,
                (p, pr) => new FilterItems
                {
                    Id = p.Id,
                    Name = p.Name,
                    CartridgeName = p.CartridgeObject.Name,
                    Room = pr.Room,
                }).ToList();
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            var PrinterInfoWindow = new PrinterInfo(ctx, ((FilterItems)DataGridPrinters.SelectedItem));
            PrinterInfoWindow.Show();
        }
    }
}

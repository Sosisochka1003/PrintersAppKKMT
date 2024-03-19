using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static PrintersApp.ContextDataBase;

namespace PrintersApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartridgePage.xaml
    /// </summary>
    public partial class CartridgePage : Page
    {
        ContextDataBase ctx;
        Cartridge? pickCartridge;
        public CartridgePage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            ObservableCollection<Cartridge> allCartridges = new ObservableCollection<Cartridge>(ctx.Cartridges.ToList());
            DataGridCartridges.ItemsSource = allCartridges;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
        }

        private void ButtonShowGrid_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Text = null;
            TextBoxStockCount.Text = null;
            ComboBoxLocation.Text = "Расопложение";
            pickCartridge = null;

            if (GridAddEditElement.Visibility == Visibility.Hidden)
            {
                GridAddEditElement.Visibility = Visibility.Visible;
            }
            else if(GridAddEditElement.Visibility == Visibility.Visible)
            {
                GridAddEditElement.Visibility = Visibility.Hidden;
            }
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCartridges.SelectedItem == null)
            {
                return;
            }
            GridAddEditElement.Visibility = Visibility.Visible;
            pickCartridge = DataGridCartridges.SelectedItem as Cartridge;
            TextBoxName.Text = pickCartridge?.Name;
            TextBoxStockCount.Text = pickCartridge?.StockCount.ToString();
            ComboBoxLocation.SelectedItem = pickCartridge?.Location;
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            ctx.Cartridges.Remove((DataGridCartridges.SelectedItem as Cartridge));
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (pickCartridge == null)
            {
                Cartridge newCartridge = new Cartridge
                {
                    Name = TextBoxName.Text,
                    StockCount = Convert.ToInt32(TextBoxStockCount.Text),
                    Location = (VarLocation)ComboBoxLocation.SelectedItem
                };
                ctx.Cartridges.Add(newCartridge);
                ctx.SaveChanges();
                pickCartridge = null;
                return;
            }
            pickCartridge.Name = TextBoxName.Text;
            pickCartridge.StockCount = Convert.ToInt32(TextBoxStockCount.Text);
            pickCartridge.Location = (VarLocation)ComboBoxLocation.SelectedItem;
            ctx.Cartridges.Update(pickCartridge);
            ctx.SaveChanges();
            pickCartridge = null;
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var paramSearchStr = ((TextBox)sender).Text;
            if (Enum.TryParse<VarLocation>(paramSearchStr, out var paramSearchLocation))
            {
                var result = ctx.Cartridges.Where(c => c.Location == paramSearchLocation).ToList();
                DataGridCartridges.ItemsSource = result;
                return;
            }

            if (!int.TryParse(paramSearchStr, out int paramSearchNumber))
            {
                var result = ctx.Cartridges.Where(c => c.Name.ToLower().Contains(paramSearchStr.ToLower())).ToList();
                DataGridCartridges.ItemsSource = result;
                return;
            }

            var intResult = ctx.Cartridges.Where(c => c.StockCount == paramSearchNumber).ToList();
            DataGridCartridges.ItemsSource = intResult;
        }
    }
}

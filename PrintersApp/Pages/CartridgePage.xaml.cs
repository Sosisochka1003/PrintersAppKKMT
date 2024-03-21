using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public BindingList<Cartridge> Cartridges { get; set; } = new BindingList<Cartridge>();
        
        public CartridgePage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            Cartridges = new BindingList<Cartridge>(ctx.Cartridges.ToList());
            DataGridCartridges.ItemsSource = Cartridges;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            TextBoxStockCount.MaxLength = 2;
        }

        private void ButtonShowGrid_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Text = null;
            TextBoxStockCount.Text = null;
            ComboBoxLocation.Text = "Расположение";
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

        private async void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCartridges.SelectedItem != null)
            {
                var deleteItem = ctx.Cartridges.FirstOrDefault(x => x.Id == (DataGridCartridges.SelectedItem as Cartridge).Id);
                ctx.Cartridges.Remove(deleteItem);
                await ctx.SaveChangesAsync();
                Cartridges.Remove(deleteItem);
            }
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxName.Text == "" || TextBoxStockCount.Text == "" || !Int32.TryParse(TextBoxStockCount.Text, out int a) || ComboBoxLocation.Text == "Расположение")
            {
                MessageBox.Show("Неверное заполнение данных");
                return;
            }
            if (pickCartridge == null)
            {
                var newCartridge = new Cartridge
                {
                    Name = TextBoxName.Text,
                    StockCount = Convert.ToInt32(TextBoxStockCount.Text),
                    Location = (VarLocation)ComboBoxLocation.SelectedItem
                };
                ctx.Cartridges.Add(newCartridge);
                await ctx.SaveChangesAsync();
                Cartridges.Add(newCartridge);
                return;
            }
            pickCartridge.Name = TextBoxName.Text;
            pickCartridge.StockCount = Convert.ToInt32(TextBoxStockCount.Text);
            pickCartridge.Location = (VarLocation)ComboBoxLocation.SelectedItem;
            ctx.Cartridges.Update(pickCartridge);
            await ctx.SaveChangesAsync();
            Cartridges.ResetItem(Cartridges.IndexOf(pickCartridge));
            DataGridCartridges.ItemsSource = Cartridges;
            TextBoxSearch.Text = "Поиск";
            GridAddEditElement.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == "Поиск")
            {
                DataGridCartridges.ItemsSource = Cartridges;
                return;
            }
            var paramSearch = ((TextBox)sender).Text.ToLower();
            var searchCartridges = Cartridges;
            DataGridCartridges.ItemsSource = searchCartridges.Where(c => c.Id.ToString() == paramSearch || 
                                                                    c.Name.ToLower().Contains(paramSearch) || 
                                                                    c.Location.ToString().ToLower().Contains(paramSearch));
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                DataGridCartridges.ItemsSource = Cartridges = new BindingList<Cartridge>(ctx.Cartridges.ToList());
                TextBoxSearch.Text = null;
                MessageBox.Show("Обновлено");
            }
        }
    }
}

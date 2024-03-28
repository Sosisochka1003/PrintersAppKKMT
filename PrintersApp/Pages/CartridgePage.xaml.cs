using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            DataGridCartridges.ItemsSource = Cartridges.OrderBy(x => x.Name);
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            TextBoxStockCount.MaxLength = 2;
        }

        private void ButtonShowGrid_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Text = null;
            TextBoxStockCount.Text = null;
            ComboBoxLocation.Text = "Расположение";
            pickCartridge = null;
            GridCommingCartridges.Visibility = Visibility.Hidden;
            GridShipmentCartridge.Visibility = Visibility.Hidden;

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
            GridCommingCartridges.Visibility = Visibility.Hidden;
            GridShipmentCartridge.Visibility = Visibility.Hidden;
            pickCartridge = DataGridCartridges.SelectedItem as Cartridge;
            TextBoxName.Text = pickCartridge?.Name;
            TextBoxStockCount.Text = pickCartridge?.StockCount.ToString();
            ComboBoxLocation.SelectedItem = pickCartridge?.Location;
        }

        private async void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCartridges.SelectedItem == null)
            {
                return;
            }
            Cartridge deleteItem = ctx.Cartridges.FirstOrDefault(x => x.Id == (DataGridCartridges.SelectedItem as Cartridge).Id);
            ctx.Cartridges.Remove(deleteItem);
            await ctx.SaveChangesAsync();
            Cartridges.Remove(deleteItem);
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxName.Text == "" || TextBoxStockCount.Text == "" || !Int32.TryParse(TextBoxStockCount.Text, out int StockCount) || ComboBoxLocation.Text == "Расположение")
            {
                MessageBox.Show("Неверное заполнение данных");
                return;
            }
            if (pickCartridge == null)
            {
                var newCartridge = new Cartridge
                {
                    Name = TextBoxName.Text,
                    StockCount = StockCount,
                    Location = (VarLocation)ComboBoxLocation.SelectedItem
                };
                ctx.Cartridges.Add(newCartridge);
                await ctx.SaveChangesAsync();
                Cartridges.Add(newCartridge);
                GridAddEditElement.Visibility = Visibility.Hidden;
                return;
            }
            pickCartridge.Name = TextBoxName.Text;
            pickCartridge.StockCount = Convert.ToInt32(TextBoxStockCount.Text);
            pickCartridge.Location = (VarLocation)ComboBoxLocation.SelectedItem;
            ctx.Cartridges.Update(pickCartridge);
            await ctx.SaveChangesAsync();
            GridAddEditElement.Visibility = Visibility.Hidden;
            Cartridges.ResetItem(Cartridges.IndexOf(pickCartridge));
            DataGridCartridges.ItemsSource = Cartridges;
            TextBoxSearch.Text = null;
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == "Поиск")
            {
                DataGridCartridges.ItemsSource = Cartridges;
                return;
            }
            var paramSearch = ((TextBox)sender).Text.ToLower();
            DataGridCartridges.ItemsSource = Cartridges.Where(c => c.Id.ToString() == paramSearch || 
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

        private void ButtonShowShipmentGrid_Click(object sender, RoutedEventArgs e)
        {
            GridAddEditElement.Visibility = Visibility.Hidden;
            GridCommingCartridges.Visibility = Visibility.Hidden;

            TextBoxShipmentRoom.Text = null;
            ComboBoxShipmentLocation.Text = "Расположение";
            ComboBoxShipmentCartridge.Text = "Картридж";
            ComboBoxPrinters.Text = "Принтер";

            ComboBoxShipmentLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();

            //ComboBoxPrinters.ItemsSource = ctx.Printers.ToList();

            ComboBoxShipmentCartridge.ItemsSource = ctx.Cartridges.ToList();


            if (GridShipmentCartridge.Visibility == Visibility.Hidden)
            {
                GridShipmentCartridge.Visibility = Visibility.Visible;
                MessageBox.Show("Порядок заполнения: Кабинет => Расположение => Принтер \nВ другом порядке всё может сломается", "Alert");
            }
            else if (GridShipmentCartridge.Visibility == Visibility.Visible)
            {
                GridShipmentCartridge.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonShowCommingGrid_Click(object sender, RoutedEventArgs e)
        {
            GridAddEditElement.Visibility= Visibility.Hidden;
            GridShipmentCartridge.Visibility= Visibility.Hidden;
            TextBoxCommingCount.Text = null;
            ComboBoxCommingCartrideLocations.Text = "Расположение";
            ComboBoxCommingCartridges.Text = "Картридж";
            ComboBoxCommingCartrideLocations.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();

            DatePickerDateComming.Text = DateOnly.FromDateTime(DateTime.Now).ToString();

            if (GridCommingCartridges.Visibility == Visibility.Hidden)
            {
                GridCommingCartridges.Visibility = Visibility.Visible;
            }
            else if (GridCommingCartridges.Visibility == Visibility.Visible)
            {
                GridCommingCartridges.Visibility = Visibility.Hidden;
            }
        }

        private async void ButtonComming_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxCommingCartridges.SelectedItem == null || TextBoxCommingCount.Text == "" || !Int32.TryParse(TextBoxCommingCount.Text, out int count) || DatePickerDateComming.Text == null)
            {
                MessageBox.Show("Ошибка заполнения");
                return;
            }
            DateTime TimeComming = DateTime.SpecifyKind((DateTime)DatePickerDateComming.SelectedDate, DateTimeKind.Utc);
            ctx.Commings.Add(new Comming
            {
                CartridgeId = ((Cartridge)ComboBoxCommingCartridges.SelectedItem).Id,
                CartridgeObject = (Cartridge)ComboBoxCommingCartridges.SelectedItem,
                Location = (VarLocation)ComboBoxCommingCartrideLocations.SelectedItem,
                Count = count,
                CommingDate = TimeComming
            });
            await ctx.SaveChangesAsync();
            ctx.Cartridges.FirstOrDefault(x => x == (Cartridge)ComboBoxCommingCartridges.SelectedItem).StockCount += count;
            await ctx.SaveChangesAsync();
            DataGridCartridges.ItemsSource = Cartridges = new BindingList<Cartridge>(ctx.Cartridges.ToList());
            GridCommingCartridges.Visibility = Visibility.Hidden;
        }

        private void ComboBoxCommingCartrideLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCommingCartrideLocations.SelectedItem == null)
            {
                return;
            }
            ComboBoxCommingCartridges.ItemsSource = ctx.Cartridges.Where(x => x.Location == (VarLocation)ComboBoxCommingCartrideLocations.SelectedItem).ToList();
        }

        private void CheckBoxAutoFillCartridge_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ComboBoxShipmentCartridge != null)
            {
                ComboBoxShipmentCartridge.IsEnabled = true;
                //ComboBoxShipmentCartridge.ItemsSource = ctx.Cartridges.ToList();
            }
        }

        private void CheckBoxAutoFillCartridge_Checked(object sender, RoutedEventArgs e)
        {
            if (ComboBoxShipmentCartridge != null)
            {
                ComboBoxShipmentCartridge.IsEnabled = false;
            }
        }

        private async void ButtonShipmentCartridge_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxShipmentRoom.Text == null || 
                ComboBoxShipmentLocation.SelectedItem == null || 
                ComboBoxPrinters.SelectedItem == null || 
                ComboBoxShipmentCartridge.SelectedItem == null)
            {
                MessageBox.Show("Неверное заполнение");
                return;
            }

            ctx.Shipments.Add(new Shipment
            {
                CartridgeId = ((PrinterCartridge)ComboBoxShipmentCartridge.SelectedItem).Cartridge.Id,
                CartridgeObject = ((PrinterCartridge)ComboBoxShipmentCartridge.SelectedItem).Cartridge,
                PrinterId = ((PrinterInRoom)ComboBoxPrinters.SelectedItem).PrinterObject.Id,
                PrinterObject = ((PrinterInRoom)ComboBoxPrinters.SelectedItem).PrinterObject,
                Room = TextBoxShipmentRoom.Text,
                ShipmentDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            });
            await ctx.SaveChangesAsync();
            ctx.Cartridges.FirstOrDefault(x => x.Id == ((PrinterCartridge)ComboBoxShipmentCartridge.SelectedItem).Cartridge.Id).StockCount--;
            await ctx.SaveChangesAsync();
            DataGridCartridges.ItemsSource = Cartridges = new BindingList<Cartridge>(ctx.Cartridges.ToList());
            GridShipmentCartridge.Visibility= Visibility.Hidden;
            MessageBox.Show("Выдалось");
        }

        private void TextBoxShipmentRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBoxPrinters.ItemsSource = ctx.PrinterInRooms.Where(x => x.Room.ToLower().Contains(TextBoxShipmentRoom.Text.ToLower())).ToList();
            if (ComboBoxPrinters.Items.Count > 0)
            {
                ComboBoxPrinters.SelectedItem = ComboBoxPrinters.Items[0];
            }
        }

        private void ComboBoxShipmentLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxPrinters.ItemsSource = ctx.PrinterInRooms.Where(x => x.Room.ToLower().Contains(TextBoxShipmentRoom.Text.ToLower()) &
                                                                             x.PrinterObject.Location == ((VarLocation)ComboBoxShipmentLocation.SelectedItem)).ToList();
                ComboBoxPrinters.SelectedItem = ComboBoxPrinters.Items[0];
            }
            catch (Exception)
            {
                MessageBox.Show("нету принтера в выбранном корпусе", "Alert");
                return;
                //throw;
            }
        }

        private void ComboBoxPrinters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPrinter = ComboBoxPrinters.SelectedItem as PrinterInRoom;
            if (selectedPrinter != null)
            {
                ComboBoxShipmentCartridge.ItemsSource = ctx.PrinterCartridges.Where(x => x.Printer == selectedPrinter.PrinterObject).ToList();
                ComboBoxShipmentCartridge.SelectedItem = ComboBoxShipmentCartridge.Items[0];
            }
            

        }

        private void DataGridCartridges_Sorting(object sender, DataGridSortingEventArgs e)
        {
            //if (e.Column.SortDirection.ToString() == ListSortDirection.Descending.ToString())
            //{
            //    switch (e.Column.Header)
            //    {
            //        case "Наименование":
            //            DataGridCartridges.ItemsSource = Cartridges.OrderByDescending(x => x.Name);
            //            break;
            //        case "Кол-во на складе":
            //            DataGridCartridges.ItemsSource = Cartridges.OrderByDescending(x => x.StockCount);
            //            break;
            //        case "Расположение":
            //            DataGridCartridges.ItemsSource = Cartridges.OrderByDescending(x => x.Location);
            //            break;
            //        default:
            //            Cartridges.OrderByDescending(x => x.Id);
            //            break;
            //    }
            //}
            //else if (e.Column.SortDirection == ListSortDirection.Ascending)
            //{
            //    switch (e.Column.Header)
            //    {
            //        case "Наименование":
            //            DataGridCartridges.ItemsSource = Cartridges.OrderBy(x => x.Name);
            //            break;
            //        case "Кол-во на складе":
            //            DataGridCartridges.ItemsSource = Cartridges.OrderBy(x => x.StockCount);
            //            break;
            //        case "Расположение":
            //            DataGridCartridges.ItemsSource = Cartridges.OrderBy(x => x.Location);
            //            break;
            //        default:
            //            Cartridges.OrderBy(x => x.Id);
            //            break;
            //    }
            //}
            
            // Отмена сортировки, если столбец не поддерживает сортировку
            if (!e.Column.SortDirection.HasValue) return;

            // Получение свойства, соответствующего столбцу
            string propertyName = e.Column.SortMemberPath;

            // Получение текущего списка данных
            List<Cartridge> cartridges = ((BindingList<Cartridge>)DataGridCartridges.ItemsSource).ToList();
            if (cartridges == null) return;

            // Сортировка списка данных
            if (e.Column.SortDirection.Value == ListSortDirection.Ascending)
            {
                cartridges.OrderBy(x => x.Name);
                //cartridges.Sort((x, y) => x.GetType().GetProperty(Name)?.GetValue(x)?.ToString().CompareTo(y.GetType().GetProperty(Name)?.GetValue(y)?.ToString()) ?? 0);
            }
            else
            {
                cartridges.OrderByDescending(x => x.Name);
                //cartridges.Sort((x, y) => y.GetType().GetProperty(Name)?.GetValue(y)?.ToString().CompareTo(x.GetType().GetProperty(Name)?.GetValue(x)?.ToString()) ?? 0);
            }

            // Обновление источника данных
            DataGridCartridges.ItemsSource = cartridges;

            // Отмена стандартной сортировки
            e.Handled = true;
        }
    }
}

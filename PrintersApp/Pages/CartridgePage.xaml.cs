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
        public List<Cartridge> Cartridges { get; set; } = new List<Cartridge>();
        public VarLocation currentLocation;
        public CartridgePage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            Cartridges = new List<Cartridge>(ctx.Cartridges.ToList());
            DataGridCartridges.ItemsSource = Cartridges;

            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            ComboBoxFilterLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
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
            DataGridCartridges.ItemsSource = Cartridges = new List<Cartridge>(ctx.Cartridges.ToList());
            searchCartridges();
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
                DataGridCartridges.ItemsSource = Cartridges = new List<Cartridge>(ctx.Cartridges.ToList());
                searchCartridges();
                GridAddEditElement.Visibility = Visibility.Hidden;
                return;
            }
            pickCartridge.Name = TextBoxName.Text;
            pickCartridge.StockCount = Convert.ToInt32(TextBoxStockCount.Text);
            pickCartridge.Location = (VarLocation)ComboBoxLocation.SelectedItem;
            ctx.Cartridges.Update(pickCartridge);
            await ctx.SaveChangesAsync();
            GridAddEditElement.Visibility = Visibility.Hidden;
            DataGridCartridges.ItemsSource = Cartridges = new List<Cartridge>(ctx.Cartridges.ToList());
            searchCartridges();
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchCartridges();
        }

        private void searchCartridges()
        {
            if (TextBoxSearch.Text == "" || TextBoxSearch.Text == "Поиск")
            {
                DataGridCartridges.ItemsSource = currentLocation == VarLocation.Общий? Cartridges : Cartridges.Where(c => c.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()));
                return;
            }
            DataGridCartridges.ItemsSource = currentLocation == VarLocation.Общий? 
                                                    Cartridges.Where(c => c.Id.ToString() == TextBoxSearch.Text ||
                                                                    c.Name.ToLower().Contains(TextBoxSearch.Text))
                                                        : 
                                                    Cartridges.Where(c => c.Name.ToLower().Contains(TextBoxSearch.Text) &&
                                                                    c.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.Id.ToString() == TextBoxSearch.Text &&
                                                                    c.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()));
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                DataGridCartridges.ItemsSource = new List<Cartridge>(ctx.Cartridges.ToList());
                searchCartridges();
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
                //MessageBox.Show("Порядок заполнения: Кабинет => Расположение => Принтер \nВ другом порядке всё может сломается", "Alert");
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
            DataGridCartridges.ItemsSource = Cartridges = new List<Cartridge>(ctx.Cartridges.ToList());
            searchCartridges();
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
                List<PrinterCartridge> temp = new List<PrinterCartridge>();
                foreach (var item in ctx.Cartridges.ToList())
                {
                    temp.Add(new PrinterCartridge { CartridgeId = item.Id, Cartridge = item, Printer = new Printer { InventoryNumber = "111", Name = "asd" } });
                }
                ComboBoxShipmentCartridge.ItemsSource = temp;
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
            DataGridCartridges.ItemsSource = Cartridges = new List<Cartridge>(ctx.Cartridges.ToList());
            searchCartridges();
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
                //MessageBox.Show("нету принтера в выбранном корпусе", "Alert");
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

        private void ComboBoxFilterLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxFilterLocation.SelectedValue.ToString())
            {
                case "Первый":
                    currentLocation = VarLocation.Первый;
                    searchCartridges();
                    break;
                case "Второй":
                    currentLocation = VarLocation.Второй;
                    searchCartridges();
                    break;
                case "ККМТ":
                    currentLocation = VarLocation.ККМТ;
                    searchCartridges();
                    break;
                case "ТТД":
                    currentLocation = VarLocation.ТТД;
                    searchCartridges();
                    break;
                case "Общий":
                    currentLocation = VarLocation.Общий;
                    searchCartridges();
                    break;
                default:
                    currentLocation = VarLocation.Первый;
                    searchCartridges();
                    break;
            }
        }
    }
}

using PrintersApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для WorkStationsPage.xaml
    /// </summary>
    public partial class WorkStationsPage : Page
    {
        ContextDataBase ctx;

        public List<WorkStationsInRoom> WorkStationData{ get; set; } = new List<WorkStationsInRoom>();
        
        public VarLocation currentLocation;
        public WorkStationsPage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            WorkStationData = new List<WorkStationsInRoom>(ctx.WorkStationsInRooms.ToList());
            foreach (var data in WorkStationData)
            {
                data.WorkStationObject = ctx.WorkStations.First(x => x.Id == data.WorkStationId);
            }
            DataGridWorkStations.ItemsSource = WorkStationData;
        }

        private void searchPrinters()
        {
            string param = TextBoxSearch.Text.ToLower();
            if (param == "" || param == "поиск")
            {
                DataGridWorkStations.ItemsSource = currentLocation == VarLocation.Общий ? WorkStationData : WorkStationData.Where(c => c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()));
                return;
            }
            DataGridWorkStations.ItemsSource = currentLocation == VarLocation.Общий ?
                                                    WorkStationData.Where(c => c.WorkStationObject.Id.ToString() == param ||
                                                                    c.WorkStationObject.Brand.ToLower().Contains(param))
            :
                                                    WorkStationData.Where(c => c.WorkStationObject.Id.ToString().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.WorkStationObject.Brand.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.Room.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.WorkStationObject.Motherboard.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.WorkStationObject.CPU.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.WorkStationObject.GPU.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.WorkStationObject.RAMVolume.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.WorkStationObject.ROMSsdVolume.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.WorkStationObject.ROMHddVolume.ToString().ToLower().Contains(param) &&
                                                                    c.WorkStationObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchPrinters();
        }

        private void ComboBoxFilterLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxFilterLocation.SelectedValue.ToString())
            {
                case "Первый":
                    currentLocation = VarLocation.Первый;
                    searchPrinters();
                    break;
                case "Второй":
                    currentLocation = VarLocation.Второй;
                    searchPrinters();
                    break;
                case "ККМТ":
                    currentLocation = VarLocation.ККМТ;
                    searchPrinters();
                    break;
                case "ТТД":
                    currentLocation = VarLocation.ТТД;
                    searchPrinters();
                    break;
                case "Общий":
                    currentLocation = VarLocation.Общий;
                    searchPrinters();
                    break;
                default:
                    currentLocation = VarLocation.Первый;
                    searchPrinters();
                    break;
            }
        }

        private void ButtonAddElement_Click(object sender, RoutedEventArgs e)
        {
            var page = new PrintersApp.Windows.WorkStation(ctx);
            page.Show();
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridWorkStations.SelectedItem as WorkStationsInRoom == null)
            {
                return;
            }
            var page = new PrintersApp.Windows.WorkStation(ctx, DataGridWorkStations.SelectedItem as WorkStationsInRoom);
            page.Show();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridWorkStations.SelectedItem as WorkStationsInRoom == null)
            {
                return;
            }
            WorkStationsInRoom deleteWorkStationInRoom = DataGridWorkStations.SelectedItem as WorkStationsInRoom;
            ctx.WorkStationsInRooms.Remove(deleteWorkStationInRoom);
            ctx.WorkStations.Remove(ctx.WorkStations.First(c => c.Id == deleteWorkStationInRoom.WorkStationId));
            ctx.SaveChanges();
            DataGridWorkStations.ItemsSource = 
        }
    }
}

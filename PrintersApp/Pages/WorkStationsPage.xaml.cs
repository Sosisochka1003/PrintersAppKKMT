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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ContextDataBase.WorkStation test = ctx.WorkStations.First();
            //ctx.WorkStationsInRooms.Add(new WorkStationsInRoom { Room = "123", WorkStationId = test.Id, WorkStationObject = test, WorkStationStatus = Status.Work });
            //ctx.SaveChangesAsync();
            //foreach (var item in ctx.WorkStationsInRooms.ToList())
            //{
            //    MessageBox.Show($"{item.WorkStationObject.Id} {item.WorkStationObject.CPU}");
            //}
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxFilterLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonAddElement_Click(object sender, RoutedEventArgs e)
        {
            var page = new PrintersApp.Windows.WorkStation(ctx);
            page.Show();
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            var page = new PrintersApp.Windows.WorkStation(ctx, DataGridWorkStations.SelectedItem as WorkStationsInRoom);
            page.Show();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

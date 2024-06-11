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
    /// Логика взаимодействия для WorkStation.xaml
    /// </summary>
    public partial class WorkStation : Window
    {
        ContextDataBase ctx;
        ContextDataBase.WorkStation UpdateWorkStation;
        public WorkStation(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
        }

        public WorkStation(ContextDataBase ctx, ContextDataBase.WorkStation WorkStationObject)
        {
            InitializeComponent();
            this.ctx = ctx;
            UpdateWorkStation = WorkStationObject;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            ComboBoxLocation.SelectedItem = WorkStationObject.Location;
            TextBoxBrand.Text = WorkStationObject.Brand;
            TextBoxMotherboad.Text = WorkStationObject.Motherboard;
            TextBoxCPU.Text = WorkStationObject.CPU;
            TextBoxGPU.Text = WorkStationObject.GPU;
            TextBoxRAMName.Text = WorkStationObject.RAMName;
            TextBoxRAMVolume.Text = WorkStationObject.RAMVolume.ToString();
            TextBoxROMSSDName.Text = WorkStationObject.ROMSsdName;
            TextBoxROMSSDVolume.Text = WorkStationObject.ROMSsdVolume.ToString();
            TextBoxROMHDDName.Text = WorkStationObject.ROMHddName;
            TextBoxROMHDDVolume.Text = WorkStationObject.ROMHddVolume.ToString();
            TextBoxMonitor.Text = WorkStationObject.Monitor;
            TextBoxKeyboard.Text = WorkStationObject.Keyboard;
            TextBoxMouse.Text = WorkStationObject.Mouse;
            TextBoxUPS.Text = WorkStationObject.UPS;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            int SSDVolume = 0;
            int HDDVolume = 0;
            if (ComboBoxLocation.SelectedItem == null ||
                TextBoxRoom.Text == null ||
                TextBoxBrand.Text == null ||
                TextBoxMotherboad.Text == null || 
                TextBoxCPU.Text == null ||
                TextBoxGPU.Text == null ||
                TextBoxRAMName.Text == null ||
                TextBoxRAMVolume.Text == null ||
                !Int32.TryParse(TextBoxRAMVolume.Text, out int RAMVolume) ||
                TextBoxMonitor.Text == null ||
                TextBoxKeyboard.Text == null ||
                TextBoxMouse.Text == null)
            {
                MessageBox.Show("Некорректное заполнение данных");
                return;
            }
            if (!Int32.TryParse(TextBoxROMSSDVolume.Text, out SSDVolume) || !Int32.TryParse(TextBoxROMHDDVolume.Text, out HDDVolume))
            {
                MessageBox.Show("Неверное заполнение данных");
                return;
            }
            if (UpdateWorkStation == null)
            {
                ContextDataBase.WorkStation newWorkStation = new ContextDataBase.WorkStation
                {
                    Location = (VarLocation)ComboBoxLocation.SelectedItem,
                    Brand = TextBoxBrand.Text,
                    Motherboard = TextBoxMonitor.Text,
                    CPU = TextBoxCPU.Text,
                    GPU = TextBoxGPU.Text,
                    RAMName = TextBoxRAMName.Text,
                    RAMVolume = RAMVolume,
                    ROMSsdName = TextBoxROMSSDName.Text,
                    ROMSsdVolume = SSDVolume,
                    ROMHddName = TextBoxROMHDDName.Text,
                    ROMHddVolume = HDDVolume,
                    Monitor = TextBoxMonitor.Text,
                    Keyboard = TextBoxKeyboard.Text,
                    Mouse = TextBoxMouse.Text,
                    UPS = TextBoxUPS.Text,
                };
                ctx.WorkStations.Add(newWorkStation);
                await ctx.SaveChangesAsync();

                var tempWorkStation = ctx.Entry(newWorkStation).Entity;

                ContextDataBase.WorkStationsInRoom workStationsInRoom = new WorkStationsInRoom
                {
                    WorkStationId = tempWorkStation.Id,
                    Room = TextBoxRoom.Text,
                    WorkStationObject = tempWorkStation,
                    WorkStationStatus = Status.Work
                };
                ctx.WorkStationsInRooms.Add(workStationsInRoom);
                await ctx.SaveChangesAsync();
                MessageBox.Show("save");
            }

        }
    }
}

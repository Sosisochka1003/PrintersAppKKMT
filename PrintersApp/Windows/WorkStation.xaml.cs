using DocumentFormat.OpenXml.Drawing.Diagrams;
using PrintersApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        ContextDataBase.WorkStationsInRoom UpdateWorkStationInRoom;
        ContextDataBase.WorkStation UpdateWorkStation;
        public WorkStation(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
        }

        public WorkStation(ContextDataBase ctx, ContextDataBase.WorkStationsInRoom WorkStationInRoomObject)
        {
            InitializeComponent();
            this.ctx = ctx;
            UpdateWorkStationInRoom = WorkStationInRoomObject;
            UpdateWorkStation = ctx.WorkStations.First(x => x.Id == UpdateWorkStationInRoom.WorkStationId);
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            ComboBoxLocation.SelectedItem = UpdateWorkStation.Location;
            TextBoxRoom.Text = UpdateWorkStationInRoom.Room;
            TextBoxBrand.Text = UpdateWorkStation.Brand;
            TextBoxMotherboad.Text = UpdateWorkStation.Motherboard;
            TextBoxCPU.Text = UpdateWorkStation.CPU;
            TextBoxGPU.Text = UpdateWorkStation.GPU;
            TextBoxRAMName.Text = UpdateWorkStation.RAMName;
            TextBoxRAMVolume.Text = UpdateWorkStation.RAMVolume.ToString();
            TextBoxROMSSDName.Text = UpdateWorkStation.ROMSsdName;
            TextBoxROMSSDVolume.Text = UpdateWorkStation.ROMSsdVolume.ToString();
            TextBoxROMHDDName.Text = UpdateWorkStation.ROMHddName;
            TextBoxROMHDDVolume.Text = UpdateWorkStation.ROMHddVolume.ToString();
            TextBoxMonitor.Text = UpdateWorkStation.Monitor;
            TextBoxKeyboard.Text = UpdateWorkStation.Keyboard;
            TextBoxMouse.Text = UpdateWorkStation.Mouse;
            TextBoxUPS.Text = UpdateWorkStation.UPS;
        }


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
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
            if (!Int32.TryParse(TextBoxROMSSDVolume.Text, out SSDVolume) && Int32.TryParse(TextBoxROMHDDVolume.Text, out HDDVolume) || Int32.TryParse(TextBoxROMSSDVolume.Text, out SSDVolume) && !Int32.TryParse(TextBoxROMHDDVolume.Text, out HDDVolume))
            {
                MessageBox.Show("Неверное заполнение данных");
                return;
            }
            if (UpdateWorkStationInRoom == null)
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
                ctx.SaveChanges();

                var tempWorkStation = ctx.Entry(newWorkStation).Entity;

                ContextDataBase.WorkStationsInRoom workStationsInRoom = new WorkStationsInRoom
                {
                    WorkStationId = tempWorkStation.Id,
                    Room = TextBoxRoom.Text,
                    WorkStationObject = tempWorkStation,
                    WorkStationStatus = Status.Work
                };
                ctx.WorkStationsInRooms.Add(workStationsInRoom);
                ctx.SaveChanges();
            }
            else
            {
                UpdateWorkStation.Location = (VarLocation)ComboBoxLocation.SelectedItem;
                UpdateWorkStation.Brand = TextBoxBrand.Text;
                UpdateWorkStation.Motherboard = TextBoxMotherboad.Text;
                UpdateWorkStation.CPU = TextBoxCPU.Text;
                UpdateWorkStation.GPU = TextBoxGPU.Text;
                UpdateWorkStation.RAMName = TextBoxRAMName.Text;
                UpdateWorkStation.RAMVolume = RAMVolume;
                UpdateWorkStation.ROMSsdName = TextBoxROMSSDName.Text;
                UpdateWorkStation.ROMSsdVolume = SSDVolume;
                UpdateWorkStation.ROMHddName = TextBoxROMHDDName.Text;
                UpdateWorkStation.ROMHddVolume = HDDVolume;
                UpdateWorkStation.Monitor = TextBoxMonitor.Text;
                UpdateWorkStation.Keyboard = TextBoxKeyboard.Text;
                UpdateWorkStation.Mouse = TextBoxMouse.Text;
                UpdateWorkStation.UPS = TextBoxUPS.Text;

                ctx.WorkStations.Update(UpdateWorkStation);
                ctx.SaveChanges();

                var tempWorkStation = ctx.Entry(UpdateWorkStation).Entity;

                UpdateWorkStationInRoom.Room = TextBoxRoom.Text;
                UpdateWorkStationInRoom.WorkStationId = tempWorkStation.Id;
                UpdateWorkStationInRoom.WorkStationObject = tempWorkStation;
                UpdateWorkStationInRoom.WorkStationStatus = Status.Work;

                ctx.WorkStationsInRooms.Update(UpdateWorkStationInRoom);
                ctx.SaveChanges();
            }
            MessageBox.Show("save");
            this.Close();
        }

    }
}

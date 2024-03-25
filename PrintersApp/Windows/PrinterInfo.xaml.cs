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
using static PrintersApp.Windows.Printers;

namespace PrintersApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для PrinterInfo.xaml
    /// </summary>
    public partial class PrinterInfo : Window
    {
        //ContextDataBase ctx;
        //Printer CurrentPrinter;
        //PrinterInRoom inRoom;
        public PrinterInfo(ContextDataBase globalctx)
        {
            InitializeComponent();
            //ctx = globalctx;
            //ComboBoxCartridges.ItemsSource = ctx.Cartridges.ToList();
        }

        //public PrinterInfo(ContextDataBase globalctx, FilterItems NewPrinter)
        //{
        //    InitializeComponent();
        //    //ctx = globalctx;
        //    //CurrentPrinter = ctx.Printers.FirstOrDefault(p => p.Id == NewPrinter.Id);
        //    //inRoom = ctx.PrinterInRooms.FirstOrDefault(p => p.PrinterId == CurrentPrinter.Id);
        //    //TextBoxName.Text = NewPrinter.Name;
        //    //TextBoxRoom.Text = NewPrinter.Room;
        //    //ComboBoxCartridges.ItemsSource = ctx.Cartridges.ToList();
        //    //ComboBoxCartridges.SelectedItem = CurrentPrinter.CartridgeObject;
        //}

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            //if (CurrentPrinter == null && inRoom == null)
            //{
                //var newPrinter = new Printer
                //{
                //    Name = TextBoxName.Text,
                //    CartridgeId = ((Cartridge)ComboBoxCartridges.SelectedItem).Id,
                //    CartridgeObject = ((Cartridge)ComboBoxCartridges.SelectedItem)
                //};
                //ctx.Printers.Add(newPrinter);
                //ctx.SaveChanges();
                //ctx.PrinterInRooms.Add(new PrinterInRoom { PrinterId = newPrinter.Id, PrinterObject = newPrinter, Room = TextBoxRoom.Text });
                //ctx.SaveChanges();
                //this.Close();
                //return;
            //}

            //CurrentPrinter.Name = TextBoxName.Text;
            //CurrentPrinter.CartridgeId = ((Cartridge)ComboBoxCartridges.SelectedItem).Id;
            //CurrentPrinter.CartridgeObject = ((Cartridge)ComboBoxCartridges.SelectedItem);
            //inRoom.Room = TextBoxRoom.Text;

            //ctx.Printers.Update(CurrentPrinter);
            //ctx.PrinterInRooms.Update(inRoom);
            //ctx.SaveChanges();
            //this.Close();
        }

        private void ComboBoxCartridges_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //ComboBoxCartridges.IsDropDownOpen = true;
        }
    }
}

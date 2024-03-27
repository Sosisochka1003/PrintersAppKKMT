using DocumentFormat.OpenXml.InkML;
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
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        ContextDataBase ctx;
        public ReportsPage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
        }

        public class CommingItem
        {
            public required Cartridge Cartridge { get; set; }
            public VarLocation Location { get; set; }
            public int Count { get; set; }
        }


        private void ButtonCommingReport_Click(object sender, RoutedEventArgs e)
        {
            ExcelReports.CartridgeComming(ctx);
        }
        

        private void ButtonShipmentsReport_Click(object sender, RoutedEventArgs e)
        {
            ExcelReports.ShipmentReport(ctx);
        }

        private void ButtonStackCountCartridgesReport_Click(object sender, RoutedEventArgs e)
        {
            ExcelReports.CartridgeCount(ctx);
        }

        private void ComboBoxReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int typeReport = ComboBoxReports.SelectedIndex;
            switch (typeReport)
            {
                case 0:
                    TextBoxCommings.Text = "";
                    var query = ctx.Commings
                        .GroupBy(c => new { c.CommingDate, c.CartridgeId, c.Location })
                        .Select(g => new
                        {
                            CommingDate = g.Key.CommingDate,
                            CartridgeId = g.Key.CartridgeId,
                            Locations = g.Key.Location,
                            Counts = g.Sum(c => c.Count)
                        }).OrderBy(g => g.CommingDate);
                    DateTime lastDate = new DateTime();
                    foreach (var group in query)
                    {
                        if (lastDate != group.CommingDate)
                        {
                            TextBoxCommings.Text += "\n" + "Дата: " + group.CommingDate + "\n";
                        }
                        using (ContextDataBase tempctx = new ContextDataBase())
                        {
                            var temp = tempctx.Cartridges.FirstOrDefault(x => x.Id == group.CartridgeId);
                            TextBoxCommings.Text += "   Картридж: " + temp.Name;
                            TextBoxCommings.Text += "  Расположение: " + group.Locations;
                            TextBoxCommings.Text += "  Кол-во: " + group.Counts;
                            lastDate = group.CommingDate;
                            if (lastDate == group.CommingDate)
                            {
                                TextBoxCommings.Text += "\n";
                            }

                        }
                    }
                    break;
                case 1:
                    TextBoxCommings.Text = "";
                    var shipment = ctx.Shipments.ToList();
                    foreach (var item in shipment)
                    {
                        TextBoxCommings.Text += $"Принтер {item.PrinterObject.Name} \n";
                        TextBoxCommings.Text += $"Расположение {item.PrinterObject.Location} \n";
                        TextBoxCommings.Text += $"Кобинет {item.Room} \n";
                        TextBoxCommings.Text += $"Картридж {item.CartridgeObject.Name} \n";
                        TextBoxCommings.Text += $"Время выдачи {item.ShipmentDate} \n \n";
                    }
                    break;
                case 2:
                    TextBoxCommings.Text = "";
                    var CartridgeStackCount = ctx.Cartridges.ToList();
                    foreach (var item in CartridgeStackCount)
                    {
                        TextBoxCommings.Text += $"Картридж {item.Name} \n";
                        TextBoxCommings.Text += $"Расположение {item.Location} \n";
                        TextBoxCommings.Text += $"Остаток {item.StockCount} \n \n";
                    }
                    break;
            }
        }
    }
}

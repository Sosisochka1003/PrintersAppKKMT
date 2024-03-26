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
            public Cartridge Cartridge { get; set; }
            public VarLocation Location { get; set; }
            public int Count { get; set; }
        }

        private void ButtonGetResult_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void ButtonCommingReport_Click(object sender, RoutedEventArgs e)
        {
            ExcelReports.CartridgeComming(ctx);
        }
    }
}

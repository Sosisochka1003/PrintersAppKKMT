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
    /// Логика взаимодействия для CartridgeInfo.xaml
    /// </summary>
    public partial class CartridgeInfo : Window
    {
        ContextDataBase ctx;
        Cartridge CurrentCartridge = null;
        public CartridgeInfo(ContextDataBase globalctx)
        {
            InitializeComponent();
            ctx = globalctx;
        }

        public CartridgeInfo(ContextDataBase globalctx, Cartridge updateCartridge)
        {
            InitializeComponent();
            ctx = globalctx;
            CurrentCartridge = updateCartridge;
            TextBoxName.Text = updateCartridge.Name;
            TextBoxCount.Text = updateCartridge.StockCount.ToString();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCartridge == null)
            {
                CurrentCartridge = new Cartridge
                {
                    Name = TextBoxName.Text,
                    StockCount = Convert.ToInt32(TextBoxCount.Text),
                };
                ctx.Cartridges.Add(CurrentCartridge);
                ctx.SaveChanges();
                this.Close();
            }
            CurrentCartridge.Name = TextBoxName.Text;
            CurrentCartridge.StockCount = Convert.ToInt32(TextBoxCount.Text);
            ctx.Cartridges.Update(CurrentCartridge);
            ctx.SaveChanges();
            this.Close();
        }
    }
}

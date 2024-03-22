﻿using PrintersApp.Pages;
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

namespace PrintersApp
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        ContextDataBase ctx = new ContextDataBase();
        
        public StartWindow()
        {
            InitializeComponent();
            FrameCartridge.Navigate(new CartridgePage(ctx));
            FramePrinters.Navigate(new PrintersPage(ctx));
            FrameReports.Navigate(new ReportsPage());
        }

        
    }
}

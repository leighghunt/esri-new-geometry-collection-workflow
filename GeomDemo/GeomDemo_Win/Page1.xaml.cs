using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ESRI.ArcGIS.Mobile.Client;

namespace GeomDemo
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : MobileApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            //title
            this.Title = "Geometry Demo";
            //Note
            this.Note = "Page Note";

            //page icon
            // TODO: If you change your task assembly name, replace 'GeomDemo' 
            // in Uri with new task assembly name
            Uri uri = new Uri("pack://application:,,,/GeomDemo;Component/PageIcon72.png");
            this.ImageSource = new System.Windows.Media.Imaging.BitmapImage(uri);

            // back button
            this.BackCommands.Add(this.BackCommand);
        }

        protected override void OnBackCommandExecute()
        {
            MobileApplication.Current.Transition(this.PreviousPage);
        }

    }
}

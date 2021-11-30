using PoliceStationNew.XAML.EmployeePage.Personnel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace PoliceStationNew.XAML.Employee
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PersonnelMainPage : Page
    {
        public PersonnelMainPage()
        {
            this.InitializeComponent();
        }

        private void listOfNavigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Employees.IsSelected)
            {
                myFrame.Navigate(typeof(AllEmp), Frame);
            }
            if (Positions.IsSelected)
            {
                myFrame.Navigate(typeof(AllPositions), Frame);
            }
            if (Formations.IsSelected)
            {
                myFrame.Navigate(typeof(AllForm), Frame);
            }
        }
    }
}

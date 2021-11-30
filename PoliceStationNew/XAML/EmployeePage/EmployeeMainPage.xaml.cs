using PoliceStationNew.XAML.EmployeePage;
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
    public sealed partial class EmployeeMainPage : Page
    {
        public EmployeeMainPage()
        {
            this.InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (statements.IsSelected)
            {
                NavigateStatement(0);
            }
            if (Appeals.IsSelected)
            {
                NavigateStatement(1);
            }
        }

        public void NavigateStatement(int i)
        {
            if (i == 0) { myFrame.Navigate(typeof(AppealsAll), Frame); }
            if (i == 1) { myFrame.Navigate(typeof(EmpStatements), Frame); }
        }
    }
}

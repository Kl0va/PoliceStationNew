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

namespace PoliceStationNew.XAML.People
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UserMainPage : Page
    {
        public UserMainPage()
        {
            this.InitializeComponent();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (documents.IsSelected)
            {
                NavigateStatement(0);
            }
            if (checking.IsSelected)
            {
                NavigateStatement(1);
            }
            if (EmpIsChe.IsSelected)
            {
                NavigateStatement(2);
            }
        }

        public void NavigateStatement(int i)
        {
            if(i == 0) { myFrame.Navigate(typeof(UserStatements), Frame); }
            if(i == 1) { myFrame.Navigate(typeof(CheckingStatements), Frame); }
            if(i == 2) { myFrame.Navigate(typeof(CheckedStatements), Frame); }
        }
    }
}

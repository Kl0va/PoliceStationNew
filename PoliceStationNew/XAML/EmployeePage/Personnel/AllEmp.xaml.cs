using NPOI.XWPF.UserModel;
using PoliceStationNew.Models;
using PoliceStationNew.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace PoliceStationNew.XAML.EmployeePage.Personnel
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AllEmp : Page
    {
        private static List<Models.Employee> Employees = new List<Models.Employee>();
        public AllEmp()
        {
            this.InitializeComponent();
            Load();
        }

        public async void Load()
        {
            Check.editEmp = null;
            Employees.Clear();
            Task<List<Models.Employee>> getEmployees = ApiWork.GetAllEmployees();
            await getEmployees.ContinueWith(t =>
            {
                foreach (Models.Employee employee in getEmployees.Result)
                {
                    Employees.Add(employee);
                }
            });
            Thread.Sleep(100);
            foreach (Models.Employee employee1 in Employees)
            {
                EmpGrid.Items.Add(employee1);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddEmp));
        }

        private void EmpGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check.editEmp = (Models.Employee)EmpGrid.SelectedItem;
            Frame.Navigate(typeof(AddEmp));
        }

    }
}

using Flurl.Http;
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
    public sealed partial class AllForm : Page
    {
        private static List<Formation> formations = new List<Formation>();
        private static List<Models.Employee> employees = new List<Models.Employee>();
        public AllForm()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            formations.Clear();
            Task<List<Formation>> getFormations = ApiWork.GetAllFormations();
            await getFormations.ContinueWith(t =>
            {
                foreach (Formation formation in getFormations.Result)
                {
                    formations.Add(formation);
                }
            });
            Thread.Sleep(100);
            foreach (Formation formation1 in formations)
            {
                FormGrid.Items.Add(formation1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddForm));
        }

        private async void FormGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employees.Clear();
            Models.Formation form = (Formation)FormGrid.SelectedItem;


            Task<List<Models.Employee>> getEmployees = ApiWork.GetAllEmployees();
            await getEmployees.ContinueWith(t =>
            {
                foreach (Models.Employee employee in getEmployees.Result)
                {
                    employees.Add(employee);
                }
            });
            //FormGrid.ItemsSource = employees;
            bool check = true;
            foreach (Models.Employee employee1 in employees)
            {
                if (check)
                {
                    foreach (Formation formation in formations.ToList())
                    {
                        if (employee1.formation_id == formation.id)
                        {
                            ContentDialog contentDialog = new ContentDialog
                            {
                                Title = "Ошибка",
                                Content = "У сотрудника есть данная должность",
                                CloseButtonText = "Ок"
                            };
                            await contentDialog.ShowAsync();
                            check = false;
                            break;
                        }
                    }
                }
            }
            if (check)
            {
                var response = await "https://police-api-russia.herokuapp.com/delete_formation".PostJsonAsync(form).ReceiveString();
                Frame.Navigate(typeof(AllForm));
            }
        }
    }
}

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

namespace PoliceStationNew.XAML.EmployeePage.Accounting
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AddVac : Page
    {
        private static Models.Employee Employee;
        private static List<string> fam = new List<string>();
        private static List<Models.Employee> EmployeeNum = new List<Models.Employee>();
        private static String fam1;
        public AddVac()
        {
            this.InitializeComponent();
            Load();
        }

        public async void Load()
        {
            if (Check.editVac != null)
            {
                Number.Visibility = Visibility.Collapsed;
                Emp.Visibility = Visibility.Collapsed;
                Type.SelectedValue = Check.editVac.type_otp;
                Start.Date = DateTime.Parse(Check.editVac.start_otp);
                End.Date = DateTime.Parse(Check.editVac.end_otp);
                Type.Items.Add("За свой счёт");
                Type.Items.Add("Обычный");
            }
            else
            {
                Task<List<Models.Employee>> getEmployees = ApiWork.GetAllEmployees();
                await getEmployees.ContinueWith(t =>
                {
                    foreach (Models.Employee employee in getEmployees.Result)
                    {
                        EmployeeNum.Add(employee);
                        fam.Add(employee.second);
                    }
                });

                Thread.Sleep(100);
                foreach (String s in fam)
                {
                    Emp.Items.Add(s);
                }
                Type.Items.Add("За свой счёт");
                Type.Items.Add("Обычный");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Type.SelectedValue.ToString() == "За свой счёт")
            {
                Coef.Visibility = Visibility.Visible;
            }
            else
            {
                Coef.Visibility = Visibility.Collapsed;
            }

        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (Check.editVac != null)
                {
                    if (Coef.Text != "")
                    {
                        try
                        {
                            var vacation = new Vacation(Check.editVac.id, Start.Date.Value.ToString(), Decimal.Parse(Coef.Text), End.Date.Value.ToString(), Type.SelectedItem.ToString());
                            var response = await "https://police-api-russia.herokuapp.com/edit_vacation".PostJsonAsync(vacation).ReceiveString();
                        }
                        catch
                        {
                            Frame.Navigate(typeof(AllPrizes));
                        }
                    }
                    else
                    {
                        try
                        {
                            var vacation = new Vacation(Check.editVac.id, Start.Date.Value.ToString(), End.Date.Value.ToString(), Type.SelectedItem.ToString());
                            var response = await "https://police-api-russia.herokuapp.com/edit_vacation".PostJsonAsync(vacation).ReceiveString();
                        }
                        catch
                        {
                            Frame.Navigate(typeof(AllPrizes));
                        }
                    }
                }
                fam1 = "";
                if (Emp.Text == "Сотрудник" || Type.SelectedValue.ToString() == "" || Start.Date.Value == null || End.Date.Value == null)
                {
                    ContentDialog contentDialog = new ContentDialog
                    {
                        Title = "Ошибка",
                        Content = "Неверно заполнены поля",
                        CloseButtonText = "Ок"
                    };
                    await contentDialog.ShowAsync();
                }
                else
                {
                    fam1 = Emp.SelectedValue.ToString();
                    Task<List<Models.Employee>> getEmployees = ApiWork.GetAllEmployees();
                    await getEmployees.ContinueWith(t =>
                    {
                        foreach (Models.Employee employee in getEmployees.Result)
                        {
                            if (fam1 == employee.second)
                            {
                                Employee = employee;
                            }
                        }
                    });
                    if (Coef.Text != "")
                    {
                        var vacation = new Vacation(Start.Date.Value.ToString(), End.Date.Value.ToString(), decimal.Parse(Coef.Text), Type.SelectedItem.ToString(), Employee.id);
                        var response = await "https://police-api-russia.herokuapp.com/input_vacation".PostJsonAsync(vacation).ReceiveString();
                        Frame.Navigate(typeof(AllVacations));
                    }
                    else
                    {
                        var vacation = new Vacation(Start.Date.Value.ToString(), End.Date.Value.ToString(), Type.SelectedItem.ToString(), Employee.id);
                        var response = await "https://police-api-russia.herokuapp.com/input_vacation".PostJsonAsync(vacation).ReceiveString();
                        Frame.Navigate(typeof(AllVacations));
                    }
                }
            }
            catch
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = "Неверно заполнены поля",
                    CloseButtonText = "Ок"
                };
                await contentDialog.ShowAsync();
            }
            }

        private void Emp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Models.Employee employee in EmployeeNum)
            {
                if (Emp.SelectedValue.ToString() == employee.second)
                {
                    Number.Text = employee.id.ToString();
                }
            }
        }
    }
}

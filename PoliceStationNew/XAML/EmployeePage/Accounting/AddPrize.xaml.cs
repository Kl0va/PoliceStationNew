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
    public sealed partial class AddPrize : Page
    {
        private static Models.Employee Employee;
        private static List<Models.Employee> EmployeeNum = new List<Models.Employee>();
        private static List<string> fam = new List<string>();
        private static String fam1;
        public AddPrize()
        {
            this.InitializeComponent();
            Load();
        }

        public async void Load()
        {
            if (Check.editPrize != null)
            {
                Emp.Visibility = Visibility.Collapsed;
                Number.Visibility = Visibility.Collapsed;
                Prize.Text = Check.editPrize.coef_prem;
            }
            else
            {
                fam.Clear();
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
            }
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Check.editPrize != null)
                {
                    string prize1 = Prize.Text;
                    var prize = new Prize(Check.editPrize.id, prize1);
                    var response = await "https://police-api-russia.herokuapp.com/edit_prize".PostJsonAsync(prize).ReceiveString();
                    Frame.Navigate(typeof(AllPrizes));
                }
                else
                {
                    fam1 = "";
                    if (Emp.Text == "Сотрудник" || Prize.Text.Length < 1 || int.Parse(Prize.Text) <= 0)
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

                        string prize1 = Prize.Text;
                        int id = Employee.id;
                        var prize = new Prize(prize1, id);
                        var response = await "https://police-api-russia.herokuapp.com/input_prize".PostJsonAsync(prize).ReceiveString();


                        Frame.Navigate(typeof(AllPrizes));

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
            foreach(Models.Employee employee in EmployeeNum)
            {
                if(Emp.SelectedValue.ToString() == employee.second)
                {
                    Number.Text = employee.id.ToString();
                }
            }
        }
    }
}

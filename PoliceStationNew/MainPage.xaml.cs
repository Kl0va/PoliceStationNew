using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using PoliceStationNew.Models;
using PoliceStationNew.Moduls;
using PoliceStationNew.XAML;
using PoliceStationNew.XAML.Employee;
using PoliceStationNew.XAML.People;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace PoliceStationNew
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = Email.Text; 
            string password = Password.Password;
            Check.emcheck = "";
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password.Password));
            var hash1 = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            Task<List<Declarant>> getDeclarants = ApiWork.GetAllDeclarants();
            await getDeclarants.ContinueWith(t =>
            {
                foreach (Declarant declarant in getDeclarants.Result)
                {
                    if (email == declarant.email && Convert.ToBase64String(hash) == declarant.password)
                    {
                        Check.emcheck = "USER";
                    }
                }
            });

            Task<List<Models.Employee>> getEmployees = ApiWork.GetAllEmployees();
            await getEmployees.ContinueWith(t =>
            {
                foreach (Models.Employee employee in getEmployees.Result)
                {
                    if (email == employee.email && Convert.ToBase64String(hash1) == employee.password)
                    {
                        Check.emcheck = "Employee";
                        Check.roleEmp = employee.role_id;
                        Check.ID = employee.id;
                    }
                }
            });



            if (Check.emcheck == "USER")
            {
                Check.email = Email.Text;
                Frame.Navigate(typeof(UserMainPage));
            }else
            if(Check.emcheck == "Employee")
            {
                Check.email = Email.Text;
                if (Check.roleEmp == "Сотрудник")
                {
                    Frame.Navigate(typeof(EmployeeMainPage));
                }
                if(Check.roleEmp == "Acc")
                {
                    Frame.Navigate(typeof(AccountingMainPage));
                }
                if(Check.roleEmp == "Pers")
                {
                    Frame.Navigate(typeof(PersonnelMainPage));
                }
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = "Неправильный логин и/или пароль",
                    CloseButtonText = "Ок"
                };
                await contentDialog.ShowAsync();
            }         
        }

        public void NavigateTO()
        {
            Frame.Navigate(typeof(UserMainPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registration));
        }
    }
}

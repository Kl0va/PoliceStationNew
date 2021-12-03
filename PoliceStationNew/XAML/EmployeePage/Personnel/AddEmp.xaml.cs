using Flurl.Http;
using PoliceStationNew.Models;
using PoliceStationNew.Moduls;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace PoliceStationNew.XAML.EmployeePage.Personnel
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AddEmp : Page
    {
        private static List<Position> positions = new List<Position>();
        private static List<Role> roles = new List<Role>();
        private static List<Formation> formations = new List<Formation>();
        public AddEmp()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            positions.Clear();
            roles.Clear();
            formations.Clear();

            
            if (Check.editEmp == null)
            {
                Dis.Visibility = Visibility.Collapsed;
            }
            else
            {
                Name.Text = Check.editEmp.first;
                Fam.Text = Check.editEmp.second;
                Middle.Text = Check.editEmp.middle;
                Email.Text = Check.editEmp.email;
                Ser.Text = Check.editEmp.series;
                Num.Text = Check.editEmp.number;
                Rec.Date = DateTime.Parse(Check.editEmp.date_rec);
                if (Check.editEmp.date_dis == "")
                {

                }
                else
                {
                    Dis.Date = DateTime.Parse(Check.editEmp.date_dis);
                }
            }
            Task<List<Position>> getPositions = ApiWork.GetAllPositions();
            await getPositions.ContinueWith(t =>
            {
                foreach (Position position in getPositions.Result)
                {
                    positions.Add(position);
                }
            });
            Thread.Sleep(100);
            foreach (Position position1 in positions)
            {
                Pos.Items.Add(position1.name);
            }
            
            Task<List<Role>> getRoles = ApiWork.GetAllRoles();
            await getRoles.ContinueWith(t =>
            {
                foreach (Role role in getRoles.Result)
                {
                    roles.Add(role);
                }
            });
            Thread.Sleep(100);
            foreach (Role role1 in roles)
            {
                Roles.Items.Add(role1.roleName);
            }
            
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
                Form.Items.Add(formation1.formation_name);
            }
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Check.editEmp != null)
                {
                    var md5 = MD5.Create();
                    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password.Text));
                    int pos_id = 0;
                    int form_id = 0;
                    foreach (Position position in positions)
                    {
                        if (Pos.SelectedValue.ToString() == position.name)
                        {
                            pos_id = position.id;
                        }
                    }
                    foreach (Formation formation in formations)
                    {
                        if (Form.SelectedValue.ToString() == formation.formation_name)
                        {
                            form_id = formation.id;
                        }
                    }
                    var emp = new Models.Employee(Check.editEmp.id,Name.Text, Fam.Text, Middle.Text, Ser.Text, Num.Text, Email.Text, Convert.ToBase64String(hash), Rec.Date.Value.ToString(),Dis.Date.Value.ToString(), pos_id, Roles.SelectedValue.ToString(), form_id);
                    var response = await "https://police-api-russia.herokuapp.com/edit_employee".PostJsonAsync(emp).ReceiveString();
                }
                else
                {
                    var md5 = MD5.Create();
                    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password.Text));
                    int pos_id = 0;
                    int form_id = 0;
                    foreach (Position position in positions)
                    {
                        if (Pos.SelectedValue.ToString() == position.name)
                        {
                            pos_id = position.id;
                        }
                    }
                    foreach (Formation formation in formations)
                    {
                        if (Form.SelectedValue.ToString() == formation.formation_name)
                        {
                            form_id = formation.id;
                        }
                    }
                    var emp = new Models.Employee(Name.Text, Fam.Text, Middle.Text, Ser.Text, Num.Text, Email.Text, Convert.ToBase64String(hash), Rec.Date.Value.ToString(), "", pos_id, Roles.SelectedValue.ToString(), form_id);
                    var response = await "https://police-api-russia.herokuapp.com/input_employee".PostJsonAsync(emp).ReceiveString();
                }
                Frame.Navigate(typeof(AllEmp));
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
            
        
    }
}

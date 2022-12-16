using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace novator_test.Windows
{
    /// <summary>
    /// Логика взаимодействия для addEmployee.xaml
    /// </summary>
    public partial class addEmployee : Window
    {
        public addEmployee()
        {
            InitializeComponent();
        }

        private void addButon_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).staff.Add(new Models.StaffDTO(name.Text, lastname.Text, patronymic.Text));
            ((MainWindow)Application.Current.MainWindow).staffManagment.Add(name.Text, lastname.Text, patronymic.Text);
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using novator_test.Components;
using novator_test.Models;
using novator_test.Windows;

namespace novator_test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public ObservableCollection<StaffDTO> staff = new ObservableCollection<StaffDTO>();//собственно сам массив с сотрудниками, которые отображаются
        //ObservableCollection выбран, потому что он позволяет автоматически уведомлять элементы, которые его используют об изменение его элементов
        public StaffManagment staffManagment = new StaffManagment();
        int itemIndexSelected = 0;

        public MainWindow()
        {
            InitializeComponent();
            staff = staffManagment.Get();//вытаскиваем из бд список сотрудников
        }


        private void staffList_Loaded(object sender, RoutedEventArgs e)
        {
            staffList.ItemsSource = staff;//устанавливаем наш массив сотрудников в таблицу
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //добавление происходит в другом окне, поэтому создаём и там добавляем
            addEmployee ae = new addEmployee();
            ae.Owner = this;//делаем это окно родительским, чтобы в другом иметь доступ к переменным в этом окне (классе)
            ae.Show();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            //кнопка удаления сотрудника
            //проверяем есть ли выделенные сотрудники и тогда удаляем того, кто был выделен последним
            if (staffList.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выделите сотрудника", "Удаление",MessageBoxButton.OK);
            }
            else {
                Trace.WriteLine(staff[itemIndexSelected].name);
                staffManagment.Delete(staff[itemIndexSelected].name);
                staff.RemoveAt(itemIndexSelected);
                staffList.UnselectAll();
                MessageBox.Show("Сотрудник удалён","Удаление", MessageBoxButton.OK);
            }
        }

        private void staffList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //записываем индекс строки, которую выделили
            itemIndexSelected = staffList.SelectedIndex;
        }
    }
}

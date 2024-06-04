using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab13var17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PeoplesEntities db = new PeoplesEntities();

        List<People> peoplesDgList = new List<People>();

        public MainWindow()
        {
            InitializeComponent();

            dgPeoples.ItemsSource = db.People.ToList();

        }

        private void btnYoungPerson_Click(object sender, RoutedEventArgs e)
        {
            peoplesDgList.Clear();
            peoplesDgList.Add(db.People.Where(x => x.Soldier == "Да").OrderBy(x => x.Age).FirstOrDefault());
            dgPeoples.ItemsSource = null;
            dgPeoples.ItemsSource = peoplesDgList;
        }

        private void btnOldPeople_Click(object sender, RoutedEventArgs e)
        {
            peoplesDgList.Clear();
            peoplesDgList.Add(db.People.Where(x => x.Soldier == "Нет").OrderByDescending(x => x.Age).FirstOrDefault());
            peoplesDgList.Add(db.People.Where(x => x.Soldier == "Да").OrderByDescending(x => x.Age).FirstOrDefault());
            dgPeoples.ItemsSource = null;
            dgPeoples.ItemsSource = peoplesDgList;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var surname = txbSurnameSearch.Text;
            peoplesDgList.Clear();
            foreach(var p in db.People.Where(x => x.Surname == surname))
            {
                peoplesDgList.Add(p);
            }
            dgPeoples.ItemsSource = null;
            dgPeoples.ItemsSource = peoplesDgList;
        }

        private void btnCancelSearch_Click(object sender, RoutedEventArgs e)
        {
            dgPeoples.ItemsSource = null;
            dgPeoples.ItemsSource = db.People.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow window = new AddPersonWindow(db);
            window.ShowDialog();
            dgPeoples.ItemsSource = null;
            dgPeoples.ItemsSource = db.People.ToList();
        }
    }
}

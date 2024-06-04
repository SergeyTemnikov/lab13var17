using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace lab13var17
{
    /// <summary>
    /// Логика взаимодействия для AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {

        PeoplesEntities db;
        byte[] image;

        public AddPersonWindow(PeoplesEntities db)
        {
            InitializeComponent();
            this.db=db;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog(); 
            if (openFile.ShowDialog() == true) 
            {
                image = File.ReadAllBytes(openFile.FileName); 
                imgPhoto.Source = new ImageSourceConverter().ConvertFrom(openFile.FileName) as ImageSource;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            People people = new People();

            people.Surname = txbSurname.Text;
            people.Age = int.Parse(txbAge.Text);
            people.Soldier = txbSoldier.Text;
            people.Image = image;

            try
            {
                db.People.Add(people);
                db.SaveChanges();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось записать человека");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

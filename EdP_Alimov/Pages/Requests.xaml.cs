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

namespace EdP_Alimov.Pages
{
    public partial class Requests : Page
    {
        private readonly Entities db;
        public Requests()
        {
            InitializeComponent();
            db = new Entities();
            DataGridRequests.ItemsSource = db.Requests.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestAddPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRequests.SelectedItem != null)
            {
                Request selectedRequest = DataGridRequests.SelectedItem as Request;
                if (selectedRequest != null)
                {
                    try
                    {
                        db.Requests.Attach(selectedRequest);
                        db.Requests.Remove(selectedRequest);
                        db.SaveChanges();
                        MessageBox.Show("Заявка удалена");
                        DataGridRequests.ItemsSource = db.Requests.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Заявка не была удалена из база данных");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestAddPage((sender as Button).DataContext as Request));
        }
    }
}

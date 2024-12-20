using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EdP_Alimov
{

    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            int UserPositionID = ((App)Application.Current).CurrentUserPositionID;

            if (UserPositionID == 1)
            {
                Employees.Visibility = Visibility.Collapsed;
                Reports.Visibility = Visibility.Collapsed;
                Equipment.Visibility = Visibility.Collapsed;
            }
            else
            {
                Feedback.Visibility = Visibility.Collapsed;
            }

            MainFrame = MainFrame;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"EdP_Alimov - {page.Title}";
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Employees.xaml", UriKind.Relative));
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Equipments.xaml", UriKind.Relative));
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Requests.xaml", UriKind.Relative));
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/RequestLogs.xaml", UriKind.Relative));
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Reports.xaml", UriKind.Relative));
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/QRPage.xaml", UriKind.Relative));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}

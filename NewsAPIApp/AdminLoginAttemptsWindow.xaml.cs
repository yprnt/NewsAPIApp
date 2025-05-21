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
using System.Windows.Shapes;

namespace NewsAPIApp
{
    /// <summary>
    /// Logique d'interaction pour AdminLoginAttemptsWindow.xaml
    /// </summary>
    public partial class AdminLoginAttemptsWindow : Window
    {
        private UserVM _userVM;
        private User _admin;
        public AdminLoginAttemptsWindow(User admin)
        {
            InitializeComponent();
            _admin = admin;
            _userVM = new UserVM();

            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            FromDatePicker.SelectedDate = firstDayOfMonth;
            ToDatePicker.SelectedDate = lastDayOfMonth;

            LoadFailedAttempts();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFailedAttempts();
        }

        private void LoadFailedAttempts()
        {
            if (FromDatePicker.SelectedDate == null || ToDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Veuillez sélectionner une période valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fromDate = FromDatePicker.SelectedDate.Value;
            DateTime toDate = ToDatePicker.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

            _userVM.LoadFailedAttempts(fromDate, toDate);
            AttemptsDataGrid.ItemsSource = _userVM.FailedAttempts;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ArticlesWindow articlesWindow = new ArticlesWindow(_admin);
            articlesWindow.Show();
            this.Close();
        }
    }
}
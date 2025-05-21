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
    /// Logique d'interaction pour RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        UserVM uservm;
        public RegisterWindow()
        {
            InitializeComponent();
            uservm = new UserVM();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string pseudo = PseudoTextBox.Text;
            string password = PasswordBox.Password;
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(pseudo) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User newUser = new User(pseudo, password, firstName, lastName);
            bool isRegistered = uservm.Inscription(newUser);

            if (isRegistered)
            {
                MessageBox.Show("Inscription réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'inscription. Veuillez réessayer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}


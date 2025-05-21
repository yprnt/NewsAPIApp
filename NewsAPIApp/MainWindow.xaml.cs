using System.Windows;
using System.Windows.Controls;

namespace NewsAPIApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserVM uservm;
        public MainWindow()
        {
            InitializeComponent();
            uservm = new UserVM();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string pseudo = PseudoTextBoxReal.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(pseudo) || string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Veuillez remplir tous les champs.");
                return;
            }

            User user = new User(pseudo, password);
            User authenticatedUser = uservm.Authenticate(user);

            if (authenticatedUser != null)
            {
                if (pseudo.ToLower() == "admin")
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Voulez-vous accéder au panneau d'administration des tentatives de connexion?",
                        "Accès administrateur",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var adminWindow = new AdminLoginAttemptsWindow(authenticatedUser);
                        adminWindow.Show();
                        this.Close();
                        return;
                    }
                }

                var articlesWindow = new ArticlesWindow(authenticatedUser);
                articlesWindow.Show();
                this.Close();
            }
            else
            {
                ShowErrorMessage("Pseudo ou mot de passe incorrect.");
            }
        }

        private void ShowErrorMessage(string message)
        {
            ErrorMessage.Text = message;
            ErrorMessage.Visibility = Visibility.Visible;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
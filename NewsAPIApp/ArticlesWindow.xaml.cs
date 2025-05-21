using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace NewsAPIApp
{
    /// <summary>
    /// Logique d'interaction pour ArticlesWindow.xaml
    /// </summary>
    public partial class ArticlesWindow : Window
    {
        public ArticlesVM _articlesVM { get; set; }
        private User _authenticatedUser;
        private string _selectedCategory = "General";
        public ArticlesWindow(User authenticatedUser)
        {
            if(authenticatedUser == null)
                throw new ArgumentNullException(nameof(authenticatedUser), "L'utilisateur authentifié ne peut pas être null");
            InitializeComponent();
            _authenticatedUser = authenticatedUser;

            if (CategoryComboBox != null && CategoryComboBox.Items.Count > 0)
            {
                CategoryComboBox.SelectedIndex = 2;
            }

            _articlesVM = new ArticlesVM();

            DataContext = _articlesVM;

            // Afficher le bouton admin uniquement pour l'utilisateur "admin"
            if (_authenticatedUser.pseudo.ToLower() == "admin")
            {
                AdminButton.Visibility = Visibility.Visible;
            }
            else
            {
                AdminButton.Visibility = Visibility.Collapsed;
            }

            LoadNews();
        }

        private async void LoadNews()
        {
            try
            {
                if (_articlesVM == null)
                {
                    MessageBox.Show("Le ViewModel n'est pas initialisé correctement.");
                    return;
                }

                if (_authenticatedUser == null)
                {
                    MessageBox.Show("L'utilisateur n'est pas authentifié correctement.");
                    return;
                }

                // News via Categorie
                await _articlesVM.LoadNewsAsync(_articlesVM.SearchTerm, _selectedCategory);

                // Favoris
                _articlesVM.LoadFavoritesFromDatabase(_authenticatedUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des actualités : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeFavoriteButton(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Article article)
            {
                UpdateFavoriteButtonAppearance(button, article.isFavorite);
            }
        }

        private void UpdateFavoriteButtonAppearance(Button button, bool isFavorite)
        {
            if (isFavorite)
            {
                var redColor = (Color)ColorConverter.ConvertFromString("#FF5722");
                button.Content = "Retirer";
                button.Background = new SolidColorBrush(redColor);
                button.BorderBrush = new SolidColorBrush(redColor);
            }
            else
            {
                var blueColor = (Color)ColorConverter.ConvertFromString("#0078D7");
                button.Content = "Favoris";
                button.Background = new SolidColorBrush(blueColor);
                button.BorderBrush = new SolidColorBrush(blueColor);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadNews();
        }

        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Article article)
            {
                ArticleDetailsWindow detailsWindow = new ArticleDetailsWindow(article, _articlesVM, _authenticatedUser);
                detailsWindow.Owner = this;
                detailsWindow.ShowDialog();
            }
        }

        private void ToggleFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Article article)
            {
                _articlesVM.ToggleFavorite(article, _authenticatedUser);
                UpdateFavoriteButtonAppearance(button, article.isFavorite);
            }
        }

        private void ShowFavorites_Click(object sender, RoutedEventArgs e)
        {
            FavoritesWindow favoritesWindow = new FavoritesWindow(_articlesVM, _authenticatedUser);
            favoritesWindow.Show();
            this.Close();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _selectedCategory = selectedItem.Content.ToString();

                if (IsLoaded)
                {
                    LoadNews();
                }
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminLoginAttemptsWindow(_authenticatedUser);
            adminWindow.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Voulez-vous vraiment vous déconnecter ?",
                "Déconnexion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
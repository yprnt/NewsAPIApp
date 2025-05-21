using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace NewsAPIApp
{
    /// <summary>
    /// Logique d'interaction pour ArticleDetailsWindow.xaml
    /// </summary>
    public partial class ArticleDetailsWindow : Window
    {
        private Article _article;
        private ArticlesVM _articlesVM;
        private User _user;
        public ArticleDetailsWindow(Article article, ArticlesVM articlesVM, User user)
        {
            InitializeComponent();
            _article = article;
            _articlesVM = articlesVM;
            _user = user;

            ArticleTitle.Text = article.title;
            ArticleDescription.Text = article.description;
            ArticleDate.Text = $"Publié le {article.publishedAt.ToString("dd/MM/yyyy à HH:mm")}";

            UpdateFavoriteButtonAppearance();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReadFullArticle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _article.url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture du lien : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToggleFavorite_Click(object sender, RoutedEventArgs e)
        {
            _articlesVM.ToggleFavorite(_article, _user);

            UpdateFavoriteButtonAppearance();
        }

        private void UpdateFavoriteButtonAppearance()
        {
            if (_article.isFavorite)
            {
                var redColor = (Color)ColorConverter.ConvertFromString("#FF5722");
                FavoriteButton.Content = "Retirer";
                FavoriteButton.Background = new SolidColorBrush(redColor);
                FavoriteButton.BorderBrush = new SolidColorBrush(redColor);
            }
            else
            {
                var blueColor = (Color)ColorConverter.ConvertFromString("#3B82F6");
                FavoriteButton.Content = "Favoris";
                FavoriteButton.Background = new SolidColorBrush(blueColor);
                FavoriteButton.BorderBrush = new SolidColorBrush(blueColor);
            }
        }
    }
}
using NewsAPI.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace NewsAPIApp
{
    /// <summary>
    /// Logique d'interaction pour FavoritesWindow.xaml
    /// </summary>
    public partial class FavoritesWindow : Window
    {
        private ArticlesVM _articlesVM;
        private User _authenticatedUser;
        public FavoritesWindow(ArticlesVM articlesVM, User authenticatedUser)
        {
            InitializeComponent();
            _articlesVM = articlesVM;
            _authenticatedUser = authenticatedUser;
            DataContext = _articlesVM;

            _articlesVM.LoadFavoritesFromDatabase(_authenticatedUser);
        }

        private void BackToNews_Click(object sender, RoutedEventArgs e)
        {
            ArticlesWindow articlesWindow = new ArticlesWindow(_authenticatedUser);
            articlesWindow.Show();
            this.Close();
        }

        private void RemoveFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Article article)
            {
                _articlesVM.RemoveFavoriteFromDatabase(article, _authenticatedUser);

                _articlesVM.FavoriteNews.Remove(article);

                var mainArticle = _articlesVM.Articles.FirstOrDefault(a => a.url == article.url);
                if (mainArticle != null)
                {
                    mainArticle.isFavorite = false;
                }
            }
        }

        private void ShowFavoriteDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Article article)
            {
                ArticleDetailsWindow detailsWindow = new ArticleDetailsWindow(article, _articlesVM, _authenticatedUser);
                detailsWindow.Owner = this;
                detailsWindow.ShowDialog();

                if (!article.isFavorite)
                {
                    _articlesVM.FavoriteNews.Remove(article);
                }
            }
        }
    }
}
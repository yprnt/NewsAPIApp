using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MySqlConnector;

namespace NewsAPIApp
{
    public class ArticlesVM
    {
        private ObservableCollection<Article> _articles;
        public ObservableCollection<Article> Articles
        {
            get => _articles;
            set
            {
                _articles = value;
            }
        }
        public ObservableCollection<Article> FavoriteNews { get; set; }

        private string apiKey = "Renseignez votre API KEY ici !";

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
            }
        }

        public ArticlesVM()
        {
            Articles = new ObservableCollection<Article>();
            FavoriteNews = new ObservableCollection<Article>();
        }

        public async Task LoadNewsAsync(string query = null, string category = null)
        {
            try
            {
                Articles.Clear();

                var newsApiClient = new NewsApiClient(apiKey);
                var articlesResponse = await Task.Run(() =>
                {
                    var request = new TopHeadlinesRequest
                    {
                        Q = query,
                        Country = Countries.US,
                        Language = Languages.EN,
                        PageSize = 50
                    };

                    if (!string.IsNullOrEmpty(category) && Enum.TryParse(category, out Categories parsedCategory))
                    {
                        request.Category = parsedCategory;
                    }

                    return newsApiClient.GetTopHeadlines(request);
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    foreach (var articleResponse in articlesResponse.Articles)
                    {
                        bool isFavorite = FavoriteNews.Any(a => a.url == articleResponse.Url);

                        var article = new Article(
                            articleResponse.Title ?? "Sans titre",
                            articleResponse.Description ?? "Aucune description disponible",
                            articleResponse.Url ?? "#",
                            articleResponse.UrlToImage,
                            articleResponse.PublishedAt ?? DateTime.Now,
                            isFavorite
                        );

                        Articles.Add(article);
                    }
                }
                else
                {
                    MessageBox.Show($"Erreur lors de la récupération des actualités : {articlesResponse.Error?.Message ?? "Erreur inconnue"}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        public void ToggleFavorite(Article article, User user)
        {
            if (article.isFavorite)
            {
                RemoveFavoriteFromDatabase(article, user);

                Article favoriteToRemove = FavoriteNews.FirstOrDefault(a => a.url == article.url);
                if (favoriteToRemove != null)
                {
                    FavoriteNews.Remove(favoriteToRemove);
                }

                article.isFavorite = false;
            }
            else
            {
                bool alreadyExists = FavoriteNews.Any(a => a.url == article.url);

                if (!alreadyExists)
                {
                    SaveFavoriteToDatabase(article, user);
                    FavoriteNews.Add(article);
                    article.isFavorite = true;
                }
            }
        }

        private void SaveFavoriteToDatabase(Article article, User user)
        {
            try
            {
                using (var connection = Database.getConnection())
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(id) FROM Favorites WHERE user_id = @User_Id AND url = @Url";

                    using (var checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@User_Id", user.id);
                        checkCommand.Parameters.AddWithValue("@Url", article.url);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            return;
                        }
                    }

                    string insertQuery = @"
                INSERT INTO Favorites (user_id, title, description, url, urlToImage, publishedAt, source) 
                VALUES (@User_Id, @Title, @Description, @Url, @UrlToImage, @PublishedAt, @Source)";

                    using (var command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@User_Id", user.id);
                        command.Parameters.AddWithValue("@Title", article.title);
                        command.Parameters.AddWithValue("@Description", article.description);
                        command.Parameters.AddWithValue("@Url", article.url);
                        command.Parameters.AddWithValue("@UrlToImage", article.urlToImage);
                        command.Parameters.AddWithValue("@PublishedAt", article.publishedAt);
                        command.Parameters.AddWithValue("@Source", article.source ?? "Unknown");

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Article ajouté aux favoris avec succès !");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde dans la base de données : {ex.Message}");
            }
        }

        public void LoadFavoritesFromDatabase(User user)
        {
            try
            {
                using (var connection = Database.getConnection())
                {
                    connection.Open();
                    string query = "SELECT title, description, url, urlToImage, publishedAt, source FROM Favorites WHERE user_id = @User_Id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@User_Id", user.id);

                        using (var reader = command.ExecuteReader())
                        {
                            FavoriteNews.Clear();

                            while (reader.Read())
                            {
                                var article = new Article(
                                    reader.GetString("title"),
                                    reader.GetString("description"),
                                    reader.GetString("url"),
                                    reader.GetString("urlToImage"),
                                    reader.GetDateTime("publishedAt"),
                                    true
                                );

                                FavoriteNews.Add(article);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des favoris : {ex.Message}");
            }
        }

        public void RemoveFavoriteFromDatabase(Article article, User user)
        {
            try
            {
                using (var connection = Database.getConnection())
                {
                    connection.Open();

                    string query = "DELETE FROM Favorites WHERE user_id = @User_Id AND url = @Url";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@User_Id", user.id);
                        command.Parameters.AddWithValue("@Url", article.url);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Favori supprimé avec succès !");
                        }
                    }
                }
                article.isFavorite = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du favori : {ex.Message}");
            }
        }
    }
}
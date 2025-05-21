using MySqlConnector;
using System;
using System.Collections.ObjectModel;

namespace NewsAPIApp
{
    public class UserVM
    {
        public ObservableCollection<FailedLoginAttempt> FailedAttempts { get; set; }

        public UserVM()
        {
            FailedAttempts = new ObservableCollection<FailedLoginAttempt>();
        }

        public User Authenticate(User user)
        {
            User authenticatedUser = null;
            string query = "SELECT id, pseudo, firstname, lastname, password FROM user WHERE Pseudo = @Pseudo";

            using (MySqlConnection connection = Database.getConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Pseudo", user.pseudo);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader.GetString("password");
                            int userId = reader.GetInt32("id");
                            string firstname = reader.GetString("firstname");
                            string lastname = reader.GetString("lastname");

                            if (BCrypt.Net.BCrypt.Verify(user.password, storedPasswordHash))
                            {
                                authenticatedUser = new User(
                                    userId,
                                    reader.GetString("pseudo"),
                                    firstname,
                                    lastname
                                );
                            }
                            else
                            {
                                // Mot de passe incorrect, enregistrer la tentative échouée avec l'utilisateur existant
                                User existingUser = new User(userId, user.pseudo, firstname, lastname);
                                LogFailedAttempt(existingUser);
                            }
                        }
                        else
                        {
                            // Utilisateur non trouvé, enregistrer la tentative échouée sans ID utilisateur
                            LogFailedAttempt(null);
                        }
                    }
                }
            }

            return authenticatedUser;
        }

        public bool Inscription(User user)
        {
            bool isValid = false;
            string query = "INSERT INTO user (pseudo, password, firstname, lastname) " +
                "VALUES (@Pseudo, @Password, @FirstName, @LastName);";

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.password);

            using (MySqlConnection connection = Database.getConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Pseudo", user.pseudo);
                    command.Parameters.AddWithValue("@Password", passwordHash);
                    command.Parameters.AddWithValue("@FirstName", user.firstName);
                    command.Parameters.AddWithValue("@LastName", user.lastName);

                    isValid = (command.ExecuteNonQuery() > 0);
                }
            }
            return isValid;
        }

        private void LogFailedAttempt(User user)
        {
            string query = "INSERT INTO failed_login_attempts (user_id, attempt_date) VALUES (@UserId, @AttemptDate)";

            try
            {
                using (MySqlConnection connection = Database.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user?.id != 0 ? (object)user?.id : DBNull.Value);
                        command.Parameters.AddWithValue("@AttemptDate", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erreur lors de l'enregistrement de la tentative échouée : {ex.Message}", "Erreur", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public void LoadFailedAttempts(DateTime fromDate, DateTime toDate)
        {
            FailedAttempts.Clear();
            string query = @"
                SELECT fa.id, fa.user_id, fa.attempt_date, u.pseudo, u.firstname, u.lastname
                FROM failed_login_attempts fa
                LEFT JOIN user u ON fa.user_id = u.id
                WHERE fa.attempt_date BETWEEN @FromDate AND @ToDate
                ORDER BY fa.attempt_date DESC";

            try
            {
                using (MySqlConnection connection = Database.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FromDate", fromDate);
                        command.Parameters.AddWithValue("@ToDate", toDate);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                DateTime attemptDate = reader.GetDateTime("attempt_date");

                                User attemptUser = null;
                                if (!reader.IsDBNull(reader.GetOrdinal("user_id")))
                                {
                                    int userId = reader.GetInt32("user_id");
                                    string pseudo = reader.GetString("pseudo");
                                    string firstname = reader.GetString("firstname");
                                    string lastname = reader.GetString("lastname");
                                    attemptUser = new User(userId, pseudo, firstname, lastname);
                                }

                                FailedAttempts.Add(new FailedLoginAttempt(id, attemptUser, attemptUser?.pseudo ?? "Inconnu", attemptDate));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erreur lors du chargement des tentatives échouées : {ex.Message}", "Erreur", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}
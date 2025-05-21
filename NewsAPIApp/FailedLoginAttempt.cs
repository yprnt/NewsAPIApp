using System;

namespace NewsAPIApp
{
    public class FailedLoginAttempt
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime AttemptDate { get; set; }

        public FailedLoginAttempt(int id, User user, string pseudo, DateTime attemptDate)
        {
            Id = id;
            User = user;
            AttemptDate = attemptDate;
        }

        public FailedLoginAttempt(string pseudo)
        {
            AttemptDate = DateTime.Now;
        }
    }
}
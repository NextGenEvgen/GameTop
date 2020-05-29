using System.Collections.Generic;

namespace GameTop
{
    public class User
    {
        private string nickname;
        private string login;
        private string regDate;
        public string Nickname { get => nickname; }
        public string Login { get => login; }
        public string RegDate { get => regDate; }
        public List<GameScore> Scores { get; set; }
        public User(string nickname, string login, string regDate) 
        {
            this.login = login;
            this.nickname = nickname;
            this.regDate = regDate;
        }
    }
}
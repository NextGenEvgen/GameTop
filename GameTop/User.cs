namespace GameTop
{
    public class User
    {
        private string nickname;
        public string Nickname { get => nickname; }
        public User(string nickname) 
        {
            this.nickname = nickname;
        }
    }
}
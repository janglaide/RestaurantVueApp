namespace WpfApp.Models
{
    public class User
    {
        public static User Instance;
        public int OrderId { get; set; }

        public User()
        {
            Instance ??= this;
        }
    }
}

namespace API.Models
{
    public class Team
    {
        public Team()
        {
            Users = new List<User>(); //Team.Users.add gibi bi kullanımı mümkün hale getirir.
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}

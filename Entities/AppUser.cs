   namespace BackOffice.Entities{

     public class AppUser : IEquatable<AppUser>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public decimal? Salary {get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public bool Equals(AppUser? other)
        {
            return this.Name == other.Name;
        }

        public override string ToString()
        {
            return "Id :" + Id + " Name :" + Name + " Surname :" + Surname;
        }
    }
   }
   
  
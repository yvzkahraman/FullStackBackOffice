namespace BackOffice.Data
{
    public static class DbData
    {

        // hash şifreleme 
        // 1 => abjcs 
        // request.password => şifreleme => abjcs 

        //VERİTABANI 
        public static List<AppUser> UserList = new List<AppUser>(){
            new (){ Id =1,Firstname ="Emrullah",Lastname="Işık", Username="Emrullah", Password ="1"},
            new (){Id=2, Firstname ="Ömer Faruk", Lastname="Pala",Username="Ömer", Password ="1"},
             new (){Id=2, Firstname ="Mustafa", Lastname="H",Username="Mustafa", Password ="1"},
        };

    }

    public class AppUser : IEquatable<AppUser>
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public bool Equals(AppUser? other)
        {
            return this.Firstname == other.Firstname;
        }

        public override string ToString()
        {
            return "Id :" + Id + " Firstname :" + Firstname + " Lastname :" + Lastname;
        }
    }
}
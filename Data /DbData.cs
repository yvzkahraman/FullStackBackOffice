namespace BackOffice.Data
{
    public static class DbData
    {

        //VERİTABANI 
        public static List<AppUser> UserList = new List<AppUser>(){
            new (){ Id =1,Firstname ="Emrullah",Lastname="Işık"},
            new (){Id=2, Firstname ="Ömer Faruk", Lastname="Pala"},
        };

    }

    public class AppUser
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public override string ToString()
        {
            return "Id :"+Id+" Firstname :"+Firstname+" Lastname :"+Lastname;
        }
    }
}
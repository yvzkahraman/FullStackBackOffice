using BackOffice.Entities;

namespace BackOffice.Data
{
    public static class DbData
    {
        public static List<AppUser> UserList = new List<AppUser>(){
            new (){ Id =1,Name ="Emrullah",Surname="Işık", Username="Emrullah", Password ="1"},
            new (){Id=2, Name ="Ömer Faruk", Surname="Pala",Username="Ömer", Password ="1"},
             new (){Id=2, Name ="Mustafa", Surname="H",Username="Mustafa", Password ="1"},
        };

    }

 
}
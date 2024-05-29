using BackOffice.Entities;
using BackOffice.Interfaces;

namespace BackOffice.Repos{
    public class UserEfRepo : IRepo
    {
        private readonly ReactDbContext context;

        public UserEfRepo(ReactDbContext context)
        {
            this.context = context;
        }

        public List<AppUser> GetAll(){
          
            return this.context.Users.ToList();
        }
      
    }
}
using BackOffice.Entities;

namespace BackOffice.Interfaces
{
    public interface IRepo
    {
        public List<AppUser> GetAll();
    }
}
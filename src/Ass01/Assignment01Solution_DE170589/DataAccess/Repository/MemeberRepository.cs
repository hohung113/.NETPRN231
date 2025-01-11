using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemeberRepository : IMemberRepository
    {
        public Member CurrentMember => throw new NotImplementedException();

        public void AddMemeber(Member member)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin()
        {
            throw new NotImplementedException();
        }

        public bool IsLoggedIn()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool LoginAdmin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}

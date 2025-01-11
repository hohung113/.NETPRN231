using BusinessObject;
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
        private bool _isAdmin;
        public void AddMemeber(Member member)
        {
            MemberDAO.Instance.AddMember(member);
        }

        public bool IsAdmin()
        {
            return _isAdmin;
        }

        public  bool Login(string email, string password)
        {
            bool result = false;
            var member = MemberDAO.Instance.GetMemberByEmail(email);
            if (member != null && member.Password.Equals(password))
            {
                result = true;
            }
            return result;
        }

        public bool LoginAdmin(string username, string password)
        {
            bool result = false;
            if (username.Trim().Equals(Helper.GetString("EmailAdmin")) && password.Equals(Helper.GetString("PassAdmin")))
            {
                result = true;
                this._isAdmin = true;
            }
            else
            {
                this._isAdmin = false;
            }

            return result;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member member)
        {
            MemberDAO.Instance.UpdateMember(member);
        }
    }
}

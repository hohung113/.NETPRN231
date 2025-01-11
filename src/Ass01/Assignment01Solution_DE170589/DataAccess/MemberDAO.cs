using BusinessObject;
using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        EStoreDbContext _context = new EStoreDbContext();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public  Member GetMemberByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            var member =  _context.Members.SingleOrDefault(m => m.Email == email);

            return member;
        }

        public IEnumerable<Member> GetMembers()
        {
            var listMembers = _context.Members.ToList();
            return listMembers.Any() ? listMembers : Enumerable.Empty<Member>();
        }

        public void AddMember(Member member)
        {
            ArgumentNullException.ThrowIfNull(member, nameof(member));
            try
            {
                _context.Members.Add(member);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the member.", ex);
            }
        }

        public  void UpdateMember(Member member)
        {
            ArgumentNullException.ThrowIfNull(member, nameof(member));
            try
            {
                // context.Entry<Member>>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Members.Update(member);
                 _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while update the member.", ex);
            }
        }

        public void DeleteMember(Member member)
        {
            ArgumentNullException.ThrowIfNull(member, nameof(member));
            try
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while delete the member.", ex);
            }
        }
    }
}

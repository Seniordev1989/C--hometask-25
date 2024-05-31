using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class UserDetailsRepository
    {
        private readonly ApplicationDbContext _context=new ApplicationDbContext();

        public List<UserDetails> GetAllUserDetails()
        {
            var result=_context.UserDetails.ToList();
            
            return result;
        }

        public bool Create(UserDetails usrobj)
        {
            _context.UserDetails.Add(usrobj);
            _context.SaveChanges();
            return true;
        }

        public UserDetails GetById(int Id)
        {
            var result = _context.UserDetails.Where(s => s.Id == Id).FirstOrDefault();

            return result;
        }
        public bool Update(UserDetails usrdata)
        {
            var usrdataobj = _context.UserDetails.Find(usrdata.Id);
            if (usrdataobj == null)
                return false;
            usrdataobj.FirstName = usrdata.FirstName;
            usrdataobj.Surname = usrdata.Surname;
            usrdataobj.Age = usrdata.Age;
            usrdataobj.Sex = usrdata.Sex;
            usrdataobj.Mobile = usrdata.Mobile;
            usrdataobj.Active = usrdata.Active;
            _context.Entry(usrdataobj).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        public bool Delete(int Id)
        {

            var usrdataobj = _context.UserDetails.Find(Id);
            if (usrdataobj == null)
                return false;
            _context.Entry(usrdataobj).State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }
    }
}
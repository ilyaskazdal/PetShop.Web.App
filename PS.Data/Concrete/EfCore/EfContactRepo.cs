using PS.Data.Abstract;
using PS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Concrete.EfCore
{
    public class EfContactRepo : IContactRepo
    {
        private readonly PSContext _context;

        public EfContactRepo(PSContext context)
        {
            _context = context;
        }

        public IQueryable<Contact> Contacts => _context.Contacts;

        public void CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

       
    }
}

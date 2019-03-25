using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Models;

namespace WebApiExample.Repositories
{
    public class PersonRepository:IPersonRepository
    {
        private readonly PersondbContext _context;

        public PersonRepository(PersondbContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            _context.Person.Add(person);
            _context.SaveChanges();
            return person;
        }
        
        public List<Person> Read()
        {
            //SELECT * FROM PERSON;
            return _context.Person
                .AsNoTracking()
                .Include(p => p.Phone)
                .ToList();

            //return _context.Person.FromSql("SELECT Person.Name FROM Person").ToList();
            //return _context.Person.FromSql("SELECT PERSON.*, PHONE.* " +
            //                              "FROM PERSON INNER JOIN PHONE ON PERSON.ID = PHONE.PERSONID").ToList();
        }

        public Person Read(int id)
        {
            //SELECT * FROM PERSON WHERE ID = {id};

            //SELECT PERSON.*, PHONE.*
            //FROM PERSON INNER JOIN PHONE ON PERSON.ID = PHONE.PERSONID
            //WHERE PERSON.ID={id};

            return _context.Person
                .AsNoTracking()
                .Include(p=>p.Phone)                
                .FirstOrDefault(p => p.Id == id);
        }

        public Person Update(Person person)
        {
            _context.Person.Update(person);
            _context.SaveChanges();
            return person;
        }

        public void Delete(int id)
        {
            var deletedPerson = Read(id);
            _context.Person.Remove(deletedPerson);
            _context.SaveChanges();
        }
    }
}

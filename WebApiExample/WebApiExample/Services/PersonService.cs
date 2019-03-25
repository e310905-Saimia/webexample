using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApiExample.Models;
using WebApiExample.Repositories;
using WebApiExample.Utilities;


namespace WebApiExample.Services
{
    public class PersonService:IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public PersonService()
        {
        }

        public Person Create(Person person)
        {
            person.Psw = PasswordHash.HashPassword(person.Psw, "QWERTY");
            return _personRepository.Create(person);
        }

        public List<Person> Read()
        {
            return _personRepository.Read();
        }

        public Person Read(int id)
        {
            return _personRepository.Read(id);
        }

        public Person Update(int id, Person person)
        {
            var savedPerson = _personRepository.Read(id);
            if(savedPerson==null)
                throw new Exception(StatusCodes.Status404NotFound.ToString());
            
            return _personRepository.Update(person);
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

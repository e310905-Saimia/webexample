using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Models;
using WebApiExample.Repositories;
using WebApiExample.Services;

namespace WebApiExample.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonService _personService;

        public PersonsController(IPersonRepository personRepository, IPersonService personService)
        {
            _personRepository = personRepository;
            _personService = personService;
        }


        // GET: api/persons
        //[HttpGet]
        //public ActionResult Testi()
        //{            
        //        return NotFound();
        //    //return Ok();
        //}

        //public bool TestiReturnTrue()
        //{
        //    return true;
        //    //return Ok();
        //}

        [HttpGet]
        public ActionResult<List<Person>> GetPersons()
        {
            var persons = _personService.Read();

            if (persons == null)
            {
                return NotFound();
            }

            return Ok(new JsonResult(persons));
            
        }

        // GET: api/persons/5
        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            var person = _personService.Read(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(_personService.Read(id)));
        }


        //POST:api/persons
        [HttpPost]
        public ActionResult<Person> Post(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPerson = _personService.Create(person);
            if (newPerson == null)
                return NoContent();

            return Ok(new JsonResult(newPerson));
        }

        //PUT:api/persons/5
        [HttpPut("{id}")]
        public ActionResult<Person> Put(int id, Person person)
        {

            try
            {
                var updatedPerson = _personService.Update(id, person);
                return updatedPerson;
            }
            catch
            {
                return NotFound();
            }

        }

        //DELETE:api/persons/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            _personService.Delete(id);
            return new NoContentResult();
        }
    }
}
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonResponse> GetPersonByDataAsync(string mail, string password)
        {
            var personList = await _personRepository.ListAsync();

            var existingPerson = personList.Where(p =>p.Mail == mail).First();

            if (existingPerson == null)
                return new PersonResponse("User can not be find");

            if (existingPerson.Password != password)
                return new PersonResponse("Password incorrect");

            return new PersonResponse(existingPerson);
        }

        public Task<PersonResponse> GetPersonById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

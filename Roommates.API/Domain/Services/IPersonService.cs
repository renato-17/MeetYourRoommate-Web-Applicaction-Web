using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IPersonService
    {
        public Task<PersonResponse> GetPersonByDataAsync(string mail, string password);
        public Task<PersonResponse> GetPersonById(int id);
    }
}

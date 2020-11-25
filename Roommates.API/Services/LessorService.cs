using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using Roommates.API.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class LessorService : ILessorService
    {
        private readonly ILessorRepository _lessorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly IPersonRepository _personRepository;

        public LessorService(ILessorRepository lessorRepository, IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IPersonRepository personRepository)
        {
            _lessorRepository = lessorRepository;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _personRepository = personRepository;
        }

 

        public async Task<LessorResponse> GetByIdAsync(int id)
        {
            var existingLessor = await _lessorRepository.FindById(id);
            if (existingLessor == null)
                return new LessorResponse("Lessor not found");
            return new LessorResponse(existingLessor);
        }

        public async Task<IEnumerable<Lessor>> ListAsync()
        {
            return await _lessorRepository.ListAsync();
        }

        public async Task<LessorResponse> RemoveAsync(int id)
        {
            var existingLessor = await _lessorRepository.FindById(id);
            if (existingLessor == null)
                return new LessorResponse("Lessor not found");
            try
            {
                _lessorRepository.Remove(existingLessor);
                await _unitOfWork.CompleteAsync();
                return new LessorResponse(existingLessor);
            }
            catch(Exception ex)
            {
                return new LessorResponse($"An error ocurred while removing lessor: {ex.Message}");
            }
        }

        public async Task<LessorResponse> SaveAsync(Lessor lessor)
        {
            var person = await _personRepository.FindByDni(lessor.Dni);
            if (person != null)
                return new LessorResponse("Dni already exists");

            person = await _personRepository.FindByMail(lessor.Mail);
            if (person != null)
                return new LessorResponse("Mail already exists");

            person = await _personRepository.FindByPhone(lessor.Phone);
            if (person != null)
                return new LessorResponse("Phone already exists");

            try
            {
                await _lessorRepository.AddAsync(lessor);
                await _unitOfWork.CompleteAsync();
                return new LessorResponse(lessor);
            }
            catch(Exception ex)
            {
                return new LessorResponse($"An error ocurred while saving lessor: {ex.InnerException} ");
            }
        }

        public async Task<LessorResponse> UpdateAsync(int id, Lessor lessor)
        {
            var existingLessor = await _lessorRepository.FindById(id);
            if (existingLessor == null)
                return new LessorResponse("Lessor not found");

            existingLessor.FirstName = lessor.FirstName;
            existingLessor.LastName = lessor.LastName;
            existingLessor.Dni = lessor.Dni;
            existingLessor.Phone = lessor.Phone;
            existingLessor.Gender = lessor.Gender;
            existingLessor.Address = lessor.Address;
            existingLessor.Birthdate = lessor.Birthdate;
            existingLessor.Premium = lessor.Premium;
            existingLessor.Mail = lessor.Mail;
            existingLessor.Password = lessor.Password;
            
            try
            {
                _lessorRepository.Update(existingLessor);
                await _unitOfWork.CompleteAsync();
                return new LessorResponse(existingLessor);
            }
            catch(Exception ex)
            {
                return new LessorResponse($"An error ocurred while updating lessor: {ex.InnerException}");
            }

        }

        public async Task<LessorAuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var lessors = await ListAsync();

            var lessor = lessors.SingleOrDefault(s =>
                s.Mail == request.Mail &&
                s.Password == request.Password
                );

            if (lessor == null) return null;

            var token = GenerateJwtToken(lessor);
            return new LessorAuthenticationResponse(lessor, token);
        }

        private string GenerateJwtToken(Lessor lessor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, lessor.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

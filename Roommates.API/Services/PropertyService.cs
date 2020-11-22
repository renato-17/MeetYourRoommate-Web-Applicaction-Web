using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ILessorRepository _lessorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IPropertyRepository propertyRepository, ILessorRepository lessorRepository, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _lessorRepository = lessorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PropertyResponse> GetByIdAndLessorIdAsync(int lessorId, int id)
        {
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId,id);
            if (existingProperty == null)
                return new PropertyResponse("Property or Lessor not found");
            return new PropertyResponse(existingProperty);
        }

        public async Task<IEnumerable<Property>> ListByLessorIdAsync(int lessorId)
        {
            return await _propertyRepository.ListByLessorIdAsync(lessorId);
        }

        public async Task<IEnumerable<Property>> ListAsync()
        {
            return await _propertyRepository.ListAsync();
        }

    

        public async Task<PropertyResponse> SaveAsync(int lessorId, Property property)
        {
            var existingLessor = await _lessorRepository.FindById(lessorId);
            if (existingLessor == null)
                return new PropertyResponse("Lessor not found");

            property.Lessor = existingLessor;
            property.LessorId = lessorId;

            try
            {

                await _propertyRepository.AddAsync(property);
                await _unitOfWork.CompleteAsync();
                return new PropertyResponse(property);
            }
            catch(Exception ex)
            {
                return new PropertyResponse($"An error ocurred while saving property: {ex.Message}");
            }

        }

        public async Task<PropertyResponse> UpdateAsync(int lessorId, int id, Property property)
        {
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId,id);
            if (existingProperty == null)
                return new PropertyResponse("Property not found");

            existingProperty.Address = property.Address;
            existingProperty.Description = property.Description;

            try
            {
                _propertyRepository.Update(existingProperty);
                await _unitOfWork.CompleteAsync();
                return new PropertyResponse(existingProperty);
            }
            catch(Exception ex)
            {
                return new PropertyResponse($"An error ocurred while updating property: {ex.Message}");
            }
        }

        public async Task<PropertyResponse> RemoveAsync(int id)
        {
            var existingProperty = await _propertyRepository.FindById(id);

            if (existingProperty == null)
                return new PropertyResponse("Property not found");

            try
            {
                _propertyRepository.Remove(existingProperty);
                await _unitOfWork.CompleteAsync();
                return new PropertyResponse(existingProperty);
            }
            catch (Exception ex)
            {
                return new PropertyResponse($"An error ocurred while removing property: {ex.Message}");
            }
        }

        public async Task<PropertyResponse> GetByIdAsync(int id)
        {
            var existingProperty = await _propertyRepository.FindById(id);
            if (existingProperty == null)
                return new PropertyResponse("Property not found");

            return new PropertyResponse(existingProperty);
        }

    }
}

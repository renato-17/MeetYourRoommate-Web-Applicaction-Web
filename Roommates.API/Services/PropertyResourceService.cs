using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class PropertyResourceService : IPropertyResourceService
    {
        private readonly IPropertyResourceRepository _propertyResourceRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyResourceService(IPropertyResourceRepository propertyResourceRepository, IUnitOfWork unitOfWork, IPropertyRepository propertyRepository)
        {
            _propertyResourceRepository = propertyResourceRepository;
            _unitOfWork = unitOfWork;
            _propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<PropertyResource>> ListByPropertyDetailId(int lessorId, int propertyId)
        {
            var property = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);

            var propertyDetail = property.PropertyDetail;
            return await _propertyResourceRepository.ListByPropertyDetailId(propertyDetail.Id);
        }

        public async Task<PropertyResourceResponse> FindByIdAndPropertyDetailId(int lessorId, int propertyId, int id)
        {
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);

            if (existingProperty == null)
                return new PropertyResourceResponse("Property not found");

            if (existingProperty.PropertyDetail == null)
                return new PropertyResourceResponse("This property does not have property detail");

            var existingPropertyResource = await _propertyResourceRepository.FindByIdAndPropertyDetailId(existingProperty.PropertyDetail.Id, id);

            return new PropertyResourceResponse(existingPropertyResource);
        }

        public async Task<PropertyResourceResponse> SaveAsync(int lessorId, int propertyId, PropertyResource propertyResource)
        {
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);

            if (existingProperty == null)
                return new PropertyResourceResponse("Property not found");

            if (existingProperty.PropertyDetail == null)
                return new PropertyResourceResponse("This property does not have property detail");

            propertyResource.PropertyDetail = existingProperty.PropertyDetail;
            propertyResource.PropertyDetailId = existingProperty.PropertyDetail.Id;

            try
            {
                await _propertyResourceRepository.AddAsync(propertyResource);
                await _unitOfWork.CompleteAsync();

                return new PropertyResourceResponse(propertyResource);
            }
            catch (Exception e)
            {
                return new PropertyResourceResponse($"An error ocurred while saving Property Resource: {e.Message}");
            }
        }

        public async Task<PropertyResourceResponse> UpdateAsync(int lessorId, int propertyId, int id, PropertyResource propertyResource)
        {
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);

            if (existingProperty == null)
                return new PropertyResourceResponse("Property not found");

            if (existingProperty.PropertyDetail == null)
                return new PropertyResourceResponse("This property does not have property detail");

            var existingPropertyResource = await _propertyResourceRepository.FindByIdAndPropertyDetailId(existingProperty.PropertyDetail.Id, id);

            existingPropertyResource.Type = propertyResource.Type;

            try
            {
                _propertyResourceRepository.Update(existingPropertyResource);
                await _unitOfWork.CompleteAsync();

                return new PropertyResourceResponse(existingPropertyResource);
            }
            catch (Exception e)
            {
                return new PropertyResourceResponse($"An error ocurred while updating Property Resource: {e.Message}");
            }
        }

        public async Task<PropertyResourceResponse> DeleteAsync(int lessorId, int propertyId, int id)
        {
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);

            if (existingProperty == null)
                return new PropertyResourceResponse("Property not found");

            if (existingProperty.PropertyDetail == null)
                return new PropertyResourceResponse("This property does not have property detail");

            var existingPropertyResource = await _propertyResourceRepository.FindByIdAndPropertyDetailId(existingProperty.PropertyDetail.Id, id);

            try
            {
                _propertyResourceRepository.Delete(existingPropertyResource);
                await _unitOfWork.CompleteAsync();

                return new PropertyResourceResponse(existingPropertyResource);
            }
            catch (Exception e)
            {
                return new PropertyResourceResponse($"An error ocurred while deleting Property Resource: {e.Message}");
            }
        }
    }
}

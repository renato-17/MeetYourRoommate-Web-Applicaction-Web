using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class PropertyDetailService : IPropertyDetailService
    {
        private readonly IPropertyDetailRepository _propertyDetailRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyDetailService(IPropertyDetailRepository propertyDetailRepository, IUnitOfWork unitOfWork, IPropertyRepository propertyRepository)
        {
            _propertyDetailRepository = propertyDetailRepository;
            _unitOfWork = unitOfWork;
            _propertyRepository = propertyRepository;
        }
        public async Task<PropertyDetailResponse> GetPropertyDetailAsync(int lessorId, int propertyId)
        {
            var existingPropertyDetail = await _propertyDetailRepository.GetPropertyDetail(propertyId);

            if (existingPropertyDetail == null)
                return new PropertyDetailResponse("This property does not have property detail");

            return new PropertyDetailResponse(existingPropertyDetail);
        }

        public async Task<PropertyDetailResponse> SaveAsync(int lessorId, int propertyId ,PropertyDetail propertyDetail)
        {
            var exisitingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);

            if (exisitingProperty == null)
                return new PropertyDetailResponse("Property not found");

            propertyDetail.Property = exisitingProperty;
            propertyDetail.PropertyId = propertyId;
           
            try
            {
                await _propertyDetailRepository.AddAsync(propertyDetail);
                await _unitOfWork.CompleteAsync();

                return new PropertyDetailResponse(propertyDetail);
            }
            catch (Exception e)
            {
                return new PropertyDetailResponse($"An error ocurred while saving Property Detail: {e.Message}");
            }
        }

        public async Task<PropertyDetailResponse> UpdateAsync(int lessorId, int propertyId, PropertyDetail propertyDetail)
        {
            var exisitingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);
            if (exisitingProperty == null)
                return new PropertyDetailResponse("Property not found");

            var existingPropertyDetail = await _propertyDetailRepository.GetPropertyDetail(propertyId);
            if (existingPropertyDetail == null)
                return new PropertyDetailResponse("Property Detail not found");

            existingPropertyDetail.Bathrooms = propertyDetail.Bathrooms;
            existingPropertyDetail.Kitchen = propertyDetail.Kitchen;
            existingPropertyDetail.Livingroom = propertyDetail.Livingroom;
            existingPropertyDetail.Price = propertyDetail.Price;
            existingPropertyDetail.Rooms = propertyDetail.Rooms;
            existingPropertyDetail.SquareMeters = propertyDetail.SquareMeters;

            try
            {
                _propertyDetailRepository.UpdateAsync(existingPropertyDetail);
                await _unitOfWork.CompleteAsync();

                return new PropertyDetailResponse(existingPropertyDetail);
            }
            catch (Exception e)
            {
                return new PropertyDetailResponse($"An error ocurred while updating Property Detail: {e.Message}");
            }
        }


        public async Task<PropertyDetailResponse> DeleteAsync(int lessorId, int propertyId)
        {
            var exisitingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);
            if (exisitingProperty == null)
                return new PropertyDetailResponse("Property not found");

            var existingPropertyDetail = await _propertyDetailRepository.GetPropertyDetail(propertyId);
            if (existingPropertyDetail == null)
                return new PropertyDetailResponse("Property Detail not found");

            try
            {
                _propertyDetailRepository.RemoveAsync(existingPropertyDetail);
                await _unitOfWork.CompleteAsync();

                return new PropertyDetailResponse(existingPropertyDetail);
            }
            catch (Exception e)
            {
                return new PropertyDetailResponse($"An error ocurred while deleting Property Detail: {e.Message}");
            }
        }

    }
}

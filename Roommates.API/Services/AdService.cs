using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ILessorRepository _lessorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdService(IAdRepository adRepository, IPropertyRepository propertyRepository, ILessorRepository lessorRepository, IUnitOfWork unitOfWork)
        {
            _adRepository = adRepository;
            _propertyRepository = propertyRepository;
            _lessorRepository = lessorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<AdResponse> DeleteAsync(int id, int lessorId, int propertyId)
        {
            var existingAd = await _adRepository.FindByIdAndLessorIdAndPropertyId(id, lessorId, propertyId);
            if (existingAd == null)
                return new AdResponse("Ad not found");
            try
            {
                _adRepository.Remove(existingAd);
                await _unitOfWork.CompleteAsync();
                return new AdResponse(existingAd);
            }
            catch (Exception ex)
            {
                return new AdResponse($"An error ocurred while deleting ad: { ex.Message}");
            }
        }

        public async Task<AdResponse> GetById(int id)
        {
            var existingAd = await _adRepository.FindById(id);
            if (existingAd == null)
                return new AdResponse("Ad not found");
            return new AdResponse(existingAd);
        }

        public async Task<AdResponse> GetByIdAndLessorIdAndPropertyIdAsync(int id, int lessorId, int propertyId)
        {
            var existingAd = await _adRepository.FindByIdAndLessorIdAndPropertyId(id, lessorId, propertyId);
            if (existingAd == null)
                return new AdResponse("Ad not found");
            return new AdResponse(existingAd);
        }

        public async Task<IEnumerable<Ad>> ListAsync()
        {
            return await _adRepository.ListAsync();
        }

        public async Task<IEnumerable<Ad>> ListByLessorIdAndPropertyIdAsync(int lessorId, int propertyId)
        {
            return await _adRepository.ListByLessorIdAndPropertyIdAsync(lessorId,propertyId);
        }

        public async Task<AdResponse> SaveAsync(Ad ad,int lessorId, int propertyId)
        {
            var existingLessor = await _lessorRepository.FindById(lessorId);
            if (existingLessor == null)
                return new AdResponse("Lessor not found");
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);
            if (existingProperty == null)
                return new AdResponse("Property not found");
            ad.Lessor = existingLessor;
            ad.LessorId = existingLessor.Id;
            ad.Property = existingProperty;
            ad.PropertyId = existingProperty.Id;
            ad.Likes = 0;
            ad.Views = 0;
            ad.Close = false;
            try
            {
                await _adRepository.AddAsync(ad);
                await _unitOfWork.CompleteAsync();
                return new AdResponse(ad);
            }
            catch(Exception ex)
            {
                return new AdResponse($"An error ocurred while saving ad: {ex.Message}");
            }
        }


        public async Task<AdResponse> UpdateAsync(Ad ad, int id, int lessorId, int propertyId)
        {
            var existingLessor = await _lessorRepository.FindById(lessorId);
            if (existingLessor == null)
                return new AdResponse("Lessor not found");
            var existingProperty = await _propertyRepository.FindByIdAndLessorId(lessorId, propertyId);
            if (existingProperty == null)
                return new AdResponse("Property not found");
            var existingAd = await _adRepository.FindByIdAndLessorIdAndPropertyId(id, lessorId,propertyId);
            existingAd.Title = ad.Title;
            try
            {
                _adRepository.Update(existingAd);
                await _unitOfWork.CompleteAsync();
                return new AdResponse(existingAd);
            }
            catch(Exception ex)
            {
                return new AdResponse($"An error ocurred while updating ad: {ex.Message}");
            }
        }
    }
}

using AutoMapper;
using JobFinder.Data.Models.ViewsModels;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;

namespace JobFinder.Services.Implementations
{
    public class CompanySubscriptionsService : ICompanySubscriptionsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompanySubscriptionsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CompanyJobAdsForSubscribersViewModel>> GetLatesJobAds()
        {
            IEnumerable<CompanyJobAdsForSubscribersDTO> jobAds = await this.unitOfWork.CompanySubscriptionsRepository
                .GetCompanyAdsBySubscriptions();

            return this.mapper.Map<IEnumerable<CompanyJobAdsForSubscribersViewModel>>(jobAds);
        }

        public async Task Subscribe(int companyId, string userId)
        {
            await this.unitOfWork.CompanySubscriptionsRepository.Subscribe(userId, companyId);
            await this.unitOfWork.SaveChanges();
        }

        public async Task Unsubscribe(int companyId, string userId)
        {
            await this.unitOfWork.CompanySubscriptionsRepository.Delete(userId, companyId);
            await this.unitOfWork.SaveChanges();
        }

        public async Task UnsubscribeAll(string userId)
        {
            this.unitOfWork.CompanySubscriptionsRepository.DeleteSubscriptions(userId);
            await this.unitOfWork.SaveChanges();          
        }

        public async Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(string userId)
        {
            IEnumerable<CompanySubscriptionDTO> subscriptions = await this.unitOfWork.CompanySubscriptionsRepository
                .GetMySubscriptions(userId);

            return this.mapper.Map<IEnumerable<CompanySubscriptionViewModel>>(subscriptions);
        }
    }
}

using JobFinder.Data.Models.ViewsModels;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;

namespace JobFinder.Services.Implementations
{
    public class CompanySubscriptionsService : ICompanySubscriptionsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;

        public CompanySubscriptionsService(IEntityFrameworkUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CompanyJobAdsForSubscribersViewData>> GetLatesJobAds()
        {
            return await this.unitOfWork.CompanySubscriptionsRepository.GetCompanyAdsBySubscriptions();
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
            return await this.unitOfWork.CompanySubscriptionsRepository.GetMySubscriptions(userId);
        }
    }
}

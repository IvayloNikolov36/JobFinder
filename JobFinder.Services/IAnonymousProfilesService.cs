﻿using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.JobAds;

namespace JobFinder.Services;

public interface IAnonymousProfilesService
{
    Task<string> Create(string cvId, string userId, AnonymousProfileCreateViewModel profile);

    Task Delete(string anonymousProfileId, string userId);

    Task<AnonymousProfileDataViewModel> GetAnonymousProfile(
        string anonymousProfileId,
        int jobAdId,
        string currentUserId);

    Task<MyAnonymousProfileDataViewModel> GetMyAnonymousProfileData(string userId);

    Task<string> GetOwnerId(string id);

    Task<bool> IsRelevant(string id, JobAdCriteriasViewModel jobAdCriterias);
}

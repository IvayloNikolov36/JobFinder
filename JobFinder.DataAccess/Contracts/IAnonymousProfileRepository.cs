﻿using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.DataAccess.Contracts;

public interface IAnonymousProfileRepository
{
    Task Create(string cvId, string userId, AnonymousProfileCreateDTO anonymousProfileDto);

    Task Delete(string anonymousProfileId);

    Task DeleteAnonymousProfile(string cvId);

    Task<AnonymousProfileDataDTO> GetAnonymousProfile(string anonymousProfileId);

    Task<AnonymousProfileDataDTO> GetAnonymousProfileData(string userId);

    Task<MyAnonymousProfileDataDTO> GetMyAnonymousProfileData(string userId);

    Task<string> GetCvId(string id);

    Task<string> GetOwnerId(string id);

    Task<IEnumerable<AnonymousProfileListingDTO>> GetProfilesRelevantToJobAd(JobAdCriteriasDTO jobAdCriterias);

    Task<bool> IsAnonymousProfileRelevantForJobAd(string id, JobAdCriteriasDTO jobAdCriterias);

    Task<bool> HasAnonymousProfile(string userId);
}

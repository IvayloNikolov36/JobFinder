﻿namespace JobFinder.Transfer.DTOs.User;

public class UserProfileDataDTO
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string PictureUrl { get; set; }

    public int CVsCount { get; set; }

    public int SubscriptionsCount { get; set; }

    public int ApplicationsCount { get; set; }
}

﻿namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System;

    public class EducationListingModel : IMapFrom<Education>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Organization { get; set; }

        public string Location { get; set; }

        public string EducationLevel { get; set; }

        public string Major { get; set; }

        public string MainSubjects { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Education, EducationListingModel>()
               .ForMember(x => x.EducationLevel, m => m.MapFrom(m => m.EducationLevel.ToString()));
        }
    }
}

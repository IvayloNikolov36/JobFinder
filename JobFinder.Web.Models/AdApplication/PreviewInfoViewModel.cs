using System;

namespace JobFinder.Web.Models.AdApplication
{
    public class PreviewInfoViewModel
    {
        public PreviewInfoViewModel(DateTime previewDate)
        {
            this.PreviewDate = previewDate;
        }

        public DateTime PreviewDate { get; set; }
    }
}

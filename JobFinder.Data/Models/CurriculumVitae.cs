using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.Data.Models
{
    public class CurriculumVitae
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }

        public string UploaderId { get; set; }

        public User Uploader { get; set; }
    }
}

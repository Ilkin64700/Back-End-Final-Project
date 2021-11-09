using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Slider : BaseEntity
    {
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        [StringLength(maximumLength: 100)]
        public string BackImage { get; set; }

        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        [StringLength(maximumLength: 100)]
        public string TitleTwo { get; set; }

        [StringLength(maximumLength: 250)]
        public string Subtitle { get; set; }
        public int Order { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public IFormFile BackImageFile { get; set; }
    }
}
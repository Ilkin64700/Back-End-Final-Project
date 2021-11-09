using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Setting : BaseEntity
    {
        [StringLength(maximumLength: 100)]
        public string Logo { get; set; }

        [StringLength(maximumLength: 150)]
        public string Question { get; set; }

        [StringLength(maximumLength: 500)]
        public string FooterDesc { get; set; }

        [StringLength(maximumLength: 35)]
        public string Phone { get; set; }

        [StringLength(maximumLength: 35)]
        public string InstagramURL { get; set; }

        [StringLength(maximumLength: 35)]
        public string PinterestURL { get; set; }

        [StringLength(maximumLength: 35)]
        public string TwitterURL { get; set; }

        [StringLength(maximumLength: 35)]
        public string Phone2 { get; set; }

        [StringLength(maximumLength: 35)]
        public string Phone3 { get; set; }

        [StringLength(maximumLength: 35)]
        public string Adress { get; set; }

        [StringLength(maximumLength: 100)]
        public string FooterLogo { get; set; }

        [StringLength(maximumLength: 100)]
        public string Bannerphoto { get; set; }

        [NotMapped]
        public IFormFile Logofile { get; set; }

        [NotMapped]
        public IFormFile FooterLogofile { get; set; }

        [NotMapped]
        public IFormFile Bannerphotofile { get; set; }
    }
}
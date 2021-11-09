using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Feature:BaseEntity
    {
        [Required]
        public string Start { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string ClassDuration { get; set; }

        [Required]
        public string SkillLevel { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Student { get; set; }

        [Required]
        public string Assesment { get; set; }

    }
}

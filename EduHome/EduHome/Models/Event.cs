using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Event:BaseEntity
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Time { get; set; }
        [StringLength(maximumLength: 500)]
        [Required]
        public string Venue { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string Desc { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<int> SpeakerIds { get; set; }
        public Category Category { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}

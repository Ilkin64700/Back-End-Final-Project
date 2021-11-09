using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Course> Courses { get; set; }
        public List<Promotion> Promotions { get; set; }
        public List<Event> Events { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Subscribe> Subscribes { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
        public List<Setting> Settings { get; set; }
    }
}

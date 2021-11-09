using EduHome.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }


    }
}
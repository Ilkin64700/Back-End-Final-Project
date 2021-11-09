using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class EventViewModel
    {
        public List<Event> Events { get; set; }
        public List<Subscribe> Subscribes { get; set; }
        public List<Setting> Settings { get; set; }
    }
}

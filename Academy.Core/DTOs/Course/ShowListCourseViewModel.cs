using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Core.DTOs.Course
{
    public class ShowListCourseViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}


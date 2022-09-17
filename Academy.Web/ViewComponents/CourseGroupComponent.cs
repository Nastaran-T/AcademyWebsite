using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Course;

namespace Academy.Web.ViewComponents
{
    public class CourseGroupComponent:ViewComponent
    {
        private ICourseService _courseService;
        public CourseGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return await Task.FromResult((IViewComponentResult) View("CourseGroup",_courseService.GetAllCourses()));
        }
    }
}

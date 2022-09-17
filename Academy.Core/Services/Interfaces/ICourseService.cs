using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Academy.Core.DTOs.Course;
using Academy.DataLayer.Entities.Course;
using Academy.DataLayer.Entities.User;

namespace Academy.Core.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseGroup> GetAllCourses();
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int groupId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatus();
        int AddCourse(Course course,IFormFile imageUp,IFormFile demoUp);
        List<ShowCourseForAdminViewModel> showCourseForAdmins();

        Course GetCourseById(int id);
        void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);

        int AddCourseEpisode(CourseEpisode episode,IFormFile file);

        //برای چک کردن اینکه فایل تکراری نباشه
        bool IsExistFile(string episodeFileName);

        List<CourseEpisode> GetListEpisode(int courseId);
        CourseEpisode GetCourseEpisodeById(int episodeId);

        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);

        //چونکه تعداد دوره نامحدود است بنابراین برای صفحه بندی به مشکل میخوریم باید در لحظه محاسبه بشه چند تا دوره است که طبق اون صفحه بندی انجام بشه
        Tuple<List<ShowListCourseViewModel>,int> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

        Course GetCourseForShow(int courseId);

        void AddComment(CourseComment comment);
        Tuple<List<CourseComment>, int> GetCourseComment(int courseId,int pageId);

        //مدلشو این در نظر میگیریم چرا چونکه باکس در ها که جدا کردیم مدل ویو این هست
        List<ShowListCourseViewModel> ShowPopularCourses();

        void AddCourseGroup(CourseGroup group);
        void UpdateCourseGroup(CourseGroup group);

        CourseGroup GetGroupById(int groupId);

        void AddCourseVote(int userId,int courseId,bool vote);

        Tuple<int, int> GetCourseVote(int courseId);
        bool IsFree(int courseId);



    }
}

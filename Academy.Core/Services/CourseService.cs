using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Academy.Core.Convertors;
using System.IO;
using System.Linq;
using System.Text;
using Academy.Core.DTOs.Course;
using Academy.Core.Generator;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Context;
using Academy.DataLayer.Entities.Course;
using Academy.DataLayer.Entities.Order;
using Academy.Core.Security;

namespace Academy.Core.Services
{
    public class CourseService : ICourseService
    {
        private AcademyContext _context;
        public CourseService(AcademyContext context)
        {
            _context = context;
        }

        #region CreateCourse
        public int AddCourse(Course course, IFormFile imageUp, IFormFile demoUp)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";
            //check image just be image/
            if (imageUp != null && imageUp.IsImage())
            {
                course.CourseImageName = NameGenerator.CodeGenarator() + Path.GetExtension(imageUp.FileName);
                string CourseImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/image", course.CourseImageName);
                using (var stream = new FileStream(CourseImgPath, FileMode.Create))
                {
                    imageUp.CopyTo(stream);
                }

                //Save Thumbnail image

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thumb", course.CourseImageName);

                imgResizer.Image_resize(CourseImgPath, thumbPath, 100);

            }

            //Upload Demo
            if (demoUp != null)
            {
                course.DemoFileName = NameGenerator.CodeGenarator() + Path.GetExtension(demoUp.FileName);
                string Demopath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/demo", course.DemoFileName);
                using (var stream = new FileStream(Demopath, FileMode.Create))
                {
                    demoUp.CopyTo(stream);
                }
            }


            _context.Add(course);
            _context.SaveChanges();
            return course.CourseId;

        }

        //GetAllGroup()
        public List<CourseGroup> GetAllCourses()
        {
            //اینجا در قسمت نمایش گروه ها و زیر گروه ها در پنل ادمین احتیاج به زیر نمایش زیر گروه هم داریم که این جدول به خودش رابطه داره که به شکل زیر می نویسم
            return _context.CourseGroups.Include(c=>c.CourseGroups).ToList();

        }

       

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.CourseGroups.Where(c => c.ParentId == null).Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            }).ToList();

        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();
        }

        public List<SelectListItem> GetStatus()
        {
            return _context.CourseStatuses.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int groupId)
        {
            return _context.CourseGroups.Where(c => c.ParentId == groupId).Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoles.Where(r => r.RoleID == 3).Include(u => u.User).Select(s => new SelectListItem()
            {
                Value = s.UserID.ToString(),
                Text = s.User.UserName
            }).ToList();

        }
        #endregion

        #region IndexCourse
        public List<ShowCourseForAdminViewModel> showCourseForAdmins()
        {
            return _context.Courses.Select(c => new ShowCourseForAdminViewModel()
            {
                CourseId = c.CourseId,
                Title = c.CourseTitle,
                ImageName = c.CourseImageName,
                EpisodeCount = c.CourseEpisodes.Count
            }).ToList();
        }
        #endregion

        #region EditCourse
        public Course GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }

        public void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;

            if (imgCourse != null && imgCourse.IsImage())
            {
                if (course.CourseImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/image", course.CourseImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thumb", course.CourseImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                course.CourseImageName = NameGenerator.CodeGenarator() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/image", course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thumb", course.CourseImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            if (courseDemo != null)
            {
                if (course.DemoFileName != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/demo", course.DemoFileName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }
                course.DemoFileName = NameGenerator.CodeGenarator() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/demo", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
        }


        #endregion

        #region CreateEpisode
        public int AddCourseEpisode(CourseEpisode episode, IFormFile fileEpisode)
        {
            //برای فایل خالی نبودن اونور چک میشه
            episode.EpisodeFileName = fileEpisode.FileName;

            //ذخیره episode
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                fileEpisode.CopyTo(stream);
            }

            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
            return episode.EpisodeId;
        }

        public bool IsExistFile(string episodeFileName)
        {
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episodeFileName);
            //return File.Exists(filePath);
            return _context.CourseEpisodes.Any(c=>c.EpisodeFileName== episodeFileName);
        }

        public List<CourseEpisode> GetListEpisode(int courseId)
        {
            return _context.CourseEpisodes.Where(c=>c.CourseId==courseId).ToList();
        }

        public CourseEpisode GetCourseEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                File.Delete(deleteFilePath);

                episode.EpisodeFileName = episodeFile.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }

            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }


        #endregion
        #region ShowCourse

        public Tuple<List<ShowListCourseViewModel>, int> GetCourse(int pageId = 1, string filter = ""
          , string getType = "all", string orderByType = "date",
          int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
                take = 8;

            IQueryable<Course> result = _context.Courses;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }

            }

            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updatedate":
                    {
                        result = result.OrderByDescending(c => c.UpdateDate);
                        break;
                    }
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }

            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < startPrice);
            }


            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int ids in selectedGroups)
                {
                    result= result.Where(c => c.GroupId == ids || c.ParentId == ids);
                }
            }

            int skip = (pageId - 1) * take;

            int pageCount = result.Include(c => c.CourseEpisodes).ToList().Select(c => new ShowListCourseViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).Count() / take;


           var query= result.Include(c => c.CourseEpisodes).ToList().Select(c => new ShowListCourseViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);


        }

        public Course GetCourseForShow(int courseId)
        {
            return _context.Courses.Include(c => c.CourseEpisodes).Include(c => c.CourseStatus).Include(c => c.CourseLevel).Include(c=>c.User).Include(c=>c.userCourses).FirstOrDefault(c=>c.CourseId==courseId);
        }


        #endregion
        #region CourseComment
        public void AddComment(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }

        public Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId)
        {
            int take = 5;
            int skip = (pageId-1)*take;
            int pageCount = _context.CourseComments.Where(c => !c.IsDelete && c.CourseId == courseId).Count() / take;
            if (pageCount % 2 != 0) 
            {
                pageCount++;
            }

            var listCourseComment = _context.CourseComments.Include(u => u.User).Where(c => !c.IsDelete && c.CourseId == courseId).Skip(skip).Take(take)
                .OrderByDescending(c=>c.CreateDate).ToList();

            return Tuple.Create(listCourseComment, pageCount);
        }




        #endregion

        #region popularCourse
        public List<ShowListCourseViewModel> ShowPopularCourses()
        {
            return _context.Courses.Include(o => o.OrderDetails).Where(c => c.OrderDetails.Any()).OrderByDescending(o => o.OrderDetails.Count).Take(8).
                Select(s => new ShowListCourseViewModel()
                {
                    CourseId = s.CourseId,
                    Price = s.CoursePrice,
                    Title = s.CourseTitle,
                    ImageName = s.CourseImageName,
                   // TotalTime = new TimeSpan(s.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks)) 
                }).ToList();
        }


        #endregion
        #region Admin_manageGroup
        public void AddCourseGroup(CourseGroup group)
        {
            _context.CourseGroups.Add(group);
            _context.SaveChanges();
        }

        public void UpdateCourseGroup(CourseGroup group)
        {
            _context.CourseGroups.Update(group);
            _context.SaveChanges();
        }

        public CourseGroup GetGroupById(int groupId)
        {
            return _context.CourseGroups.Find(groupId);
        }


        #endregion
        #region CourseVote
        public void AddCourseVote(int userId, int courseId, bool vote)
        {
            var courseVote = _context.CourseVotes.SingleOrDefault(c=>c.CourseId==courseId && c.UserId==userId);
            if (courseVote!=null)
            {
                courseVote.Vote = vote;
            }
            else
            {
                courseVote = new CourseVote()
                {
                    CourseId=courseId,
                    UserId=userId,
                    Vote=vote
                };
                _context.CourseVotes.Add(courseVote);

            }

            _context.SaveChanges();
        }


        //به جای tuple می تونیم از viewdata هم استفاده کنیم
        public Tuple<int, int> GetCourseVote(int courseId)
        {
            var Votes = _context.CourseVotes.Where(c => c.CourseId == courseId).Select(c => c.Vote).ToList();
            return Tuple.Create(Votes.Count(c => c), Votes.Count(c => !c));
        }

        public bool IsFree(int courseId)
        {
             return _context.Courses.Where(c => c.CourseId == courseId).Select(c => c.CoursePrice).First() == 0;
        }
        #endregion

    }
}

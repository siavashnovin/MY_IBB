using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyIBB.Controllers
{
    public class NewsController : Controller
    {

        MyIBBContext db = new MyIBBContext();
        private IPageGroupRepository pageGroupRepository;
        private IPageRepository PageRepository;
        private IPageCommentRepository pageCommentRepository;
         
        public NewsController()
        {
            pageGroupRepository = new PageGroupRepository(db);
            PageRepository = new PageRepository(db);
            pageCommentRepository = new PageCommentRepository(db);
            
        }


        // GET: News
        public ActionResult ShowGroups()
        {
            return PartialView(pageGroupRepository.GetGroupsForView());
        }
        public ActionResult ShowGroupsInMenu()
        {
            return PartialView(pageGroupRepository.GetAllGroups());
        }
        public ActionResult TopNews()
        {
            return PartialView(PageRepository.TopNews());
        }
        public ActionResult LatesNews()
        {
            return PartialView(PageRepository.LastNews());
        }
        [Route("Archive")]
        public ActionResult ArchiveNews()
        {
            return View(PageRepository.GetAllPage());
        }
        [Route("Group/{id}/{title}")]
        public ActionResult ShowNewsByGroupId(int id,string title)
        {
            ViewBag.name = title;
            return View(PageRepository.ShowPageByGroupId(id));
        }
        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            var news = PageRepository.GetPageById(id);
            if (news==null)
            {
                return HttpNotFound();
            }
            news.Visit +=1;
            PageRepository.UpdatePage(news);
            PageRepository.Save();
            return View(news);
        }
        public ActionResult AddComment(int id,string name , string email, string comment)
        {
            PageComment addcomment = new PageComment()
            {
                CreateDate = DateTime.Now,
                PageID=id,
                Comment=comment,
                Email=email,
                Name=name
            };
            pageCommentRepository.AddComment(addcomment);
            return PartialView("ShowComments",pageCommentRepository.GetCommentByNewsId(id));
        }
        public ActionResult ShowComments(int id)
        {
            return PartialView(pageCommentRepository.GetCommentByNewsId(id));
        }
       


        public ActionResult ShowGroupsInFooter()
        {
            return PartialView(pageGroupRepository.GetAllGroups());

        }
    }
}
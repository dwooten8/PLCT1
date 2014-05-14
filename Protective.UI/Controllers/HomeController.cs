using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using Protective.Core.Entity;
using Protective.Core.Interfaces.Repository;
using Protective.UI.Models;

namespace Protective.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Logger _logger = LogManager.GetLogger("Protective.UI.Home");
        private IRepository<Message> _messageRepository;

        public HomeController(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.MessageList = GetMessageList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMessage(HomeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _messageRepository.Create(model.Message);
                    return RedirectToAction("Index");
                }

                return View("Index", model);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                SetModelError(model, "Unable to post the message.  Please contact technical support.");
                return View("Index", model);
            }
        }

        [HttpPost]
        public JsonResult DeleteMessage(int id)
        {
            try
            {
                bool success = _messageRepository.Delete(new Message() { Id = id });
                return Json(new {success = success });

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [HttpPost]
        public JsonResult SetStar(int id, string isStarred)
        {
            try
            {
                bool success = _messageRepository.Update(new Message() { Id = id, IsStarred = !Convert.ToBoolean(isStarred) });
                return Json(new { success = success });

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private void SetModelError(HomeModel model, string message)
        {
            ModelState.Clear();
            model.Error = message;
            ModelState.AddModelError("Error", model.Error);
        }

        private List<Message> GetMessageList()
        {
            return _messageRepository.GetList().OrderByDescending(m => m.AddedDate).ToList();
        }
    }
}
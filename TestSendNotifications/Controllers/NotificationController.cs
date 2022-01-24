using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestSendNotifications.Models;
using TestSendNotifications.Services;

namespace TestSendNotifications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendNotificationController : Controller
    {
        private INotificationService _notificationService;

        // TODO: add unit tests
        public SendNotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        
        [HttpPost]
        public ActionResult Post(NotificationModel notification)
        {
            if (!notification.IsValid(out string error))
            {
                return BadRequest(error);
            }

            var sendedNotification = _notificationService.SendNotification(notification.Convert());

            return Json(new { id = sendedNotification.Id, isSendedResult = sendedNotification.IsDelivered });
        }

        [HttpGet(Name = "Result")]
        public ActionResult Get(Guid id)
        {
            var isSendedResult = _notificationService.IsSended(id, out string error);
            if (!string.IsNullOrWhiteSpace(error))
            {
                return BadRequest(error);
            }

            return Json(new { isSendedResult });
        }
    }
}
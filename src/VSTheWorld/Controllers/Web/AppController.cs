using System.Linq;
using Microsoft.AspNet.Mvc;
using VSTheWorld.ViewModels;
using VSTheWorld.Services;
using VSTheWorld.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace VSTheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IWorldRepository _repository;

        public AppController(IMailService service, IWorldRepository repository)
        {
            // Constructor for injection
            _mailService = service;
            _repository = repository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var trips = _repository.GetAllTrips();

            // pass trips into the View.  The view will be able to read information from the Model.
            return View(trips);
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid) { 
                string email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email.  Configuration issues exist on the server.  Contact the system administrator.");
                }

                if (_mailService.SendMail(email, email, $"Contact Page from {model.Name} ({model.Email}): {model.Subject}", model.Message))
                {
                    ModelState.Clear();
                    ViewBag.Message = "Mail sent.  Thanks!";
                }
            }

            return View();
        }
	}

}

using System.Web.Mvc;
using WebApplication1.Models.DomainModel;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly FormService formService;
        private readonly UserService userService;

        public HomeController ()
        {
            formService = new FormService();
            userService = new UserService();
        }
        // 首页
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(int? salesId)
        {
            var salesList = userService.GetSalesList();
            if (salesId != null && salesId.HasValue)
                ViewBag.SelectedSales = salesId.Value;
            return View(salesList);
        }

        // 课程介绍
        public ActionResult Course()
        {
            return View();
        }

        // 关于我们
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // 联系我们
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // 近期活动
        public ActionResult Activities()
        {
            return View();
        }

        // 我的
        public ActionResult MyPage()
        {
            return View();
        }

        [HttpPost]
        public bool SubmitBooking(BookingModel bookingModel)
        {
            // TODO: Submit form
            formService.RegisterInterest(bookingModel);

            return true;
        }

        public ActionResult SubmitSuccess()
        {
            return View();
        }
    }
}
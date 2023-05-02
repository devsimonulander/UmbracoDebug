using System.Web;
using System.Web.Mvc;
using Ucommerce.Extensions;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

using Ucommerce.EntitiesV2;
using Ucommerce.Api;

namespace ZiplineShop.Controllers
{
    public class HomeController : RenderMvcController
    {
        //public StartPageController(Ucommerce.Api.ITransactionLibrary transactionLibrary)
        //{
        //    TransactionLibrary = transactionLibrary;
        //}

        //public ITransactionLibrary TransactionLibrary { get; }

        // GET: StartPage
        public override ActionResult Index(ContentModel model)
        {
            var catalogLibrary = Ucommerce.Infrastructure.ObjectFactory.Instance.Resolve<Ucommerce.Api.ICatalogLibrary>();
            
            var productBySku = catalogLibrary.GetProduct("123");

            if(productBySku == null)
            {
                productBySku = new Ucommerce.Search.Models.Product();
                productBySku.Name = "Null";
            }

            var vm = new StartPageViewModel
            {
                Body = (HtmlString)model.Content.GetProperty("bodyText").GetValue(),
                Products = productBySku.Name

            };

            return View(vm);
        }
    }


    public class StartPageViewModel
    {
        
        public HtmlString Body { get; set; }
        public string Products { get; set; }
    }
}
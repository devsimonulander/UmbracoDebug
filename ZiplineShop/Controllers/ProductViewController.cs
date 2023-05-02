using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ucommerce.EntitiesV2;
using Ucommerce.Api;
using Umbraco.Web.Mvc;
using System.Collections;
using Ucommerce.Extensions;
using System.Text;
using Umbraco.Core.Models;
using Umbraco.Web.Models.PublishedContent;
using Umbraco.Core;
using Ucommerce.Search.Slugs;
using Ucommerce.Synchronization.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace ZiplineShop.Controllers
{
    public class ProductViewController : RenderMvcController
    {
        private readonly ICatalogLibrary _catalogLibrary;
        private readonly IUrlService _urlService;
        private readonly ICatalogContext _catalogContext;

        public ProductViewController()
        {
            _catalogLibrary     = Ucommerce.Infrastructure.ObjectFactory.Instance.Resolve<ICatalogLibrary>();
            _urlService         = Ucommerce.Infrastructure.ObjectFactory.Instance.Resolve<IUrlService>();
            _catalogContext     = Ucommerce.Infrastructure.ObjectFactory.Instance.Resolve<ICatalogContext>();
        }

        // GET: ProductView
        public ActionResult Index()
        {

            var rootCategories = _catalogLibrary.GetRootCategories();

            return View(new VM2
            {
                Categories = rootCategories.Select(category => new CategoryVm
                {
                   Id = category.Guid,
                   Name = category.Name
                }).ToArray(),
            });
        }
        public ActionResult Category()
        {


            var allProductsInCategory = _catalogLibrary.GetCategory(new Guid()).Products;

            return View();
        }
    }
    public class CategoryViewModel
    {
        public String Name { get; set; }
        public String Url { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }

    public class VM2
    {
        public ICollection<CategoryVm> Categories { get; set; }
    }

    public class CategoryVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
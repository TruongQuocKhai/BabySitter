using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BabySitter
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "",
               url: "Home",
               defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BabySitter.Controller" }

       );
            routes.MapRoute(
               name: "",
               url: "Service",
               defaults: new { controller = "Main", action = "Service", id = UrlParameter.Optional },
                namespaces: new[] { "BabySitter.Controller" }
           );
            routes.MapRoute(
            name: "",
            url: "Introduce",
            defaults: new { controller = "Main", action = "Introduce", id = UrlParameter.Optional },
             namespaces: new[] { "BabySitter.Controller" }

        );
            routes.MapRoute(
         name: "",
         url: "Contact",
         defaults: new { controller = "Main", action = "ContactDees", id = UrlParameter.Optional },
          namespaces: new[] { "BabySitter.Controller" }

     );
            routes.MapRoute(
             name: "",
             url: "Review",
             defaults: new { controller = "Chil", action = "CreateReview", id = UrlParameter.Optional },
             namespaces: new[] { "BabySitter.Controller" }

            );
            routes.MapRoute("", "InformationTravel/{meta}-{id}",
                new { controller = "Main", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BabySitter.Controller" }
            );

            routes.MapRoute(
                name: "",
                url: "InformationTravel",
                defaults: new { controller = "Main", action = "InformationTravel", id = UrlParameter.Optional },
                namespaces: new[] { "BabySitter.Controller" }
            );

            //router tin tức
            routes.MapRoute(
               name: "",
               url: "gioi-thieu",
               defaults: new { controller = "Main", action = "Introduce", id = UrlParameter.Optional },
               namespaces: new[] { "BabySitter.Controller" }
           );
            routes.MapRoute(
              name: "",
              url: "tin-tuc",
              defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "BabySitter.Controller" }
              );
            routes.MapRoute(
             name: "",
             url: "admin",
             defaults: new { controller = "Admin", action = "Login", id = UrlParameter.Optional },
             namespaces: new[] { "BabySitter.Controller" }
             
           
             );
            // tin tuc theon danh muc
            routes.MapRoute(
             name: "",
             url: "tin-tuc/{name_category}-{id}",
             defaults: new { controller = "News", action = "tintuctheodanhmuc", id = UrlParameter.Optional },
             namespaces: new[] { "BabySitter.Controller" }
         );

            //chi tiet bai viet tin tuc
            routes.MapRoute(
              name: "",
              url: "tin-tuc/chi-tiet/{meta}-{id}",
              defaults: new { controller = "News", action = "chitietbaiviet", id = UrlParameter.Optional },
              namespaces: new[] { "BabySitter.Controller" }
          );
           
            routes.MapRoute("", "Review/{title_review}-{id}",
               new { controller = "Chil", action = "DetailsReview", id = UrlParameter.Optional },
               namespaces: new[] { "BabySitter.Controller" }
            );
            routes.MapRoute("", "InformationTravel/{nameProvince}/{id}",
               new { controller = "Chil", action = "listTravel", id = UrlParameter.Optional },
               namespaces: new[] { "BabySitter.Controller" }
            );
            routes.MapRoute("", "dang-nhap",
             new { controller = "User", action = "Login", id = UrlParameter.Optional },
             namespaces: new[] { "BabySitter.Controller" }
          );
            routes.MapRoute("", "dang-ky",
            new { controller = "User", action = "Register", id = UrlParameter.Optional },
            namespaces: new[] { "BabySitter.Controller" }
         );

            routes.MapRoute("", "lien-he",
            new { controller = "Product", action = "ContactDees", id = UrlParameter.Optional },
            namespaces: new[] { "BabySitter.Controller" }
         );
            routes.MapRoute("", "lien-he-thanh-cong",
          new { controller = "Product", action = "Sent", id = UrlParameter.Optional },
          namespaces: new[] { "BabySitter.Controller" }
       );
            routes.MapRoute("", "dang-xuat",
            new { controller = "User", action = "Logout", id = UrlParameter.Optional },
            namespaces: new[] { "BabySitter.Controller" }
         );  //routes.MapRoute("Detail", "{controller}/{action}/{meta}/{id}",
            //    new { controller = "Main", action = "Detail", id = UrlParameter.Optional },
            //    new RouteValueDictionary
            //    {
            //        {"type","InformationTravel/{meta}/{id}" }
            //    },
            //    new[] { "BabySitter.Controller" }
            //);
            //shoppping
            // routes.MapRoute(
            //    name: "Product Category",
            //    url: "san-pham/{metatitle}-{cateId}",
            //    defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
            //    namespaces: new[] { "BabySitter.Controllers" }
            //);

            routes.MapRoute("", "chi-tiet/{meta}-{id}",
             new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
             namespaces: new[] { "BabySitter.Controller" }
         );
            routes.MapRoute("", "san-pham/{MetaTitle}-{id}",
              new { controller = "Product", action = "ListProduct", id = UrlParameter.Optional },
              namespaces: new[] { "BabySitter.Controller" }
          
        ); routes.MapRoute("", "tat-ca-san-pham",
         new { controller = "Product", action = "AllProduct", id = UrlParameter.Optional },
         namespaces: new[] { "BabySitter.Controller" }

   );
            routes.MapRoute(
              name: "Cart",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "BabySitter.Controllers" }
          );
            routes.MapRoute(
              name: "Search",
              url: "tim-kiem",
              defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
              namespaces: new[] { "BabySitter.Controllers" }
            );
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "BabySitter.Controllers" }
            );

            routes.MapRoute(
          name: "Add Cart",
          url: "them-gio-hang",
          defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
          namespaces: new[] { "BabySitter.Controllers" }
      );
            routes.MapRoute(
          name: "Payment Success",
          url: "hoan-thanh",
          defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
          namespaces: new[] { "BabySitter.Controllers" }
      );


            //Định tuyến trang mặt định
            routes.MapRoute(
                name: "Main",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //        namespaces: new[] {"SIA.Controller"}
            );
        }
    }
}

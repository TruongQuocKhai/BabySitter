using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabySitter.Models.Dao
{
    public class ProductCategoryDao
    {
        BabySitterEntities db = null;
        public ProductCategoryDao()
        {
            db = new BabySitterEntities();
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
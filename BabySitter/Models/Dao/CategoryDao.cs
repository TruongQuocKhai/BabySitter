using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabySitter.Models.Dao
{
    public class CategoryDao
    {
        BabySitterEntities db = null;
        public CategoryDao()
        {
            db = new BabySitterEntities();
        }
        public long Insert(CategoryPaint category)
        {
            db.CategoryPaints.Add(category);
            db.SaveChanges();
            return category.ID;
        }
        public List<CategoryPaint> ListAll()
        {
            return db.CategoryPaints.Where(x => x.Status == true).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category()
            {
                Name = model.Name,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Categories
                    .Select(e => new CategoryListItem
                    {
                        CategoryId = e.CategoryId,
                        Name = e.Name,
                    }
                    );
                return query.ToArray();
            }
        }

        public Category GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Categories
                    .Single(e => e.CategoryId == id);
                return
                    new Category
                    {
                        CategoryId = entity.CategoryId,
                        Name = entity.Name,
                    };
            }
        }

        public bool UpdateCategory(Category model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == model.CategoryId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Categories
                    .Single(e => e.CategoryId == categoryId);
                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Category> Categories => _appDbContext.Categories;
        public void AddCategory(Category category)
        {
            _appDbContext.Add(category);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void ClearCategories()
        {
            _appDbContext.Categories.RemoveRange(Categories);
            _appDbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Categories', RESEED, 0)");
        }
    }
}

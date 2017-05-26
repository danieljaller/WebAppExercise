using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder.Internal;

namespace BethanysPieShop.Models
{
    public class PieRepository: IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> Pies
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }

        public void AddPie(Pie pie)
        {
            _appDbContext.Pies.Add(pie);
        }

        public void UpdatePie(Pie pie)
        {
            _appDbContext.Pies.Update(pie);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void ClearPies()
        {
            _appDbContext.ShoppingCartItems.RemoveRange(_appDbContext.ShoppingCartItems);
            _appDbContext.Pies.RemoveRange(Pies);
            _appDbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT('ShoppingCartItems', RESEED, 0)");
            _appDbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Pies', RESEED, 0)");
        }

        public void SeedDatabase()
        {
            DbInitializer.Seed(_appDbContext);
        }
    }
}

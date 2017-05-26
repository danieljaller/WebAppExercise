using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;

        public AdminController(IPieRepository pieRepository, ICategoryRepository categoryRepository, IOrderRepository orderRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult AddPie()
        {

            var vm = new AdminViewModel();
            PopulateVmLists(vm);
            return View(vm);
        }

        private void PopulateVmLists(AdminViewModel vm)
        {
            vm.Categories = new List<SelectListItem>();
            foreach (var category in _categoryRepository.Categories)
            {
                vm.Categories.Add(new SelectListItem() { Text = category.CategoryName });
            }
            vm.Pies = new List<SelectListItem>();
            foreach (var pie in _pieRepository.Pies)
            {
                vm.Pies.Add(new SelectListItem() { Text = pie.Name, Value = pie.PieId.ToString() });
            }
        }

        [HttpPost]
        public IActionResult AddPie(AdminViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ExtraDescription == null)
                    viewModel.ExtraDescription = string.Empty;

                var category = _categoryRepository.Categories.First(x => x.CategoryName == viewModel.CategoryName);
                var pieName = viewModel.PieName.Trim();
                _pieRepository.AddPie(new Pie()
                {
                    Name = pieName,
                    Category = category,
                    ImageThumbnailUrl = "https://www.postplanner.com/hs-fs/hub/513577/file-2909864225-gif/blog-files/facebook-thumbnail-250px.gif?t=1495052037194&width=250&height=250&name=facebook-thumbnail-250px.gif",
                    ImageUrl = "https://static.mathem.se/shared/images/products/large/07310240060157_c1n1.jpg",
                    Price = 20000,
                    ExtraDescription = viewModel.ExtraDescription
                });
                _pieRepository.Save();

                return RedirectToAction("List", "Pie", new { category = viewModel.CategoryName });
            }
            PopulateVmLists(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdatePrice(AdminViewModel viewModel)
        {
            if (viewModel.Price < 1 || viewModel.Price > 999)
                return RedirectToAction("AddPie");

            var pie = _pieRepository.GetPieById(viewModel.PieId);
            pie.Price = viewModel.Price;
            _pieRepository.UpdatePie(pie);
            _pieRepository.Save();

            return RedirectToAction("Details", "Pie", new { id = pie.PieId });
        }

        [HttpPost]
        public IActionResult AddCategory(AdminViewModel viewModel)
        {
            if (viewModel.CategoryName == null || viewModel.CategoryName.Length > 19 || viewModel.CategoryName.Length < 1)
                return RedirectToAction("AddPie");

            var category = new Category(){CategoryName = viewModel.CategoryName};
            
            _categoryRepository.AddCategory(category);
            _categoryRepository.Save();
            return RedirectToAction("AddPie");
        }

        public IActionResult SeedDatabase()
        {
            _categoryRepository.ClearCategories();
            _pieRepository.ClearPies();
            _orderRepository.ClearOrders();
            _categoryRepository.Save();
            _pieRepository.SeedDatabase();
            return RedirectToAction("AddPie");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Components
{
    public class WeeklySelectionMenu : ViewComponent
    {
        private readonly IPieRepository _pieRepository;

        public WeeklySelectionMenu(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_pieRepository.PiesOfTheWeek);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShelfLife.Models;
using ShelfLife.ViewModels;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ShelfLife.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string connString = JObject.Parse(File.ReadAllText("appsettings.Development.json"))["ConnectionString"].ToString();
            FoodItemRepository foodRepo = new FoodItemRepository();
            FoodItemViewModel viewModel = new FoodItemViewModel();
            viewModel.foodItems = foodRepo.GetAllFoods();
            return View(viewModel);
        }

        public IActionResult Delete(int FoodItemIdToDelete)
        {
            FoodItemRepository foodRepo = new FoodItemRepository();
            foodRepo.DeleteFood(FoodItemIdToDelete);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Label()
        {
            ViewData["Message"] = "Enter the food item you wish to track.";
            return View();
        }

        public IActionResult AddFood(string name, int shelfLife)
        {
            FoodItemRepository repo = new FoodItemRepository();
            DateTime dateCreated = DateTime.Now;
            DateTime dateExpired = dateCreated.AddDays(shelfLife);
            repo.CreateFood(name, dateCreated, dateExpired, "false");
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Pantry()
        {
            FoodItemRepository foodRepo = new FoodItemRepository();
            FoodItemViewModel viewModel = new FoodItemViewModel();
            viewModel.foodItems = foodRepo.GetAllFoods();
            return View(viewModel.foodItems);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

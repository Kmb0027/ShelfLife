﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShelfLife.Models;
using ShelfLife.ViewModels;

namespace ShelfLife.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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
            repo.CreateFood(name, dateCreated, dateExpired);
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Pantry()
        {
            FoodItemRepository foodRepo = new FoodItemRepository();
            FoodItemViewModel viewModel = new FoodItemViewModel();
            viewModel.foodItems = foodRepo.GetAllFoods();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

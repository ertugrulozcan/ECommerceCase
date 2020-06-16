﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECommerceCase.WebClient.ViewModels;

namespace ECommerceCase.WebClient.Controllers
{
	public class HomeController : Controller
	{
		#region Services

		private readonly ILogger<HomeController> _logger;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="logger"></param>
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		#endregion
		
		#region Index

		public IActionResult Index()
		{
			return View();
		}

		#endregion
		
		#region Error

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		#endregion
	}
}
using api_consume.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace api_consume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        ///Show Data
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = new List<Teacher>();
            HttpClient _clint = new HttpClient();
            _clint.BaseAddress = new Uri("https://localhost:7266/");
            HttpResponseMessage response = await _clint.GetAsync("Api/Admin");
            if (response.IsSuccessStatusCode)
            {
                var result=response.Content.ReadAsStringAsync().Result;
                teachers=JsonConvert.DeserializeObject<List<Teacher>>(result);
            }
            return View(teachers);
        }


        ///Insert Data
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7266/");
            var response = await client.PostAsJsonAsync<Teacher>("Api/Admin", teacher);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

       ///Delete Data
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7266/");
            var response = await client.DeleteAsync($"Api/Admin/{id}");
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Teacher teacher = await GetTeacherByID(id);
            return View(teacher);
            //Teacher teacher = await GetTeacherByID(id);
            //return View();
        }

        //[HttpPost("{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Teacher teacher)
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:7266/");
            //var response = await client.PutAsJsonAsync<Teacher>($"Api/Admin/{teacher.Id}", teacher);
            //if (response.IsSuccessStatusCode)
            //{

            //    return RedirectToAction("Index");
            //}

            //return View();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7266/");
            var response = await client.PutAsJsonAsync($"Api/Admin/{teacher.Id}", teacher);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }


        public async Task<IActionResult> Details(int id)
        {
            Teacher teachers = await GetTeacherByID(id);
            return View(teachers);
        }

        private static async Task<Teacher> GetTeacherByID(int id)
        {
            Teacher teachers = new Teacher();
            HttpClient _clint = new HttpClient();
            _clint.BaseAddress = new Uri("https://localhost:7266/");
            HttpResponseMessage response = await _clint.GetAsync($"Api/Admin/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                teachers = JsonConvert.DeserializeObject<Teacher>(result);
            }

            return teachers;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

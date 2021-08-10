using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.WEB.Additional;
using Hotel.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Hotel.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, ILogger<CategoryController> logger, 
            ICategoryService guestService)
        {
            _mapper = mapper;
            _logger = logger;
            _categoryService = guestService;
        }


        [HttpGet]
        [Route(Routes.CategoryList)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Index()
        {
            var categories = _mapper.Map<IEnumerable<CategoryModel>>(_categoryService.GetAllCategories());
            if (categories is null)
                return Redirect(Routes.Home);

            return View(categories);
        }

        [HttpGet]
        [Route(Routes.CategoryDetails)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Details(int id)
        {
            var category = _mapper.Map<CategoryModel>(_categoryService.Get(id));

            if (category == null)
                return Redirect(Routes.Home);

            return View(category);
        }

        [HttpGet]
        [Route(Routes.CategoryCreate)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(Routes.CategoryCreate)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                int id = _categoryService.Create(_mapper.Map<CategoryDTO>(category));
                if (id > 0)
                    return RedirectToAction("Details", category.ID);
            }
            return Redirect(Routes.Home);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route(Routes.CategoryEdit, Name = nameof(Routes.CategoryEdit))]
        public IActionResult Edit(int id)
        {
            var category = _mapper.Map<CategoryModel>(_categoryService.Get(id));
            if (category == null)
                return Redirect(Routes.Home);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(Routes.CategoryEdit)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Edit(int id, CategoryModel category)
        {
            if (id != category.ID)
                return Redirect(Routes.Home);

            if (ModelState.IsValid)
            {
                try
                {
                    if (_categoryService.Update(_mapper.Map<CategoryDTO>(category)))
                        return RedirectToRoute(nameof(Routes.CategoryEdit), category.ID);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_categoryService.Get(id) is null)
                        return Redirect(Routes.Home);
                    else
                    {
                        throw;
                    }
                }
            }
            return Redirect(Routes.Home);
        }

        [HttpGet]
        [Route(Routes.CategoryDelete)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Delete(int id)
        {
            if (_categoryService.Delete(id))
                return Redirect(Routes.CategoryList);

            return Redirect(Routes.Home);
        }
    }
}

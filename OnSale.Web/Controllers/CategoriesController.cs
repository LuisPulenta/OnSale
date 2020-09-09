using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSale.Common.Entities;
using OnSale.Web.Data;

using OnSale.Web.Helpers;
using OnSale.Web.Models;
using Vereyon.Web;

namespace OnSale.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public CategoriesController(DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper,
            IFlashMessage flashMessage)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            this._flashMessage = flashMessage;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Categories");
                }

                var category = _converterHelper.ToCategory(model, path, true);
                _context.Add(category);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Esta Categoría ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(model);
        }



        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return NotFound();
            }

            CategoryViewModel model = _converterHelper.ToCategoryViewModel(Category);
            return View(model);
        }


        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var path = model.ImagePath;

                    if (model.ImageFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Categories");
                    }

                    Category category = _converterHelper.ToCategory(model, path, false);
                    _context.Update(category);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("duplicate"))
                        {
                            ModelState.AddModelError(string.Empty, "Esta Categoría ya existe");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        }
                    }
                }
            }
            return View(model);
        }




        // POST: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("The category was deleted.");
            }
            catch
            {
                _flashMessage.Danger("The category can't be deleted because it has related records.");
            }

            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> CreateTeam(int? id)
        //{
        //    if (id == null)

        //    {

        //        return NotFound();
        //    }

        //    var category = await _context.Categories.FindAsync(id.Value);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new ProductViewModel
        //    {
        //        categoryId = category.Id
        //        //Categories = _combosHelper.GetComboCategories()
        //    };


        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateTeam(ProductViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var path = string.Empty;
        //        model.Initials = model.Initials.ToUpper();
        //        model.category = await _context.Categories
        //        .FirstOrDefaultAsync(p => p.Id == model.categoryId);
        //        if (model.ImageFile != null)
        //        {
        //            path = await _imageHelper.UploadImageAsync(model.ImageFile, "Products");
        //        }

        //        var team = _converterHelper.ToProductEntity(model, path, true);
        //        _context.Products.Add(team);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction($"Details/{model.Id}");
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.InnerException.Message.Contains("duplicate"))
        //            {
        //                ModelState.AddModelError(string.Empty, "Este Equipo ya existe");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
        //            }

        //        }
        //    }

        //    //model.Categories = _combosHelper.GetComboCategories();
        //    return View(model);
        //}


        //public async Task<IActionResult> EditTeam(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var team = await _context.Products
        //        .Include(p => p.category)
        //        .FirstOrDefaultAsync(p => p.Id == id);
        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(_converterHelper.ToProductViewModel(team));
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditTeam(ProductViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var path = model.ImagePath;
        //        model.Initials = model.Initials.ToUpper();
        //        model.category = await _context.Categories
        //        .FirstOrDefaultAsync(p => p.Id == model.categoryId);

        //        if (model.ImageFile != null)
        //        {
        //            path = await _imageHelper.UploadImageAsync(model.ImageFile, "Products");
        //        }

        //        var team = _converterHelper.ToProductEntity(model, path, false);
        //        _context.Products.Update(team);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction($"Details/{model.category.Id}");
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.InnerException.Message.Contains("duplicate"))
        //            {
        //                ModelState.AddModelError(string.Empty, "Este Equipo ya existe");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
        //            }
        //        }
        //    }
        //    //model.Categories = _combosHelper.GetComboCategories();
        //    return View(model);
        //}

        //public async Task<IActionResult> DetailsTeam(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var team = await _context.Products
        //        .Include(p => p.category)
        //        .FirstOrDefaultAsync(o => o.Id == id.Value);
        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(team);
        //}

        //public async Task<IActionResult> DeleteTeam(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var team = await _context.Products
        //        .Include(p => p.category)
        //        .FirstOrDefaultAsync(pi => pi.Id == id.Value);
        //    if (team == null)
        //    {
        //        return NotFound();
        //    }


        //    _context.Products.Remove(team);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction($"{nameof(Details)}/{team.category.Id}");
        //}
    }
}

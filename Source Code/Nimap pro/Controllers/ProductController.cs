using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nimap_pro.Models;

namespace Nimap_pro.Controllers
{
    public class ProductController : Controller
    {

        private readonly ProductContext _Db;

        public object EntitySate { get; private set; }

        public ProductController(ProductContext Db)
        {
            _Db = Db;
        }
        
        public IActionResult ProductList()
        {
            try
            {
                var stdList = from a in _Db.tblProduct
                              join b in _Db.tblCategory
                              on a.CategoryID equals b.CategoryID
                              into Category
                              from b in Category.DefaultIfEmpty()

                              select new Product
                              {
                                  ProductID = a.ProductID,
                                  ProductName = a.ProductName,
                                  CategoryID = a.CategoryID,
                                  CategoryName = b == null ? "" : b.CategoryName
                              };

                return View(stdList);
            }
              catch(Exception ex)
            {
                return View();
            }


           
        }

        public IActionResult Create(Product obj)
        {
            LoadDDL();
            return View(obj);
        }
        [HttpPost]
        public async  Task<IActionResult> AddProduct(Product obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(obj.ProductID==0)
                    {
                        _Db.tblProduct.Add(obj);
                        await _Db.SaveChangesAsync();
                    }
                    else
                    {
                        _Db.Entry(obj).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();
                    }
                    
                    return RedirectToAction("ProductList");
                }
                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("ProductList");
            }
        }

        public async Task<IActionResult> DeletePro(int ID)
        {
            try
            {
                var std = await _Db.tblProduct.FindAsync(ID);
                 if(std!=null)
                {
                    _Db.tblProduct.Remove(std);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("ProductList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ProductList");
            }
        }

        private void LoadDDL()
        {
            try
            {
                List<Category> catList = new List<Category>();
                catList = _Db.tblCategory.ToList();
                catList.Insert(0, new Category { CategoryID = 0, CategoryName = "Please Select" });
                ViewBag.CatList = catList;
            }
            catch(Exception ex)

            {

            }
        }
    }
}

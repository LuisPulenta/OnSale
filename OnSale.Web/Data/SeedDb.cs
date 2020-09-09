using Microsoft.EntityFrameworkCore;
using OnSale.Common.Entities;
using OnSale.Common.Enums;
using OnSale.Web.Data.Entities;
using OnSale.Web.Helpers;
using OnSale.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;

            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("17157729", "Luis", "Núñez", "luisalbertonu@gmail.com", "156 814 963", "Espora 2052 - Rosedal", UserType.Admin);
            await CheckCategoriesAsync();
            await CheckProductsAsync();

        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                User user = await _userHelper.GetUserAsync("luisalbertonu@gmail.com");
                
                Category informatica = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Informatica");
                
                string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris gravida, nunc vel tristique cursus, velit nibh pulvinar enim.";
                await AddProductAsync(informatica, lorem, "ApplePencil", 3500000M, new string[] { "ApplePencil", "ApplePencil2"}, user);
                await AddProductAsync(informatica, lorem, "IPad", 2100000M, new string[] { "IPad", "IPad2", "IPad3" }, user);
                await AddProductAsync(informatica, lorem, "IPhoneX", 12000000M, new string[] { "IPhoneX", "IPhoneX2", "IPhoneX3", "IPhoneX4"}, user);
                await AddProductAsync(informatica, lorem, "Notebook", 95000M, new string[] { "Notebook", "Notebook2", "Notebook3", "Notebook4", "Notebook5" }, user);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync(Category category, string description, string name, decimal price, string[] images, User user)
        {
            Product product = new Product
            {
                Category = category,
                Description = description,
                IsActive = true,
                Name = name,
                Price = price,
                //ProductImages = new List<ProductImage>(),
                ProductImages = GetProductImages(images),
                Qualifications = GetRandomQualifications(description, user)
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        private ICollection<ProductImage> GetProductImages(string[] images)
        {
            List<ProductImage> productImages = new List<ProductImage>();
            foreach (string image in images)
            {
                productImages.Add(new ProductImage { ImageUrl = $"~/images/Products/{image}.jpg" });
            }

            return productImages;
        }


        private ICollection<Qualification> GetRandomQualifications(string description, User user)
        {
            List<Qualification> qualifications = new List<Qualification>();
            for (int i = 0; i < 10; i++)
            {
                qualifications.Add(new Qualification
                {
                    Date = DateTime.UtcNow,
                    Remarks = description,
                    Score = _random.Next(1, 5),
                    User = user
                });
            }

            return qualifications;
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                await AddCategoryAsync("Informatica");
                await AddCategoryAsync("Alimentos");
                await AddCategoryAsync("Deportes");
                await AddCategoryAsync("Calzado");
                await AddCategoryAsync("Indumentaria");
                await AddCategoryAsync("Juguetes");
                await AddCategoryAsync("Libreria");
                await AddCategoryAsync("Mascotas");
                await AddCategoryAsync("Muebles");
                await AddCategoryAsync("Viajes");
            }
        }

        private async Task AddCategoryAsync(string name)
        {
            _context.Categories.Add(new Category { Name = name, ImagePath = $"~/images/Categories/{name}.jpg" });
            await _context.SaveChangesAsync();
        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }


        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Córdoba",
                        Cities = new List<City>
                        {
                            new City { Name = "Córdoba" },
                            new City { Name = "Río Cuarto" },
                            new City { Name = "Villa María" }
                        }
                    },
                    new Department
                    {
                        Name = "Buenos Aires",
                        Cities = new List<City>
                        {
                            new City { Name = "La Plata" },
                            new City { Name = "Mar del  Plata" },
                            new City { Name = "Tandil" }
                        }
                    },
                    new Department
                    {
                        Name = "Santa Fe",
                        Cities = new List<City>
                        {
                            new City { Name = "Santa Fe" },
                            new City { Name = "Rosario" },
                            new City { Name = "Venado Tuerto" }
                        }
                    }
                }
                });
                _context.Countries.Add(new Country
                {
                    Name = "USA",
                    Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "California",
                        Cities = new List<City>
                        {
                            new City { Name = "Los Angeles" },
                            new City { Name = "San Diego" },
                            new City { Name = "San Francisco" }
                        }
                    },
                    new Department
                    {
                        Name = "Illinois",
                        Cities = new List<City>
                        {
                            new City { Name = "Chicago" },
                            new City { Name = "Springfield" }
                        }
                    },
                    new Department
                    {
                        Name = "Florida",
                        Cities = new List<City>
                        {
                            new City { Name = "Miami" },
                            new City { Name = "Orlando" }
                        }
                    }
                }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
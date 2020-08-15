using OnSale.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
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
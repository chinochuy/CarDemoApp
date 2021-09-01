using CarDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarDemoApp.Services
{
    public class NewDataService
    {
        List<CarModel> Cars = new List<CarModel>();
        public async Task StartData()
        {
            //Delete

            try
            {
                var carsResponse = await App.Database.GetCars();

                if (carsResponse.Count <= 0 || carsResponse == null)
                {
                    Cars.Add(new CarModel { Brand = "Seat", Line = "Ibiza", Year = 2020 });
                    Cars.Add(new CarModel { Brand = "Seat", Line = "Leon", Year = 2021 });
                    Cars.Add(new CarModel { Brand = "Seat", Line = "Leon Cupra", Year = 2021 });

                    foreach (var item in Cars)
                    {
                        await App.Database.SaveCars(item);
                    }
                }             
            }
            catch (Exception e)
            { 
            
            }
        }
    }
}

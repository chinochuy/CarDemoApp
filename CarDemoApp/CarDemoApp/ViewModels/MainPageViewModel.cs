using CarDemoApp.Models;
using CarDemoApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CarDemoApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        NewDataService newData;
        private readonly IPageDialogService pageDialogService;
        public ICommand AddNewCarCommand { get; set; }

        private ObservableCollection<CarModel> cars;
        public ObservableCollection<CarModel> Cars
        {
            get { return cars; }
            set { SetProperty(ref cars, value); }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set { SetProperty(ref brand, value); }
        }

        private string line;
        public string Line
        {
            get { return line; }
            set { SetProperty(ref line, value); }
        }

        private int year;
        public int Year
        {
            get { return year; }
            set { SetProperty(ref year, value); }
        }

        private int totalItems;
        public int TotalItems
        {
            get { return totalItems; }
            set { SetProperty(ref totalItems, value); }
        }
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            this.pageDialogService = pageDialogService;
            Title = "Cars Page";
            newData = new NewDataService();
            AddNewCarCommand = new DelegateCommand(AddNewCarCommandExecute);
        }

        private async void AddNewCarCommandExecute()
        {
            if (string.IsNullOrEmpty(Brand) || string.IsNullOrEmpty(Line) || Year <= 0)
            {
                await pageDialogService.DisplayAlertAsync("Error", "Empty fields", "Ok");
            }
            else
            {
                var existingCar = Cars.FirstOrDefault(x => x.Line == Line && x.Brand == Brand && x.Year == Year);
                if (existingCar != null)
                {
                    await pageDialogService.DisplayAlertAsync("Error", "Car in the list", "Ok");
                }
                else
                {
                    CarModel addNewCar = new CarModel()
                    {
                        Brand = Brand,
                        Line = Line,
                        Year = Year
                    };
                    await App.Database.SaveCars(addNewCar);
                    GetCarsInfo();
                }

                Brand = string.Empty;
                Line = string.Empty;
                Year = 0;
            }
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            //TODO Show list 
            await newData.StartData();
            GetCarsInfo();
        }

        private async void GetCarsInfo()
        {
            var carsResponse = await App.Database.GetCars();
            Cars = new ObservableCollection<CarModel>(carsResponse);
            TotalItems = Cars.Count();
        }

        public async override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
    }
}

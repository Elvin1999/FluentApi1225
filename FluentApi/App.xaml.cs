using FluentApi.DataAccess.EFServer;
using FluentApi.Domain.Abstractions;
using FluentApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FluentApi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnitOfWork DB;
        public App()
        {
            DB = new EFUnitOfWork();

            if (DB.CustomerRepository.GetAll().Count == 0)
            {
                var c1 = new Customer
                {
                    City = "Baku",
                    CompanyName = "STEP IT MMC",
                    ContactName = "+457955651",
                    Country = "Azerbaijan"
                };

                var c2 = new Customer
                {
                    City = "Silicon Valley",
                    CompanyName = "Elvin MMC",
                    ContactName = "165165165161",
                    Country = "USA"
                };

                DB.CustomerRepository.AddData(c1);
                DB.CustomerRepository.AddData(c2);
            }

            if (DB.OrderRepository.GetAll().Count == 0)
            {
                var o1 = new Order
                {
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-3),
                    ImagePath = @"https://cdn.tmobile.com/content/dam/t-mobile/en-p/cell-phones/apple/Apple-iPhone-14-Pro-Max/Gold/Apple-iPhone-14-Pro-Max-Gold-frontimage.png"
                };

                var o2 = new Order
                {
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    ImagePath = @"https://reviewed-com-res.cloudinary.com/image/fetch/s--Hv8HwBnm--/b_white,c_limit,cs_srgb,f_auto,fl_progressive.strip_profile,g_center,h_668,q_auto,w_1187/https://reviewed-production.s3.amazonaws.com/1615332223245/P1050294.jpg"
                };

                var o3 = new Order
                {
                    CustomerId = 2,
                    OrderDate = DateTime.Now.AddDays(-2),
                    ImagePath = @"https://m.media-amazon.com/images/I/71HUnJvHsbL._SL1500_.jpg"
                };

                var o4 = new Order
                {
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    ImagePath = @"https://www.notebookcheck.net/fileadmin/Notebooks/News/_nc3/twocloursredminote12pro.jpg"
                };
                DB.OrderRepository.AddData(o1);
                DB.OrderRepository.AddData(o2);
                DB.OrderRepository.AddData(o3);
                DB.OrderRepository.AddData(o4);
            }
        }
    }
}

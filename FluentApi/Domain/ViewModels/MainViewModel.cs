using FluentApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi.Domain.ViewModels
{
    public class MainViewModel:BaseViewModel
    {

		private ObservableCollection<Customer> allCustomers;

		public ObservableCollection<Customer> AllCustomers
        {
			get { return allCustomers; }
			set { allCustomers = value; OnPropertyChanged(); }
		}


        private ObservableCollection<Order> allOrders;

        public ObservableCollection<Order> AllOrders
        {
            get { return allOrders; }
            set { allOrders = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {
            var customers = App.DB.CustomerRepository.GetAll();
            AllCustomers = new ObservableCollection<Customer>(customers);

            var orders = App.DB.OrderRepository.GetAll();
            AllOrders = new ObservableCollection<Order>(orders);
        }
    }
}

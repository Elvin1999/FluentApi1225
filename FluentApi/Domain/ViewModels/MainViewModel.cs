using FluentApi.Commands;
using FluentApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FluentApi.Domain.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand OrderNowCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public RelayCommand DeleteOrderCommand { get; set; }
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

        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; OnPropertyChanged(); }
        }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {

            Customer = new Customer();
            SelectedOrder = new Order();

            var customers = App.DB.CustomerRepository.GetAll();
            AllCustomers = new ObservableCollection<Customer>(customers);

            var orders = App.DB.OrderRepository.GetAll();
            AllOrders = new ObservableCollection<Order>(orders);

            UpdateCommand = new RelayCommand((obj) =>
            {
                App.DB.CustomerRepository.UpdateData(Customer);
                customers = App.DB.CustomerRepository.GetAll();
                AllCustomers = new ObservableCollection<Customer>(customers);
                MessageBox.Show("Updated successfully");
                ClearForm();
            }, (pred) =>
            {
                return Customer?.Id >= 1;
            });

            AddCommand = new RelayCommand((obj) =>
            {
                if (Customer.Id >= 1)
                {
                    var item=App.DB.CustomerRepository
                    .GetAll()
                    .FirstOrDefault(c=>c.ContactName==Customer.ContactName);
                    if (item != null)
                    {
                        MessageBox.Show("Customer is already exist");
                    }
                    else
                    {
                        AddCustomer();
                    }
                }
                else
                {
                    AddCustomer();
                }
            });

            ResetCommand = new RelayCommand((obj) =>
            {
                ClearForm();
            });

            DeleteCustomerCommand = new RelayCommand((obj) => {
                App.DB.CustomerRepository.DeleteData(Customer);
                customers = App.DB.CustomerRepository.GetAll();
                AllCustomers = new ObservableCollection<Customer>(customers);
                MessageBox.Show("Deleted successfully");
                ClearForm();
            }, (pred) =>
            {
                return Customer?.Id >= 1;
            });

            OrderNowCommand = new RelayCommand((obj) =>
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    CustomerId = Customer.Id,
                    ImagePath= "https://cdn5.vectorstock.com/i/1000x1000/05/19/unknown-product-vector-42350519.jpg"
                };
                App.DB.OrderRepository.AddData(order);
                orders = App.DB.OrderRepository.GetAll();
                AllOrders = new ObservableCollection<Order>(orders);
                MessageBox.Show("Order added successfully");
            },
            (pred) =>
            {
                return Customer?.Id >= 1;
            });


            DeleteOrderCommand = new RelayCommand((obj) =>
            {
                App.DB.OrderRepository.DeleteData(SelectedOrder);
                orders = App.DB.OrderRepository.GetAll();
                AllOrders = new ObservableCollection<Order>(orders);
                MessageBox.Show("Order deleted successfully");
            },
            (pred) =>
            {
                return SelectedOrder?.Id >= 1;
            });
        }

        private void AddCustomer()
        {
            App.DB.CustomerRepository.AddData(Customer);
            var customers = App.DB.CustomerRepository.GetAll();
            AllCustomers = new ObservableCollection<Customer>(customers);
            MessageBox.Show("Added successfully");
            ClearForm();
        }

        private void ClearForm()
        {
            Customer = new Customer();
        }
    }
}

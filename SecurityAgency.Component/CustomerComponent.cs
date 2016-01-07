using AutoMapper;
using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.ViewModels;
using SecurityAgency.Repository;
using SecurityAgency.Repository.DbServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Component
{
    public class CustomerComponent : ICustomer
    {
        /// <summary>
        /// Initilize Referance of IDbRepository
        /// </summary>
        IDbRepository _repository = null;

        /// <summary>
        /// Assign  IDbRepository
        /// </summary>
        /// <param name="repositry">Refrence of IDbRepository</param>
        public CustomerComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        public List<CustomerViewModel> GetAllCustomer()
        {
            List<Customer> customer = _repository.GetAll<Customer>().Where(x => x.Active == true).ToList();

            if (customer == null)
                return null;

            Mapper.CreateMap<Customer, CustomerViewModel>().ForMember(dest => dest.NameEmail,
               opts => opts.MapFrom(
                   src => string.Format("{0} ({1})",
                       src.Name,
                       src.Email)));


            return Mapper.Map<List<Customer>, List<CustomerViewModel>>(customer);
        }

        public CustomerViewModel GetCustomer(int id)
        {
            Customer customer = _repository.Find<Customer>(x => x.CustomerId == id);

            if (customer == null)
                return null;

            Mapper.CreateMap<Customer, CustomerViewModel>();
            return Mapper.Map<Customer, CustomerViewModel>(customer);
        }

        public int? CreateUpdateCustomer(CustomerViewModel customerViewModel)
        {
            Customer customer = null;
            if (customerViewModel.CustomerId > 0)
            {
                customer = _repository.Find<Customer>(x => x.CustomerId == customerViewModel.CustomerId);
                if (customer == null)
                    return null;

                customer.Name = customerViewModel.Name;
                customer.Email = customerViewModel.Email;
                customer.Address = customerViewModel.Address;
                customer.ContactNo = customerViewModel.ContactNo;
                customer.Zip = customerViewModel.Zip;
                customer.HourlyRate = customerViewModel.HourlyRate;
                customer.Comments = customerViewModel.Comments;
                customer.ModifiedBy = customerViewModel.ModifiedBy;
                customer.ModifiedDate = DateTime.Now;

                _repository.Modify<Customer>(customer);
                return customer.CustomerId;
            }

            Mapper.CreateMap<CustomerViewModel, Customer>();
            customer = Mapper.Map<CustomerViewModel, Customer>(customerViewModel);

            customer.CreatedDate = DateTime.Now;
            customer.CreatedBy = customerViewModel.CreatedBy;
            customer.Active = true;
            customer.IsDeleted = false;
            _repository.Insert<Customer>(customer);
            return customer.CustomerId;
            
        }

        public int DeleteCustomer(int id, int UserId)
        {
            Customer customer = _repository.Find<Customer>(x => x.CustomerId == id);
            if (customer == null)
                return 0;
            customer.IsDeleted = true;
            customer.DeletedBy = UserId;
            customer.Active = false;
            customer.DeletedDate = DateTime.Now;
            return _repository.Modify<Customer>(customer);
        }
        //public void LogException(Exception e, string extraInfo = null)
        //{

        //    SecurityAgencyEntities context = new SecurityAgencyEntities();

        //    Error_Log obj = new Error_Log();
        //    obj.Message = e.Message;

        //    if (!string.IsNullOrEmpty(extraInfo))
        //        obj.Message = obj.Message + "<br/> Extar Info :" + extraInfo;

        //    obj.Source = e.Source;
        //    obj.StackTrace = e.StackTrace;
        //    obj.TargetSite = e.TargetSite.ToString();
        //    obj.ErrorDate = DateTime.Now;
        //    obj.ExceptionDetail = e.ToString();

        //    DateTime dt = (DateTime)obj.ErrorDate;


        //    context.Error_Log.InsertOnSubmit(obj);
        //    try
        //    {
        //        context.Error_Log.SubmitChanges();

        //    }
        //    catch
        //    {
        //    }

        //}
        public List<CustomerViewModel> GetCustomersForReport(string startDate, string endDate)
        {
            DateTime? _startDate = null;
            DateTime? _endDate = null;
            if (startDate != "") { _startDate = Convert.ToDateTime(startDate); };
            if (endDate != "") { _endDate = Convert.ToDateTime(endDate); };
            SecurityAgencyEntities ob = new SecurityAgencyEntities();
            List<getAllCustomers_Result> customer = ob.getAllCustomers(_startDate, _endDate).ToList();

            if (customer == null)
                return null;

            Mapper.CreateMap<getAllCustomers_Result, CustomerViewModel>();
            return Mapper.Map<List<getAllCustomers_Result>, List<CustomerViewModel>>(customer);
        }
        public bool validateCustomerEmailAddress(int customerId, string email)
        {
            Customer customer = _repository.Find<Customer>(x => x.CustomerId != customerId && x.Email == email);
            if(customer==null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
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
    public class CustomerPaymentComponent : ICustomerPayment
    {
        /// <summary>
        /// Initilize Referance of IDbRepository
        /// </summary>
        IDbRepository _repository = null;

        /// <summary>
        /// Assign  IDbRepository
        /// </summary>
        /// <param name="repositry">Refrence of IDbRepository</param>
        public CustomerPaymentComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        public List<CustomerPaymentViewModel> GetAllCustomerPayment()
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                List<CustomerPaymentViewModel> customerPaymentViewModel = (from customerPayment in objContext.CustomerPayments
                                                                           join customer in objContext.Customers on customerPayment.CustomerId equals customer.CustomerId
                                                                           join customerInvoice in objContext.CustomerInvoices on customerPayment.InvoiceId equals customerInvoice.InvoiceId
                                                                           select new CustomerPaymentViewModel
                                                                           {
                                                                               CustomerPaymentId=customerPayment.CustomerPaymentId,
                                                                               CustomerName = customer.Name,
                                                                               CustomerId = customer.CustomerId,
                                                                               PaymentDate=customerPayment.PaymentDate,
                                                                               InvoiceNo = customerInvoice.InvoiceNo,
                                                                               InvoiceId = customerPayment.InvoiceId,
                                                                               BeginDate = customerInvoice.BeginDate,
                                                                               EndDate = customerInvoice.EndDate,
                                                                               InvoiceDate = customerInvoice.InvoiceDate,
                                                                               Amount = customerPayment.Amount,
                                                                               TotalHours = customerInvoice.TotalHours,
                                                                               Comments = customerPayment.Comments,
                                                                               CreatedBy = customerPayment.CreatedBy,
                                                                               CreatedDate = customerPayment.CreatedDate,
                                                                               ModifiedBy = customerPayment.ModifiedBy,
                                                                               ModifiedDate = customerPayment.ModifiedDate,
                                                                               IsDeleted = customerPayment.IsDeleted,
                                                                               DeletedBy = customerPayment.DeletedBy,
                                                                               DeletedDate = customerPayment.DeletedDate
                                                                           }).Where(i=>i.IsDeleted==false).ToList();
                if (customerPaymentViewModel == null)
                    return null;

                return customerPaymentViewModel;
            }
        }

        public CustomerPaymentViewModel GetCustomerPayment(int id)
        {
            CustomerPayment customer = _repository.Find<CustomerPayment>(x => x.CustomerPaymentId == id);

            if (customer == null)
                return null;

            Mapper.CreateMap<CustomerPayment, CustomerPaymentViewModel>();
            return Mapper.Map<CustomerPayment, CustomerPaymentViewModel>(customer);
        }

        public int? CreateUpdateCustomerPayment(CustomerPaymentViewModel customerPaymentViewModel)
        {
            CustomerPayment customerPayment = null;

            if (customerPaymentViewModel.CustomerPaymentId > 0)
            {
                customerPayment = _repository.Find<CustomerPayment>(x => x.CustomerPaymentId == customerPaymentViewModel.CustomerPaymentId);
                if (customerPayment == null)
                    return null;

                customerPayment.InvoiceId = customerPaymentViewModel.InvoiceId;
                customerPayment.CustomerId = customerPaymentViewModel.CustomerId;
                customerPayment.PaymentDate = customerPaymentViewModel.PaymentDate;
                customerPayment.Amount = customerPaymentViewModel.Amount;
                customerPayment.Comments = customerPaymentViewModel.Comments;
                customerPayment.ModifiedBy = customerPaymentViewModel.ModifiedBy;
                customerPayment.ModifiedDate = DateTime.Now;

                _repository.Modify<CustomerPayment>(customerPayment);
                return customerPayment.CustomerPaymentId;
            }

            Mapper.CreateMap<CustomerPaymentViewModel, CustomerPayment>();
            customerPayment = Mapper.Map<CustomerPaymentViewModel, CustomerPayment>(customerPaymentViewModel);

            customerPayment.CreatedDate = DateTime.Now;
            customerPayment.CreatedBy = customerPaymentViewModel.CreatedBy;
            customerPayment.IsDeleted = false;
            return _repository.Insert<CustomerPayment>(customerPayment);
        }

        public int DeleteCustomerPayment(int id, int UserId)
        {
            CustomerPayment customerPayment = _repository.Find<CustomerPayment>(x => x.CustomerPaymentId == id);
            if (customerPayment == null)
                return 0;
            customerPayment.IsDeleted = true;
            customerPayment.DeletedBy = UserId;
            customerPayment.DeletedDate = DateTime.Now;
            return _repository.Modify<CustomerPayment>(customerPayment);
        }
    }
}
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
    public class CustomerInvoiceComponent : ICustomerInvoice
    {
        /// <summary>
        /// Initilize Referance of IDbRepository
        /// </summary>
        IDbRepository _repository = null;

        /// <summary>
        /// Assign  IDbRepository
        /// </summary>
        /// <param name="repositry">Refrence of IDbRepository</param>
        public CustomerInvoiceComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        public List<CustomerInvoiceViewModel> GetAllCustomerInvoice()
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                List<CustomerInvoiceViewModel> customerInvoice = (from objcustomer in objContext.Customers
                                                                  join objcustomerinvoice in objContext.CustomerInvoices on objcustomer.CustomerId equals objcustomerinvoice.CustomerId
                                                                  select new CustomerInvoiceViewModel
                                                                  {
                                                                      InvoiceId = objcustomerinvoice.InvoiceId,
                                                                      CustomerId = objcustomerinvoice.CustomerId,
                                                                      CustomerName = objcustomer.Name,
                                                                      HourlyRate=objcustomerinvoice.HourlyRate,
                                                                      InvoiceNo = objcustomerinvoice.InvoiceNo,
                                                                      InvoiceDate = objcustomerinvoice.InvoiceDate,
                                                                      BeginDate = objcustomerinvoice.BeginDate,
                                                                      EndDate = objcustomerinvoice.EndDate,
                                                                      TotalHours = objcustomerinvoice.TotalHours,
                                                                      Amount = objcustomerinvoice.Amount,
                                                                      Description = objcustomerinvoice.Description,
                                                                      InternalComments = objcustomerinvoice.InternalComments,
                                                                      CreatedBy = objcustomerinvoice.CreatedBy,
                                                                      CreatedDate = objcustomerinvoice.CreatedDate,
                                                                      ModifiedBy = objcustomerinvoice.ModifiedBy,
                                                                      IsDeleted = objcustomerinvoice.IsDeleted,
                                                                      DeletedBy = objcustomerinvoice.DeletedBy,
                                                                      DeletedDate = objcustomerinvoice.DeletedDate

                                                                  }).Where(i=>i.IsDeleted==false).ToList();

                if (customerInvoice == null)
                    return null;

                return customerInvoice;
            }
        }

        public CustomerInvoiceViewModel GetCustomerInvoice(int id)
        {
            CustomerInvoice customerInvoice = _repository.Find<CustomerInvoice>(x => x.InvoiceId == id);

            if (customerInvoice == null)
                return null;

            Mapper.CreateMap<CustomerInvoice, CustomerInvoiceViewModel>();
            return Mapper.Map<CustomerInvoice, CustomerInvoiceViewModel>(customerInvoice);
        }

        public List<CustomerInvoiceViewModel> GetCustomerInvoiceByCustomerId(int id)
        {
            List<CustomerInvoice> customerInvoice = _repository.GetAll<CustomerInvoice>().Where(i => i.CustomerId == id && i.IsDeleted==false).ToList();
            if (customerInvoice == null)
                return null;

            Mapper.CreateMap<CustomerInvoice, CustomerInvoiceViewModel>();
            return Mapper.Map<List<CustomerInvoice>, List<CustomerInvoiceViewModel>>(customerInvoice);
        }

        public int? CreateCustomerInvoice(CustomerInvoiceViewModel customerInvoiceViewModel)
        {
            CustomerInvoice customerInvoice = null;

            if (customerInvoiceViewModel.InvoiceId > 0)
            {
                customerInvoice = _repository.Find<CustomerInvoice>(x => x.InvoiceId == customerInvoiceViewModel.InvoiceId);
                if (customerInvoice == null)
                    return null;

                customerInvoice.CustomerId = customerInvoiceViewModel.CustomerId;
                customerInvoice.InvoiceNo = customerInvoiceViewModel.InvoiceNo;
                customerInvoice.BeginDate = customerInvoiceViewModel.BeginDate;
                customerInvoice.EndDate = customerInvoiceViewModel.EndDate;
                customerInvoice.TotalHours = customerInvoiceViewModel.TotalHours;
                customerInvoice.HourlyRate = customerInvoiceViewModel.HourlyRate;
                customerInvoice.Amount = customerInvoiceViewModel.Amount;
                customerInvoice.Description = customerInvoiceViewModel.Description;
                customerInvoice.InternalComments = customerInvoiceViewModel.InternalComments;
                customerInvoice.ModifiedBy = customerInvoiceViewModel.ModifiedBy;
                customerInvoice.ModifiedDate = DateTime.Now;

                _repository.Modify<CustomerInvoice>(customerInvoice);
                return customerInvoice.InvoiceId;
            }

            Mapper.CreateMap<CustomerInvoiceViewModel, CustomerInvoice>();
            customerInvoice = Mapper.Map<CustomerInvoiceViewModel, CustomerInvoice>(customerInvoiceViewModel);

            customerInvoice.CreatedDate = DateTime.Now;
            customerInvoice.CreatedBy = customerInvoiceViewModel.CreatedBy;
            customerInvoice.IsDeleted = false;
            return _repository.Insert<CustomerInvoice>(customerInvoice);
        }

        public int DeleteCustomerInvoice(int Id, int UserId)
        {
            CustomerInvoice customerInvoice = _repository.Find<CustomerInvoice>(x => x.InvoiceId == Id);
            if (customerInvoice == null)
                return 0;
            customerInvoice.IsDeleted = true;
            customerInvoice.DeletedBy = UserId;
            customerInvoice.DeletedDate = DateTime.Now;
            return _repository.Modify<CustomerInvoice>(customerInvoice);
        }
        
        public int GetNewInvoiceNumber()
        {
            CustomerInvoice customerInvoice = _repository.GetAll<CustomerInvoice>().OrderByDescending(i => i.InvoiceNo).FirstOrDefault();
            if (customerInvoice == null)
                return 1001;

            return customerInvoice.InvoiceNo + 1;
        }
        public CustomerInvoiceViewModel GetCustomerHoursAndAmount(int customerId, string type, string beginDate, string endDate, int inoviceId)
        {
            CustomerInvoiceViewModel objectCustomerInvoiceViewModel = new CustomerInvoiceViewModel();
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                if (string.IsNullOrEmpty(type))
                {
                    //Get beginDate and endDate first
                    CustomerInvoice objectCustomerInvoice = _repository.GetAll<CustomerInvoice>().Where(x => x.CustomerId == customerId && x.IsDeleted == false && x.InvoiceId!= inoviceId).OrderByDescending(i => i.EndDate).FirstOrDefault();
                    if (objectCustomerInvoice != null)
                    {
                        beginDate = objectCustomerInvoice.EndDate.AddDays(1).ToString();
                        endDate = objectCustomerInvoice.EndDate.AddDays(7).ToString();
                    }
                }
                //////Get values from hourlog table
                List<getCustomerInvoiceAmount_Result> objectgetCustomerInvoiceAmount_Result = objContext.getCustomerInvoiceAmount(Convert.ToDateTime(beginDate), Convert.ToDateTime(endDate), customerId).ToList();

                objectCustomerInvoiceViewModel.TotalHours = Convert.ToInt32(objectgetCustomerInvoiceAmount_Result.Sum(i=>i.TotHours));
                objectCustomerInvoiceViewModel.Amount = Convert.ToInt32(objectgetCustomerInvoiceAmount_Result.Sum(i=>i.Amount));
                objectCustomerInvoiceViewModel.BeginDate = Convert.ToDateTime(beginDate);
                objectCustomerInvoiceViewModel.EndDate = Convert.ToDateTime(endDate);
                objectCustomerInvoiceViewModel.HourlyRate = Convert.ToDecimal(objectgetCustomerInvoiceAmount_Result.FirstOrDefault().HourlyRate);
                return objectCustomerInvoiceViewModel;
            } 
        }
        public CustomerInvoiceViewModel GenerateInvoice(int invoiceId)
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                CustomerInvoiceViewModel objectCustomerInvoiceViewModel = new CustomerInvoiceViewModel();
                objectCustomerInvoiceViewModel = (from customer in objContext.Customers
                                                  join customerInvoice in objContext.CustomerInvoices on customer.CustomerId equals customerInvoice.CustomerId
                                                  where customerInvoice.InvoiceId == invoiceId
                                                  select new CustomerInvoiceViewModel
                                                  {
                                                      InvoiceNo = customerInvoice.InvoiceNo,
                                                      Name = customer.Name,
                                                      HourlyRate=customerInvoice.HourlyRate,
                                                      Address = customer.Address,
                                                      Zip = customer.Zip,
                                                      ContactNo = customer.ContactNo,
                                                      InvoiceDate = customerInvoice.InvoiceDate,
                                                      TotalHours = customerInvoice.TotalHours,
                                                      Description = customerInvoice.Description,
                                                      Amount = customerInvoice.Amount,
                                                      BeginDate = customerInvoice.BeginDate,
                                                      EndDate = customerInvoice.EndDate,
                                                      CustomerId = customerInvoice.CustomerId
                                                  }).FirstOrDefault();

                //////Get values from hourlog table
                List<getCustomerInvoiceAmount_Result> objectgetCustomerInvoiceAmount_Result = objContext.getCustomerInvoiceAmount(objectCustomerInvoiceViewModel.BeginDate, objectCustomerInvoiceViewModel.EndDate, objectCustomerInvoiceViewModel.CustomerId).ToList();

                if (objectCustomerInvoiceViewModel == null)
                    return null;

                return objectCustomerInvoiceViewModel;

            }
        }
        public List<CustomerInvoiceViewModel> GetCustomersInvoiceForReport(string startDate, string endDate, int customerId, string invoiceType)
        {
            DateTime? _startDate = null;
            DateTime? _endDate = null;
            if (startDate != "") { _startDate = Convert.ToDateTime(startDate); };
            if (endDate != "") { _endDate = Convert.ToDateTime(endDate); };
            SecurityAgencyEntities ob = new SecurityAgencyEntities();
            List<getAllCustomerInvoice_Result> customer = ob.getAllCustomerInvoice(_startDate, _endDate).ToList();

            if(customerId!=0)
            {
                customer = customer.Where(i => i.customerid == customerId).ToList();
            }
            ////Check For invoiceType
            //if (invoiceType == SecurityAgency.Common.Utility.EnumUtility.InvoiceType.Paid.ToString())
            //{
            //    customer = customer.Where(i => i.CustomerPaymentId != 0).ToList();
            //}
            //else if (invoiceType == SecurityAgency.Common.Utility.EnumUtility.InvoiceType.Unpaid.ToString())
            //{
            //    customer = customer.Where(i => i.CustomerPaymentId == 0).ToList();
            //}
            if (customer == null)
                return null;

            Mapper.CreateMap<getAllCustomerInvoice_Result, CustomerInvoiceViewModel>();
            return Mapper.Map<List<getAllCustomerInvoice_Result>, List<CustomerInvoiceViewModel>>(customer);
        }

        public bool CheckDailyLogOverlap(int customerId, string beginDate)
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                DateTime _beginDate = Convert.ToDateTime(beginDate);
                var overlapDailyLog = objContext.CustomerInvoices.Where(i => i.IsDeleted == false && i.CustomerId == customerId && (i.BeginDate < _beginDate && i.EndDate >= _beginDate)).FirstOrDefault();
                if(overlapDailyLog==null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
           
        }
        public int CheckInvoiceOverlap(int customerId, string beginDate, string endDate, int inoviceId)
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                DateTime _beginDate = Convert.ToDateTime(beginDate);
                DateTime _endDate = Convert.ToDateTime(endDate);
                var overlapDailyLog = objContext.CustomerInvoices.Where(i => i.IsDeleted == false && i.InvoiceId != inoviceId && i.CustomerId == customerId && (
                                                                            (_beginDate >= i.BeginDate && _beginDate <= i.EndDate)
                                                                        ||  (_endDate >= i.BeginDate && _endDate <= i.EndDate)
                                                                        ||  (i.BeginDate >= _beginDate && i.BeginDate <= _endDate)
                                                                        ||  (i.EndDate >= _beginDate && i.EndDate <= _endDate)
                                                                        )).FirstOrDefault();
                if (overlapDailyLog == null)
                {
                    return 0;
                }
                else
                {
                    return overlapDailyLog.InvoiceNo;
                }
            }
        }
    }
}
using SecurityAgency.Common.ViewModels;
using SecurityAgency.Repository;
using SecurityAgency.Repository.DbServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SecurityAgency.utility
{
   public class ExceptionHandler : IExceptionFilter
        {
            /// <summary>
            /// Catch exceptions and route to the correct page.
            /// </summary>
            /// <param name="filterContext"></param>
       public void OnException(ExceptionContext filterContext)
       {
           // to do:check if user is logged out
           //if (filterContext != null && filterContext.Exception is ExpiredSessionException)
           //{
           //    filterContext.HttpContext.Response.Clear();
           //    filterContext.Result = new RedirectResult("/logout");
           //    filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
           //}
           if (filterContext.ExceptionHandled || filterContext.Exception == null)
           {
               return;
           }

           //log the exception with elmah
           //ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
           Exception exception = filterContext.Exception;

           //work out the status code and build the result
           int statusCode = BuildStatusCode(filterContext, exception);
           bool isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
           filterContext.Result = BuildResult(filterContext, statusCode, isAjax);

           //execute the result, bypass iis7
           filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
           filterContext.ExceptionHandled = true;
           filterContext.HttpContext.Response.Clear();
           filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
           //Exception ex = new Exception();
           LogException(exception, null);
       }
           public void LogException(Exception e, string extraInfo = null)
           {
           SecurityAgencyEntities context = new SecurityAgencyEntities();

               Error_Log obj = new Error_Log();
               obj.Message = e.Message;

               if (!string.IsNullOrEmpty(extraInfo))
                   obj.Message = obj.Message + "<br/> Extar Info :" + extraInfo;

               obj.Source = e.Source;
               obj.StackTrace = e.StackTrace;
               obj.TargetSite = e.TargetSite.ToString();
               obj.ErrorDate = DateTime.Now;
               obj.ExceptionDetail = e.ToString();

               DateTime dt = (DateTime)obj.ErrorDate;


               context.Error_Log.Add(obj);
               try
               {
                   context.SaveChanges();

               }
               catch
               {
             }
       }
           

            /// <summary>
            /// Builds the correct result based on the status code.
            /// </summary>
            /// <param name="filterContext"></param>
            /// <param name="statusCode"></param>
            /// <param name="isAjax"></param>
            /// <returns></returns>
            private ActionResult BuildResult(ExceptionContext filterContext, int statusCode, bool isAjax)
            {
                ActionResult result;

                if (isAjax)
                {
                    result = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new
                        {
                            success = false,
                            exception = filterContext.Exception.ToString(),
                            exceptionMessage = filterContext.Exception.Message
                        }
                    };
                }
                else
                {
                    if (statusCode.Equals((int)HttpStatusCode.NotFound))
                    {
                        result = new ViewResult
                        {
                            ViewName = @"~/Views/Error/_NotFound.cshtml",
                            ViewData = new ViewDataDictionary(new ErrorViewModel(true, filterContext.Exception))
                        };
                    }
                    else if (statusCode.Equals((int)HttpStatusCode.Redirect))
                    {
                        result = new RedirectResult("/account/logout");
                    }
                    else
                    {
                        result = new ViewResult
                        {
                            ViewName = "~/views/Error/_InternalError.cshtml",
                            ViewData = new ViewDataDictionary(new ErrorViewModel(true, filterContext.Exception))
                        };
                    }
                }

                filterContext.HttpContext.Response.StatusCode = statusCode;
                return result;
            }

            /// <summary>
            /// Builds the status code from the exception type
            /// </summary>
            /// <param name="filterContext"></param>
            /// <param name="exception"></param>
            /// <returns></returns>
            private int BuildStatusCode(ExceptionContext filterContext, Exception exception)
            {
                if (exception is HttpException)
                {
                    return (exception as HttpException).GetHttpCode();
                }

                // to do
                //if (exception is ExpiredSessionException)
                //{
                //    return (int)HttpStatusCode.Redirect;
                //}

                return (int)HttpStatusCode.InternalServerError;
            }
        }
    }

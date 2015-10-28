using System;
using System.Web;
using App.ErrorHandling.Data;
using App.ErrorHandling.Domain;

namespace App.ErrorHandling
{
    public class PageErrorLoggingService
    {
        private readonly IPageErrorRepository _pageErrorRepository;

        public PageErrorLoggingService(IPageErrorRepository pageErrorRepository)
        {
            _pageErrorRepository = pageErrorRepository;
        }

        public virtual ErrorInfo GetErrorInfoFromException(Exception ex, string url, string userId, string appName)
        {
            var message = ex.Message;
            if (ex is HttpException)
            {
                if (ex.InnerException != null)
                {
                    var iex = ex.InnerException;
                    message = iex.Message;
                }
            }

            var errorInfo = new ErrorInfo();
            errorInfo.ApplicationName = appName;
            errorInfo.ErrorDate = DateTime.Now;
            errorInfo.ErrorDescription = ex.ToString();
            errorInfo.ErrorTitle = message;
            errorInfo.OffendingUrl = url;
            errorInfo.UserId = userId;

            return errorInfo;
        }

        public void AddLogEntry(ErrorInfo errorInfo)
        {
            errorInfo.LogId = _pageErrorRepository.Add(errorInfo);
        }
    }

    
}

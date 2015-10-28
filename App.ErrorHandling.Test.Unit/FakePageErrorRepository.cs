using System.Collections.Generic;
using App.ErrorHandling.Data;
using App.ErrorHandling.Domain;

namespace App.ErrorHandling.Test.Unit
{
    public class FakePageErrorRepository : IPageErrorRepository
    {
        readonly List<ErrorInfo> errorLog = new List<ErrorInfo>(); 

        public int Add(ErrorInfo errorInfo)
        {
            var errorLogCount = errorLog.Count;
            errorInfo.LogId = errorLogCount + 1;
            errorLog.Add(errorInfo);

            return errorInfo.LogId;
        }

        public List<ErrorInfo> GetAll()
        {
            return errorLog;
        }
    }
}
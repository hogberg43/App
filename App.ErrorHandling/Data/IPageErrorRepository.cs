using App.ErrorHandling.Domain;

namespace App.ErrorHandling.Data
{
    public interface IPageErrorRepository
    {
        int Add(ErrorInfo errorInfo);
    }
}
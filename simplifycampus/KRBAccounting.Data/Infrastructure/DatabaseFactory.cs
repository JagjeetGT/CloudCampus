
using KRBAccounting.Data;

namespace KRBAccounting.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private DataContext _dataContext;
    public DataContext Get()
    {
        return _dataContext ?? (_dataContext = new DataContext());
    }
    protected override void DisposeCore()
    {
        if (_dataContext != null)
            _dataContext.Dispose();
    }
}
}

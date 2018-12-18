using System;
using KRBAccounting.Data;

namespace KRBAccounting.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DataContext Get();
    }
}

namespace KRBAccounting.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}

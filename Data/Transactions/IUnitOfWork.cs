namespace Data.Transactions
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}

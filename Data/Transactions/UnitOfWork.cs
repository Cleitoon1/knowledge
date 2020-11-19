namespace Data.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KnowledgeContext _context;

        public UnitOfWork(KnowledgeContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

using ModernStore2.Infra.Contexts;
using System;

namespace ModernStore2.Infra.Transactions
{
    public class Uow : IUow
    {
        private readonly ModernStoreContext _context;

        public Uow(ModernStoreContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
           // Do Nothing 
        }
    }
}

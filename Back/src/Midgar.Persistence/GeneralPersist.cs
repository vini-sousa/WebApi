using Midgar.Persistence.Context;
using Midgar.Persistence.Interface;

namespace Midgar.Persistence
{
    public class GeneralPersist : IGeneralPersist
    {
        private readonly MidgarContext _context;
        public GeneralPersist(MidgarContext context)
        {
            _context = context;
        }

        void IGeneralPersist.Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        void IGeneralPersist.Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        void IGeneralPersist.Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);       
        }

        void IGeneralPersist.DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        async Task<bool> IGeneralPersist.SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
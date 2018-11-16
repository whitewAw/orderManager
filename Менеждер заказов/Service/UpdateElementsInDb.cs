using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.Service
{
    public class IEntity
    {
        public int Id;
    }

    public class UpdateElementsInDb
    {
        public static void Update<T>(T entity, Func<T, int> getKey, MODBEntities _context) where T:class
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = _context.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                T attachedEntity = set.Find(getKey(entity));

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }
    }
}

using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Intefaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
        IRepository<Person> Person { get; }
        IRepository<RelatedPerson> RelatedPerson { get; }
    }
}

using Domain;
using Domain.Intefaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext _context;
        private Hashtable _repositories;
        public IRepository<Person> Person { get; }
        public IRepository<RelatedPerson> RelatedPerson { get; }
        public UnitOfWork(DataContext context,
            IRepository<Person> person,
            IRepository<RelatedPerson> relatedPerson
            )
        {
            _context = context;
            Person = person;
            RelatedPerson = relatedPerson;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }
    }
}
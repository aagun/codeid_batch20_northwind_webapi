using Northwind.Domain.Base;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Repositories;
using Northwind.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private IRegionRepository _regionRepository;

        //private Lazy<IRegionRepository> _regionRepositoryLazy;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
            //_regionRepositoryLazy = new Lazy<IRegionRepository>(()=> new RegionRepository(adoContext));
        }

        //public IRegionRepository RegionRepository => _regionRepositoryLazy.Value;

        public IRegionRepository RegionRepository
        {
            get
            {
                if(_regionRepository == null)
                {
                    _regionRepository = new RegionRepository(_adoContext);
                }
                return _regionRepository;
            }        
        }


            
    }
}

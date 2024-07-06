using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        IGenericRepository<Product> _productRepository;
        IGenericRepository<Category> _categoryRepository;
        IGenericRepository<Brand> _brandRepository;
        IRoleRepository _roleRepository;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IGenericRepository<Product> ProductRepository {
            get
            {
                if (_productRepository == null)
                    _productRepository = new GenericRepository<Product>(_storeContext);

                return _productRepository;
            }
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new GenericRepository<Category>(_storeContext); 
                
                return _categoryRepository;
            }
        }

        public IGenericRepository<Brand> BrandRepository
        {
            get
            {
                if (_brandRepository == null)
                    _brandRepository = new GenericRepository<Brand>(_storeContext);

                return (_brandRepository);
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new RoleRepository(_storeContext);

                return  _roleRepository;
            }
        }
        public void SaveChanges()
        {
            _storeContext.SaveChanges();
        }
    }
}

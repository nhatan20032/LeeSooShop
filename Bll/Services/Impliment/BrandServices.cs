using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;

namespace Bll.Services.Impliment
{
    public class BrandServices : DbServices, IBrandServices
    {
        public BrandServices()
        {            
        }

        public bool Create(Brand brand)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Brand> GetAll(PagingModels page)
        {
            throw new NotImplementedException();
        }

        public Brand GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataTableResult> List(PagingModels page)
        {
            throw new NotImplementedException();
        }

        public bool Update(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}

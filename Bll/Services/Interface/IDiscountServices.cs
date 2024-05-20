using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IDiscountServices
    {
        public bool Create(Discount discount);
        public bool Update(Discount discount);
        public bool Delete(int id);
        public List<Discount> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Discount GetById(int id);
    }
}

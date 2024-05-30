using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IDiscountServices
    {
        public Task<bool> Create(Discount discount);
        public Task<bool> Update(Discount discount);
        public Task<bool> Delete(int id);
        public Task<List<Discount>> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Task<Discount> GetById(int id);
    }
}

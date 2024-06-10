using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
	public interface IBannerServices
	{
		public Task<bool> Create(Banner brand);
		public Task<bool> Update(Banner brand);
		public Task<bool> Delete(int id);
		public Task<List<Banner>> GetAll(PagingModels page);
		public Task<DataTableResult> List(PagingModels page);
		public Task<Banner> GetById(int id);
	}
}

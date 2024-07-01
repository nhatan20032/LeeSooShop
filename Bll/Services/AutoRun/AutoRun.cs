using Bll.Services.Impliment;
using Microsoft.Extensions.Caching.Memory;
using Quartz;
using ServiceStack.Logging;

namespace Bll.Services.AutoRun
{
    public class AutoRun : IJob
    {
        //private readonly UserServices _userServices;
        //private readonly User_VipServices _userVipServices;
        //private readonly IMemoryCache _memoryCache;

        //public VipExpiredJob(UserServices userServices, User_VipServices userVipServices, IMemoryCache memoryCache)
        //{
        //    _userServices = userServices;
        //    _userVipServices = userVipServices;
        //    _memoryCache = memoryCache;
        //}

        //public Task Execute(IJobExecutionContext context)
        //{
        //    try
        //    {
        //        List<User_Vip> userVips;
        //        // Kiểm tra xem danh sách User_Vip đã được lưu trong cache chưa
        //        if (!_memoryCache.TryGetValue("UserVipsToCheck", out userVips))
        //        {
        //            // Nếu chưa có trong cache, lấy từ service và lưu vào cache
        //            userVips = _userVipServices.GetUserVipsToCheck();
        //            _memoryCache.Set("UserVipsToCheck", userVips, TimeSpan.FromMinutes(30)); // Cập nhật lại Cache sau mỗi 30 phút
        //        }
        //        if (userVips != null && userVips.Count == 0)
        //        {
        //            // Kiểm tra và xử lý từng tài khoản trong danh sách
        //            foreach (var obj in userVips)
        //            {
        //                if (obj.vip_expired != null && obj.active)
        //                {
        //                    DateTime currentDate = DateTime.Now;
        //                    TimeSpan timeUntilExpired = obj.vip_expired.Value - currentDate;
        //                    if (timeUntilExpired.Days <= 3)
        //                    {
        //                        // Tạo thông báo cho người dùng là hết vip
        //                        Log.Warning($"Tài khoản user_id = {obj.user_id} còn lại là 3 ngày sử dụng!");
        //                    }
        //                    if (timeUntilExpired.Days <= 0)
        //                    {
        //                        Log.Warning($"Tài khoản user_id = {obj.user_id} đã hết thời gian sử dụng VIP!");
        //                        bool isSuccess = _userServices.Set_Expired_Vip(obj.user_id);
        //                        if (isSuccess)
        //                        {
        //                            // Đặt active = false để không lặp lại sau nữa
        //                            obj.active = false;
        //                        }
        //                    }
        //                }
        //            }
        //            // Xóa các tài khoản hết hạn khỏi danh sách trong cache
        //            userVips.RemoveAll(uv => uv.vip_expired != null && uv.vip_expired <= DateTime.Now && uv.active);
        //            _memoryCache.Set("UserVipsToCheck", userVips, TimeSpan.FromMinutes(30)); // Cập nhật danh sách trong cache sau mỗi 30 phút
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Lỗi class VipExpiredJob");
        //    }
        //    // Sử dụng _userServices để cập nhật trạng thái tài khoản khi hết hạn
        //    return Task.CompletedTask;
        //}
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}

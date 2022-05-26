using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Demo.FileUpload.Model
{
    public interface IBannerService
    {
        List<Banner> GetList(BannerFilter filter, ResultPaging paging);
        Banner GetData(int bannerId);
        Banner SaveData(Banner banner);
        bool DeletData(int bannerId);
    }
}

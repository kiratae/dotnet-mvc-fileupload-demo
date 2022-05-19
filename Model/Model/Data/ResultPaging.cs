using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Demo.FileUpload.Model
{
    public class ResultPaging
    {
        public int ItemPerPage { get; set; }

        public int PageNo { get; set; }

        public int TotalItem { get; set; }

        public ResultPaging(int itemPerPage, int pageNo)
        {
            ItemPerPage = itemPerPage;
            PageNo = pageNo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbModel.ViewModel.SubFunction
{
    public class SubFunctionVM
    {
        public long SubFunctionId { get; set; }
        public string SubFunctionName { get; set; }
        public long? ParentFunctionId { get; set; }
        public long MainFunctionId { get; set; }
    }
}

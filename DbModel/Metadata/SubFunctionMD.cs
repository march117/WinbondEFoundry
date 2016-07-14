using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DbModel
{
    [MetadataType(typeof(SubFunctionMD))]
    public partial class SubFunction
    {
        public SubFunction()
        {
            CreateDate = DateTime.Now;
        }
    }

    public class SubFunctionMD
    {
        [DisplayName("次功能表流水號"),Key]
        public long SubFunctionId { get; set; }

        [DisplayName("次功能表名稱"),Required]
        public string SubFunctionName { get; set; }

        [DisplayName("主功能表分類"), Required]
        public long MainFunctionId { get; set; }

        [DisplayName("次功能表流水號")]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("是否生效"), Required]
        public bool IsActive { get; set; }

        [DisplayName("父層功能表分類")]
        public Nullable<long> ParentFunctionId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace DbModel
{
    [MetadataType(typeof(GroupsMD))]
    public partial class Groups
    {
        public Groups(){
            CreateDate = DateTime.Now;
            IsActive = true;
        }        
    }

    public class GroupsMD
    {
        [DisplayName("群組流水號"), Key, HiddenInput(DisplayValue = false)]
        public long GroupId { get; set; }

        [DisplayName("群組名稱"), Required]
        public string GroupName { get; set; }

        [DisplayName("是否啟用"), Required]
        public bool IsActive { get; set; }

        [DisplayName("建立時間"), Required, HiddenInput(DisplayValue = false)]
        public System.DateTime CreateDate { get;set; }
    }
}

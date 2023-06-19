using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlace.DataLayer.Entities.Stores
{
    public class Store:BaseEntity
    {
        #region properties
        public long UserId { get; set; }
        [Display(Name ="نام فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string StoreName { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Phone { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Mobile { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Address { get; set; }

        [Display(Name = "توضیحات فروشگاه")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string? Description { get; set; }

        [Display(Name = "یادداشت ادمین در مورد فروشگاه")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string? AdminDescription { get; set; }

        [Display(Name = "تایید/عدم تایید فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public StoreAcceptanceState StoreAcceptanceState { get; set; }

        [Display(Name = "توضیحات تایید/عدم تایید فروشگاه")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string? StoreAcceptanceDescription { get; set; }
        #endregion

        #region relations
        public User User { get; set; }
        #endregion
    }

    public enum StoreAcceptanceState
    {
        [Display(Name ="در حال بررسی")]
        UnderProgress,
        [Display(Name = "تایید شده")]
        Accepted,
        [Display(Name = "رد شده")]
        Rejected
    }
}

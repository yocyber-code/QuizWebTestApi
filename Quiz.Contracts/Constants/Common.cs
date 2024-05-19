using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Constants
{
    public class Common
    {
        public const string DefaultLanguage = "en";

        public const string ImageRoot = "{Comcode}/Images/{Category}/";
        public const string VideoRoot = "{Comcode}/Videos/{Category}/";
        public const string FileRoot = "{Comcode}/Files/{Category}/";

        public const string Successfully = "Successfully.";
        public const string ErrorDuplicated = "The primary has duplicated.";
        public const string Notfound = "Not found.";
        public const string UnSupportLocation = "Not found.";


        public const string RequiredValidation = "โปรดระบุ {0}";
        public const string ExaclyValidation = "โปรดระบุ {1} {0} ตัวอักษร";
        public const string LengthValidation = "โปรดระบุ {2} {0} หรือ {1} ตัวอักษร";
        public const string MaxLengthValidation = "โปรดระบุ {1} ไม่เกิน {0} ตัวอักษร";
        public const string UniqueValidation = "{0} นี้มีอยู่แล้วในระบบ";
        public const string ExistingValidation = "{0}ไม่มีในระบบ";
        public const string RestricToChange = "ไม่สามารถแก้ไข {0} ได้";
        public const string MaxValueValidation = "โปรดระบุ {0} ไม่เกิน {1}";
        public const string GreaterThanZeroValidation = "โปรดระบุ {0} มากกว่า 0";

        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public enum StorageType
        {
            Local = 1,
            Blob = 2,
            Remote = 3
        }

        public enum FileTypeEnum
        {
            Image = 1,
            Document = 2,
            Video = 3
        }
        public enum OrderStatus
        {
            Unpaid,
            Pending,
            Shipping,
            Completed,
            Cancelled
        }
        public enum UserRoleEnum
        {
            [Description("เจ้าหน้าที่")]
            Admin = 1,
            [Description("ผู้ซื้อ")]
            Customer = 2,
        }
        public enum Gender
        {
            Male,
            Female
        }

        public enum OrderStatusEnum
        {
            [Description("ยกเลิก")]
            Cancelled = -1,
            [Description("รอดำเนินการ")]
            Pending = 1,
            [Description("กำลังจัดเตรียม")]
            Processing = 3,
            [Description("จัดส่ง")]
            Delivered = 4,
        }
        //public enum InvoiceTypeEnum
        //{
        //    [Display(Name = "บิลเงินสด")]
        //    MoneyInvoice = 1,
        //    [Display(Name = "ใบกำกับภาษี")]
        //    TaxInvoice = 2
        //}

        //public enum PlanStatusEnum
        //{
        //    [Description("เข้าเยี่ยม")]
        //    Visit = 1,
        //    [Description("วางแผน")]
        //    Plan = 2,
        //    [Description("พิเศษ")]
        //    Extra = 3,
        //    [Description("สรรหา")]
        //    Lead = 4
        //}



    }
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }
            return value.ToString();
        }
    }
}

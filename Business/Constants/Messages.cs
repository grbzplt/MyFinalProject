using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";       
        public static string ProductsListed = "Ürünler listelendi";        
        public static string ProductUpdated = "Ürün güncellendi";

        public static string CategoryAdded = "Kategori eklendi";
        public static string CategoryUpdated = "Kategori güncellendi";
        public static string CategoriesListed="Kategoriler listelendi";

        public static string MaintenanceTime = "Sistem bakımda";

        internal static string ProductCountOfCategoryError = "Bir kategoride ... Ürün sayısın aştınız";
        internal static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        internal static string CategoryLimitExceded="Kategori limiti aşıldığı için yeni ürün eklenemiyor.";
        internal static string AuthorizationDenied= "AuthorizationDenied";
    }
}

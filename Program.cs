using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscountConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal orderTotal = 1000M;

            int usedDiscount = 0;

            //İndirim tanımlanacak kuralları diziye atıp rastgele bir tanesini seçeceğiz.
            var availableKeys = new string[] { "GoldCard", "SilverCard", "Affiliate", "OldMember", "None" };

            var random = new Random();

            //0 ile dizi genişliği arasında bir sayı tuttuk.
            int randomKey = random.Next(0, availableKeys.Length);

            //Tutulan rastgele sayı ile diziden bir değer seçildi.
            string selectedRule = availableKeys[randomKey];

            //Kullanılacak kural setleri için indirim oranları ve platform bilgileri tanımlanıyor.
            var rules = new List<DiscountRule>
            {
                new DiscountRule
                {
                    Key="GoldCard",
                    Rate=30,
                    Device=Device.Web
                },
                new DiscountRule
                {
                    Key="SilverCard",
                    Rate=20,
                    Device=Device.Web
                },
                new DiscountRule
                {
                    Key="Affiliate",
                    Rate=10,
                    Device=Device.Web
                },
                new DiscountRule
                {
                    Key="OldMember",
                    Rate=5,
                    Device=Device.Web
                },
                new DiscountRule
                {
                    Key="Total",
                    Rate=5,
                    Device=Device.Web
                }
            };

            //Rastgele seçilen kural için kurallar içerisindeki indirim seti seçiliyor.
            var rule = rules.Where(r => r.Key == selectedRule).FirstOrDefault();


            //Seçilen kurala göre indirim uygulanıyor.
            switch (selectedRule)
            {
                case "GoldCard":
                    orderTotal -= (orderTotal * rule.Rate) / 100;
                    usedDiscount = rule.Rate;
                    break;
                case "SilverCard":
                    orderTotal -= (orderTotal * rule.Rate) / 100;
                    usedDiscount = rule.Rate;
                    break;
                case "Affiliate":
                    orderTotal -= (orderTotal * rule.Rate) / 100;
                    usedDiscount = rule.Rate;
                    break;
                case "OldMember":
                    orderTotal -= (orderTotal * rule.Rate) / 100;
                    usedDiscount = rule.Rate;
                    break;
                default:
                    break;
            }

            //Eğer herhangi bir indirim uygulanamazsa müşterinin genel toplamına göre %5 * 200$ için indirim uygulanıyor.
            if (usedDiscount == 0)
            {
                //Genel toplam indirim oranı kural setinden seçiliyor.
                rule = rules.Where(r => r.Key == "Total").FirstOrDefault();

                //Eğer genel toplam 200 ve üzerinde ise kuralı uygula.
                if (orderTotal >= 200)
                {
                    //Her 200$ için %5 uygula
                    var loop = orderTotal / 200;
                    var discount = loop * rule.Rate;
                    orderTotal -= (orderTotal * discount) / 100;
                    usedDiscount = (int)discount;
                }
            }

            //Kullanılan kuralı, genel toplamı ve indirim oranını ekrana yaz.
            Console.WriteLine($"Selected Rule: {selectedRule}");
            Console.WriteLine($"Order Total: {orderTotal}");
            Console.WriteLine($"Used Discount: %{usedDiscount}");

            Console.ReadLine();
        }

        public class DiscountRule
        {
            public string Key { get; set; }
            public int Rate { get; set; }
            public Device Device { get; set; }
        }

        public enum Device
        {
            Other,
            Web,
            MobileApp
        }
    }
}
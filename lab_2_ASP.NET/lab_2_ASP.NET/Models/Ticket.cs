using System;
using System.Collections.Generic;

namespace lab_2_ASP.NET.Models
{
    public class Ticket
    {
        public string FullName { get; set; }
        public string PassportData { get; set; }
        public string NumberPlace { get; set; }
        public double Price { get; set; }

        public static List<Ticket> Initializer(int ticketNumber)
        {
            List<Ticket> collection = new List<Ticket>();
            Random randObj = new Random(1);

            string[] fullName = { "Гусь Ольга Владимировна", "Голубь Татьяна Владимировна", "Сорока Татьяна Владимировна", "Аист Виктория Владимировна", "Ястреб Ирина Владимировна",
                "Беркут Оксана Владимировна", "Ласточка Диана Владимировна", "Снегирь Татьяна Владимировна", "Сеница Анастасия Владимировна", "Филин Иван Иванович" };
            string[] placeNumber = { "A", "B", "C", "D", "E", "F", "G", "H" };
            for (int i = 0; i < ticketNumber; i++)
            {
                string name = fullName[randObj.Next(fullName.Length)];
                string passportDate = "HB" + randObj.Next(100, 999).ToString() + randObj.Next(10, 99).ToString() + randObj.Next(10, 99).ToString();
                string number = placeNumber[randObj.Next(placeNumber.Length)] + (i + 1).ToString();
                int price = 50 * randObj.Next(1, 9);
                collection.Add(new Ticket { FullName = name, PassportData = passportDate, NumberPlace = number, Price = price });
            }

            return collection;
        }
    }
}

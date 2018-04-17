using System;
using System.Collections.Generic;

namespace lab_2_ASP.NET.Models
{
    public class Aircraft
    {
        public string Mark { get; set; }
        public int Capasity { get; set; }
        public double Carrying { get; set; }

        public static List<Aircraft> Initializer(int aircraftNumber)
        {
            List<Aircraft> collection = new List<Aircraft>();
            Random randObj = new Random(1);

            string[] marks = { "Airbus-A310", "Airbus-A380", "Boeing-737", "Boeing-777", "ИЛ-62", "ТУ-154", "Adam A500 Adamjet", "Aerospatiale N262", "Spike S-512", "Aero Boero AB-95" };
            for (int aircraftID = 0; aircraftID < aircraftNumber; aircraftID++)
            {
                string mark = marks[randObj.Next(marks.Length)];
                int capasity = randObj.Next(300, 800);
                double carrying = 1000 * randObj.NextDouble();
                collection.Add(new Aircraft { Mark = mark, Capasity = capasity, Carrying = carrying });
            }

            return collection;
        }
    }
}

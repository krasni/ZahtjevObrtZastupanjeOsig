using Itenso.TimePeriod;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VjezbaFreeSpire
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- time interval 1 ---
            TimeInterval timeInterval1 = new TimeInterval(
              new DateTime(2011, 5, 8),
              new DateTime(2011, 5, 9));
            Console.WriteLine("TimeInterval1: " + timeInterval1);
            // > TimeInterval1: [01.05.2011 - 09.05.2011] | 1.00:00

            // --- time interval 2 ---
            TimeInterval timeInterval2 = new TimeInterval(
              timeInterval1.End,
              timeInterval1.End.AddDays(1));
            Console.WriteLine("TimeInterval2: " + timeInterval2);
            // > TimeInterval2: [04.05.2011 - 10.05.2011] | 1.00:00

            // --- relation ---
            Console.WriteLine("Relation: " + timeInterval1.GetRelation(timeInterval2));
            // > Relation: EndTouching
            Console.WriteLine("Intersection: " +
                               timeInterval1.GetIntersection(timeInterval2));
            // > Intersection: [09.05.2011]

            timeInterval1.EndEdge = IntervalEdge.Open;
            Console.WriteLine("TimeInterval1: " + timeInterval1);
            // > TimeInterval1: [08.05.2011 - 09.05.2011) | 1.00:00

            timeInterval2.StartEdge = IntervalEdge.Open;
            Console.WriteLine("TimeInterval2: " + timeInterval2);
            // > TimeInterval2: (09.05.2011 - 10.05.2011] | 1.00:00

            // --- relation ---
            Console.WriteLine("Relation: " + timeInterval1.GetRelation(timeInterval2));
            // > Relation: Before
            Console.WriteLine("Intersection: " +
                               timeInterval1.GetIntersection(timeInterval2));

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splite
{
    class Program
    {
        static void Main(string[] args)
        {
            var ps = new PageSizeCollectionGeneration(20, true);
            var temp = ps.GetCollection;
            foreach (var obj in temp)
                Console.WriteLine($"size: [0] {obj[0]}; [1] {obj[1]}; [2] {obj[2]}; [3] {obj[3]}");

            Console.WriteLine(new string('-', 50));

            var splite = new SpliteDocument(temp);
            splite.DebugPrintBaseArray();

            // Delay
            Console.ReadKey();
        }
    }
}

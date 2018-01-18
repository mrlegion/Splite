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
            var ps = new PageSizeCollectionGeneration(100, true);

            var splite = new SpliteDocument(ps.GetCollection);
            splite.DebugPrintBaseArray();

            Console.WriteLine(new string('-', 50));

            splite.DebugPrintResultArray();

            // Delay
            Console.ReadKey();
        }
    }
}

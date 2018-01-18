using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splite
{
    class SpliteDocument
    {
        private List<int[]> _items;
        private readonly int _pageCount = -1;

        public SpliteDocument(List<int[]> list)
        {
            if (list.Count == 0)
                throw new ArgumentNullException();

            _items = list;
            _pageCount = _items.Count;

            // Create base array
            CreateBaseArray();
        }

        #region PUBLIC:



        #endregion

        #region PRIVATE:

        private void CreateBaseArray()
        {
            var temp = new List<int[]>();
            foreach (int[] item in _items)
            {
                temp.Add(new[] { item[1], item[2] });
            }

            _items = temp;
        }

        private void SotrBaseArray()
        {
            
        }

        #endregion

        #region DEBUG:

        public void DebugPrintBaseArray()
        {
            int count = -1;
            foreach (int[] item in _items)
            {
                Console.WriteLine($"Item [ {++count} ]:\t [0 - {item[0]}]\t [1 - {item[1]}]");
            }
        }

        #endregion
    }
}

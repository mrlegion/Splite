using System;
using System.Collections.Generic;
using Pages = System.Collections.Generic.Dictionary<string, int[]>;

namespace Splite
{
    class SpliteDocument
    {
        private List<int[]> _items;
        private Pages _result;
        private readonly int _pageCount = -1;
        private Dictionary<string, int> _area = new Dictionary<string, int>()
        {
            { "a5", 31080 },
            { "a4", 62370 },
            { "a3", 124740 }
        };

        public SpliteDocument(List<int[]> list)
        {
            if (list.Count == 0)
                throw new ArgumentNullException();

            _items = list;
            _pageCount = _items.Count;
            _result = new Pages();

            // Sort array
            SotrBaseArray();

        }

        #region PUBLIC:

        public Pages GetSpliteList
        {
            get { return _result; }
        }

        #endregion

        #region PRIVATE:

        private void SotrBaseArray()
        {
            int area;
            int[] temp;
            for (int page = 0; page < _pageCount; page++)
            {
                area = _items[page][1] * _items[page][2];
                foreach(var format in _area)
                {
                    if (format.Value == area)
                    {
                        if(_result.ContainsKey(format.Key))
                        {
                            temp = _result[format.Key];
                            AddToArray(ref temp, page);
                            _result[format.Key] = temp;
                        }
                        else
                        {
                            _result.Add(format.Key, new int[] { page });
                        }
                    }
                }
            }
        }

        private void AddToArray(ref int[] array, int value)
        {
            int[] temp = new int[array.Length + 1];
            Array.Copy(array, 0, temp, 0, array.Length);
            temp[temp.Length - 1] = value;
            array = temp;
        }

        #endregion

        #region DEBUG:

        public void DebugPrintBaseArray()
        {
            int count = 0;
            foreach (int[] item in _items)
            {
                Console.WriteLine($"Item [ {++count} ]:\t [0 - {item[1]}]\t [1 - {item[2]}]");
            }
        }

        public void DebugPrintResultArray()
        {
            foreach(var pages in _result)
            {
                Console.WriteLine($"Format name: << {pages.Key} >>");
                foreach (var number in pages.Value)
                    Console.Write($"{number + 1} ");
                Console.WriteLine("\n" + new string('-', 20));
            }
        }

        #endregion
    }
}

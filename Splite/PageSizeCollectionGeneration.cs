using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splite
{
    public class PageSizeCollectionGeneration : IEnumerable, IEnumerator
    {
        Random random = new Random();
        private List<int[]> _items;
        private int _capacity = 0;
        // Format list for Generate List
        private readonly int[][] _formatsList = new[]
        {
            new []{ 210, 297 },
            new []{ 297, 420 },
            new []{ 148, 210 },
            new []{ 297, 210 },
            new []{ 420, 297 },
            new []{ 210, 148 },
        };

        public List<int[]> GetCollection => _items;

        #region Constructors

        public PageSizeCollectionGeneration(int size, bool generate)
        {
            _capacity = (size > 0) ? size : random.Next(10, 100);

            _items = new List<int[]>();

            if (generate)
                Generate();
        }

        public PageSizeCollectionGeneration()
            : this(0, true)
        {
        }

        public PageSizeCollectionGeneration(bool generate)
            : this(0, generate)
        {
        }

        #endregion

        public void Generate()
        {
            int[] size;
            for (int i = 0; i < _capacity; i++)
            {
                size = _formatsList[random.Next(0, _formatsList.Length)];
                _items.Add(new[] { 0, size[0], size[1], 0 });
            }
        }

        #region Interface Impliment

        private int _position = -1;

        public object Current => (_position >= 0 || _position < _items.Count) ? _items[_position] : null;

        public void Reset()
        {
            _position = -1;
        }

        public bool MoveNext()
        {
            if (_position >= 0 && _position < _items.Count)
            {
                ++_position;
                return true;
            }

            Reset();
            return false;
        }

        public IEnumerator GetEnumerator() => (IEnumerator)this;

        #endregion
    }
}

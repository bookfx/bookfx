namespace BookFx.Tests.Functional
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OnceEnumerable : IEnumerable<int>
    {
        private readonly int _size;
        private bool _isEnumerated;

        public OnceEnumerable(int size)
        {
            _size = size;
            _isEnumerated = false;
        }

        public IEnumerator<int> GetEnumerator()
        {
            if (_isEnumerated)
            {
                throw new InvalidOperationException();
            }

            _isEnumerated = true;

            return new OnceEnumerator(_size);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class OnceEnumerator : IEnumerator<int>
        {
            private readonly int _size;
            private int _current;

            public OnceEnumerator(int size)
            {
                _size = size;
                _current = -1;
            }

            public int Current => _current < 0 ? throw new InvalidOperationException() : _current;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _current++;
                return _current < _size;
            }

            public void Reset() => throw new InvalidOperationException();

            public void Dispose()
            {
                // There is nothing to be disposed.
            }
        }
    }
}
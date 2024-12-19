using System.Runtime.CompilerServices;
using UnityEngine;

namespace Inventories
{
    public struct BoundsInt2
    {
        private readonly Vector2Int _minPosition;
        private Vector2Int _maxPosition;
        private Vector2Int _size;
        private Vector2Int[] _positions;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BoundsInt2(Vector2Int position, Vector2Int size)
        {
            _minPosition = position;
            _size = size;
            _maxPosition = new Vector2Int(position.x + size.x, position.y + size.y);
            _positions = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int[] GetAllPositions()
        {
            if (_positions == null)
            {
                _positions = new Vector2Int[_size.x * _size.y];

                int index = 0;


                for (int x = 0; x < _size.x; x++)
                {
                    for (int y = 0; y < _size.y; y++)
                    {
                        _positions[index] = _minPosition + new Vector2Int(x, y);

                        Debug.Log(_positions[index]);
                        index++;
                    }
                }
            }

            return _positions;
        }
    }
}
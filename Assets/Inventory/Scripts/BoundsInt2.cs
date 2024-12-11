using System.Runtime.CompilerServices;
using UnityEngine;

namespace Inventories
{
    public struct BoundsInt2
    {
        private Vector2Int _minPosition;
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
        public Vector2Int[] GetPositions()
        {
            if (_positions == null)
            {
                _positions = new Vector2Int[_size.x * _size.y];
                for (int y = 0; y < _size.y; y++)
                {
                    for (int x = 0; x < _size.x; x++)
                    {
                        _positions[x + y] = _minPosition + new Vector2Int(x, y);
                    }
                }
            }

            return _positions;
        }
    }
}
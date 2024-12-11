using System.Runtime.CompilerServices;
using UnityEngine;

namespace Inventories
{
    public struct InventoryCell
    {
        public BoundsInt2 Bounds => _bounds;
        
        private BoundsInt2 _bounds;
        private int _count;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InventoryCell(int x, int y, Vector2Int size)
        {
            _bounds = new BoundsInt2(new Vector2Int(x, y), size);
            _count = 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InventoryCell(Vector2Int position, Vector2Int size)
        {
            _bounds = new BoundsInt2(position, size);
            _count = 1;
        }
        
        public void AddCount(int count)
        {
            _count += count;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
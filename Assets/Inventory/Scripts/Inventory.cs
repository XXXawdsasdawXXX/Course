using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable NotResolvedInText

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public int Width => _width;
        public int Height => _height;
        public int Count => _cells?.Count ?? 0;

        private readonly int _width;
        private readonly int _height;

        private readonly Item[,] _items;
        private readonly Dictionary<Item, InventoryCell> _cells;


        public Inventory(in int width, in int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _width = width;
            _height = height;

            _items = new Item[_width, _height];
            _cells = new Dictionary<Item, InventoryCell>();
        }

        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException($"The items collection cannot be null.");
            }

            foreach ((Item item, Vector2Int position) in items)
            {
                AddItem(item, position);
            }
        }

        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException($"The items collection cannot be null.");
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = x + y;

                    if (items.Length < index)
                    {
                        Item item = items[index];

                        AddItem(item, x, y);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException($"The items collection cannot be null.");
            }

            Dictionary<Item, Vector2Int> collection = items.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach ((Item item, Vector2Int position) in collection)
            {
                AddItem(item, position);
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException($"The items collection cannot be null.");
            }

            Item[] itemsArray = items.ToArray();

            for (int y = 0; y < itemsArray.Length; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = x + y;

                    if (itemsArray.Length < index)
                    {
                        AddItem(itemsArray[index], x, y);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            return _сanAddItem(item, position.x, position.y);
        }

        private bool _сanAddItem(in Item item, in int posX, in int posY)
        {
            if (item == null || !_isInsideBounds(posX, posY) || _cells.ContainsKey(item))
            {
                return false;
            }

            if (!_isCorrectSize(item.Size))
            {
                throw new ArgumentException();
            }

            bool isCan = true;

            for (int y = posY; y < posY + item.Size.y; y++)
            {
                for (int x = posX; x < posX + item.Size.x; x++)
                {
                    if (!IsFree(x, y))
                    {
                        isCan = false;
                    }
                }
            }

            return isCan;
        }

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            if (!_сanAddItem(item, position.x, position.y))
            {
                return false;
            }

            InventoryCell cell = new(position, item.Size);

            _cells.Add(item, cell);

            _setItemMatrixValue(position, item.Size, item);

            OnAdded?.Invoke(item, position);

            return true;
        }

        public bool AddItem(in Item item, in int posX, in int posY)
        {
            if (!_сanAddItem(item, posX, posY))
            {
                return false;
            }

            Vector2Int position = new Vector2Int(posX, posY);

            InventoryCell cell = new(position, item.Size);

            _cells.Add(item, cell);

            _setItemMatrixValue(position, item.Size, item);

            OnAdded?.Invoke(item, position);

            return true;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (item == null || _cells.ContainsKey(item))
            {
                return false;
            }

            if (!_isCorrectSize(item.Size))
            {
                throw new ArgumentException();
            }

            return FindFreePosition(item.Size, out Vector2Int position);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (item == null || _cells.ContainsKey(item))
            {
                return false;
            }

            if (FindFreePosition(item.Size, out Vector2Int position))
            {
                AddItem(item, position);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            if (!_isCorrectSize(size))
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int y = 0; y <= _height - size.y; y++)
            {
                for (int x = 0; x <= _width - size.x; x++)
                {
                    if (_isFreeArea(x, y, size))
                    {
                        freePosition = new Vector2Int(x, y);
                        return true;
                    }
                }
            }

            freePosition = Vector2Int.zero;
            return false;
        }

        private bool _isCorrectSize(Vector2Int value)
        {
            return value.x > 0 && value.y > 0 && value.x <= _width && value.y <= _height;
        }

        private bool _isCorrectPosition(Vector2Int position)
        {
            return position.x > 0 && position.y > 0 && position.x < _width && position.y < _height;
        }

        private bool _isFreeOrIntersects(Vector2Int position, Item item)
        {
            for (int y = position.y; y < position.y + item.Size.y; y++)
            {
                for (int x = position.x; x < position.x + item.Size.x; x++)
                {
                    if (_items[x, y] != null && !_items[x, y].Equals(item))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool _isFreeArea(int startPositionX, int startPositionY, Vector2Int size)
        {
            for (int y = startPositionY; y < startPositionY + size.y; y++)
            {
                for (int x = startPositionX; x < startPositionX + size.x; x++)
                {
                    if (_items[x, y] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            if (item == null)
            {
                return false;
            }

            return _cells.ContainsKey(item);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            return _isInsideBounds(position.x, position.y) && _items[position.x, position.y] != null;
        }

        public bool IsOccupied(in int x, in int y)
        {
            return _isInsideBounds(x, y) && _items[x, y] != null;
        }

        /// <summary>
        /// Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
        {
            return _isInsideBounds(position.x, position.y) && _items[position.x, position.y] == null;
        }

        public bool IsFree(in int x, in int y)
        {
            return _isInsideBounds(x, y) && _items[x, y] == null;
        }

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
        {
            return RemoveItem(item, out Vector2Int position);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = Vector2Int.zero;

            if (item == null || !_cells.ContainsKey(item))
            {
                return false;
            }

            Vector2Int[] positions = _cells[item].Bounds.GetAllPositions();

            position = positions[0];

            foreach (Vector2Int pos in positions)
            {
                _items[pos.x, pos.y] = null;
            }

            _cells.Remove(item);

            OnRemoved?.Invoke(item, position);

            return true;
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            if (_isInsideBounds(position.x, position.y))
            {
                return _items[position.x, position.y];
            }

            return null;
        }

        public Item GetItem(in int x, in int y)
        {
            if (_isInsideBounds(x, y))
            {
                Item item = _items[x, y];

                if (item == null)
                {
                    throw new NullReferenceException();
                }

                return item;
            }

            throw new IndexOutOfRangeException();
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            item = null;

            if (_isInsideBounds(position.x, position.y))
            {
                item = _items[position.x, position.y];
            }

            return item != null;
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            item = null;

            if (_isInsideBounds(x, y))
            {
                item = _items[x, y];
            }

            return item != null;
        }

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }

            if (_cells.TryGetValue(item, out InventoryCell cell))
            {
                return cell.Bounds.GetAllPositions();
            }

            throw new KeyNotFoundException();
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            if (item == null || !_cells.ContainsKey(item))
            {
                positions = null;
                return false;
            }

            positions = _cells[item].Bounds.GetAllPositions();
            return true;
        }
        

        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            if (_cells.Count == 0)
            {
                return;
            }

            _cells.Clear();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _items[x, y] = null;
                }
            }

            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            int count = 0;

            foreach ((Item item, InventoryCell cell) in _cells)
            {
                if (item.Name == name)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Moves a specified item to a target position if it exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int newPosition)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            
            if (!_isCorrectPosition(newPosition + item.Size))
            {
                return false;
            }

            if (_cells.ContainsKey(item) && _isFreeOrIntersects(newPosition, item))
            {
                Vector2Int[] oldPositions = _cells[item].Bounds.GetAllPositions();

                foreach (Vector2Int position in oldPositions)
                {
                    _items[position.x, position.y] = null;
                }

                _cells[item].Move(newPosition, item.Size);

                Vector2Int[] currentPositions = _cells[item].Bounds.GetAllPositions();

                foreach (Vector2Int position in currentPositions)
                {
                    _items[position.x, position.y] = item;
                }

                OnMoved?.Invoke(item, newPosition);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Reorganizes inventory space to make the free area uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            Item[] sortedItems = _cells.Keys.OrderByDescending(item => item.Size.x * item.Size.y).ToArray();

            _cells.Clear();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _items[x, y] = null;
                }
            }

            foreach (Item item in sortedItems)
            {
                FindFreePosition(item.Size, out Vector2Int position);
                
                _cells.Add(item, new InventoryCell(position, item.Size));

                for (int y = position.y; y < position.y + item.Size.y; y++)
                {
                    for (int x = position.x; x < position.x + item.Size.x; x++)
                    {
                        _items[x, y] = item;
                    }
                }
            }
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    matrix[x, y] = _items[x, y];
                }
            }
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return _cells.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cells.Keys.GetEnumerator();
        }

        private bool _isInsideBounds(int positionX, int positionY)
        {
            return positionX >= 0 && positionY >= 0 &&
                   positionX < _width && positionY < _height;
        }

        private void _setItemMatrixValue(Vector2Int position, Vector2Int size, Item value)
        {
            for (int y = position.y; y < position.y + size.y; y++)
            {
                for (int x = position.x; x < position.x + size.x; x++)
                {
                    _items[x, y] = value;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;

using UnityEngine;


namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public event Action<List<InventoryCell>> OnInventoryUpdated;

        private List<InventoryCell> _cells = new List<InventoryCell>();
        public List<InventoryCell> Cells { get => _cells; }

        public void LoadItems(List<InventoryCell> cells)
        {
            _cells = cells;

            OnInventoryUpdated?.Invoke(_cells);
        }

        public void AddItem(int id, int count)
        {
            for (int i = 0; i < _cells.Count; i++)
                if (_cells[i].ID == id)
                {
                    _cells[i].Add(count);
                    OnInventoryUpdated?.Invoke(_cells);

                    return;
                }

            _cells.Add(new InventoryCell(id, count));
            OnInventoryUpdated?.Invoke(_cells);
        }

        public void RemoveItem(int cellID)
        {
            _cells.RemoveAt(cellID);

            OnInventoryUpdated?.Invoke(_cells);
        }

        private void OnDestroy()
        {
            OnInventoryUpdated = null;
        }

        [System.Serializable]
        public class InventoryCell
        {
            public int ID { get; private set; }
            public int Count { get; private set; }

            public InventoryCell(int id, int count)
            {
                ID = id;
                Count = count;
            }

            public void Add(int count)
            {
                Count += count;
            }
        }
    }
}
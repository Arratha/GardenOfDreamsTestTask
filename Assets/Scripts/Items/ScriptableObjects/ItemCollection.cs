using System.Collections.Generic;

using UnityEngine;


namespace Items
{
    [CreateAssetMenu(fileName = "ItemCollection", menuName = "ScriptableObjects/ItemSettings/ItemCollection", order = 1)]
    public class ItemCollection : ScriptableObject
    {
        [SerializeField] private ItemSettings[] _items;

        [ContextMenu(nameof(CheckIDs))]
        private void CheckIDs()
        {
            HashSet<int> ids = new HashSet<int>();
            List<int> duplicateIDs = new List<int>();

            for (int i = 0; i < _items.Length; i++)
                if (!ids.Contains(_items[i].Item.ID))
                    ids.Add(_items[i].Item.ID);
                else
                    duplicateIDs.Add(_items[i].Item.ID);

            duplicateIDs.Sort();

            for (int i = 0; i < duplicateIDs.Count; i++)
                Debug.LogError($"Duplicate item id: {duplicateIDs[i]}");
        }

        public Dictionary<int, BasicItem> CreateDictionary()
        {
            Dictionary<int, BasicItem> result = new Dictionary<int, BasicItem>();

            for (int i = 0; i < _items.Length; i++)
                result.Add(_items[i].Item.ID, _items[i].Item);

            return result;
        }
    }
}
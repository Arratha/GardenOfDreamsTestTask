using System.Collections.Generic;

using UnityEngine;


namespace Items
{
    public class ItemsController : MonoBehaviour
    {
        [SerializeField] private ItemCollection _collection;

        public Dictionary<int, BasicItem> Items;

        public static ItemsController Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            Items = _collection.CreateDictionary();
        }
    }
}
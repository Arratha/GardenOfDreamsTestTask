using System;
using System.Collections.Generic;

using UnityEngine;

using Items;
using Player;


namespace UI.Player.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public static event Action<int> DeleteCell;

        [SerializeField] private InventoryCard[] _cards;

        public void Initialize(PlayerInventory inventory)
        {
            inventory.OnInventoryUpdated += UpdateInventory;

            for (int i = 0; i < _cards.Length; i++)
                _cards[i].Initialize(i, SetDeleteButtonActive, (_) => DeleteCell?.Invoke(_));
        }

        private void SetDeleteButtonActive(int id)
        {
            for (int i = 0; i < _cards.Length; i++)
                _cards[i].SetDeleteButtonActive(i == id);
        }

        private void UpdateInventory(List<PlayerInventory.InventoryCell> cells)
        {
            for (int i = 0; i < _cards.Length; i++)
                if (i < cells.Count)
                    _cards[i].SetItem(ItemsController.Instance.Items[cells[i].ID].Icon, cells[i].Count);
                else
                    _cards[i].ClearCard();
        }
    }
}
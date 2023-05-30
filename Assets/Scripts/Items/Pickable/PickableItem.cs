using UnityEngine;

using Player;


namespace Items
{
    [RequireComponent(typeof(Collider2D))]
    public class PickableItem : MonoBehaviour, IPickable
    {
        [SerializeField] ItemSettings _itemReference;

        private bool _isPicked;

        public void Pick(PlayerController controller)
        {
            if (_isPicked)
                return;

            controller.InventoryScript.AddItem(_itemReference.Item.ID, 1);

            _isPicked = true;

            Destroy(gameObject);
        }
    }
}
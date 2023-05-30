using UnityEngine;


namespace Items
{
    [CreateAssetMenu(fileName = "ItemSettings", menuName = "ScriptableObjects/ItemSettings/BasicItem", order = 2)]
    public class ItemSettings : ScriptableObject
    {
        [SerializeField] private BasicItem _item;

        public BasicItem Item { get => _item; }
    }
}
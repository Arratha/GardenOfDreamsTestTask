using UnityEngine;


namespace Items
{
    [System.Serializable]
    public class BasicItem
    {
        [SerializeField] private int _id;

        [Space(10)]
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;

        #region Accessors
        public int ID { get => _id; }

        public Sprite Icon { get => _icon; }
        public GameObject Prefab { get => _prefab; }
        #endregion
    }
}
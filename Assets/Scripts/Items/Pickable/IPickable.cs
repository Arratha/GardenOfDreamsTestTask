using UnityEngine;

using Player;


namespace Items
{
    public interface IPickable
    {
        public void Pick(PlayerController controller);

        public GameObject gameObject { get; }
    }
}
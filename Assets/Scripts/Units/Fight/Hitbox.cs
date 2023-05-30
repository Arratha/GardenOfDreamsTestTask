using UnityEngine;


namespace Units.Fight
{
    public class Hitbox : MonoBehaviour
    {
        private UnitController _unitController;

        public void Initialize(UnitController unitController)
        {
            _unitController = unitController;
        }

        public UnitController Hit()
        {
            return _unitController;
        }
    }
}
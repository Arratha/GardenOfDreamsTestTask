using UnityEngine;


namespace Units.Move
{
    public interface IMovable
    {
        public void Move(Vector2 targetVector);
        public void Rotate(Vector2 targetVector);
        public void JumpBack(Vector2 threatPosition, float force);

        public GameObject gameObject { get; }
    }
}
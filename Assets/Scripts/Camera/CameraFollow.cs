using UnityEngine;


namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _objectToFollow;
        public Transform ObjectToFollow { set => _objectToFollow = value; }

        private void LateUpdate()
        {
            SetNewPosition();
        }

        private void SetNewPosition()
        {
            if (_objectToFollow == null)
                return;

            transform.position = new Vector3(_objectToFollow.position.x, _objectToFollow.position.y,
                transform.position.z);
        }
    }
}
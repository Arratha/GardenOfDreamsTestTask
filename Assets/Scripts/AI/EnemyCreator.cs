using UnityEngine;


namespace AI
{
    public class EnemyCreator : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private EnemyController _prefab;

        [Space(10)]
        [SerializeField] SpawnArea _area;

        private void Awake()
        {
            for (int i = 0; i < _count; i++)
                Instantiate(_prefab, _area.GetRandomPoint(), new Quaternion(0, 0, 0, 0));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_area.GetAreaCenter(), _area.GetAreaSize());
        }

        [System.Serializable]
        private class SpawnArea
        {
            [SerializeField] private Transform _pointA;
            [SerializeField] private Transform _pointB;

            public Vector2 GetAreaCenter()
            {
                Vector2 result = Vector2.zero;
                result.x = _pointA.transform.position.x - (_pointA.transform.position.x - _pointB.transform.position.x) / 2;
                result.y = _pointA.transform.position.y - (_pointA.transform.position.y - _pointB.transform.position.y) / 2;

                return result;
            }

            public Vector3 GetAreaSize()
            {
                Vector2 result = Vector2.zero;
                result.x = Mathf.Abs(_pointA.transform.position.x - _pointB.transform.position.x);
                result.y = Mathf.Abs(_pointA.transform.position.y - _pointB.transform.position.y);

                return result;
            }

            public Vector3 GetRandomPoint()
            {
                Vector3 result = GetAreaCenter();
                Vector3 size = GetAreaSize();

                result.x += Random.Range(-size.x / 2f, size.x / 2f);
                result.y += Random.Range(-size.y / 2f, size.y / 2f);

                return result;
            }
        }
    }
}
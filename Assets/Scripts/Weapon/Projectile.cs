using UnityEngine;

using Units.Fight;


namespace Weapon
{
    [System.Serializable]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        private string _tagToIgnore;
        private int _damage;

        private float _lifeTime = 4;

        private void FixedUpdate()
        {
            _lifeTime -= Time.fixedDeltaTime;

            if (_lifeTime <= 0)
                Destroy(gameObject);
        }

        public void Initialize(string tagToIgnore, Vector2 velocity, int damage)
        {
            _tagToIgnore = tagToIgnore;
            _damage = damage;

            GetComponent<Rigidbody2D>().velocity = velocity;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Hitbox hitbox)
                && !collision.CompareTag(_tagToIgnore))
            {
                hitbox.Hit().TakeDamage(_damage, transform.position, 50);

                Destroy(gameObject);
            }
        }
    }
}
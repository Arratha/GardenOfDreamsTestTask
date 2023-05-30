using UnityEngine;

using Main;

using Camera;
using UI.Units;
using UI.Player.Inventory;

using Weapon;


namespace Player
{
    public class PlayerCreator : MonoBehaviour
    {
        #region Player
        [SerializeField] private PlayerController _playerController;

        [Space(5)]
        [SerializeField] private RangeWeaponSettings _baseWeapon;
        #endregion

        [Space(10)]
        [SerializeField] private Transform _spawnPoint;

        [Space(10)]
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private HealthbarController _playerHealthbar;
        [SerializeField] private InventoryController _playerInventory;

        private PlayerController _player;

        private void Awake()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            _player = Instantiate(_playerController, _spawnPoint.position, new Quaternion(0, 0, 0, 0));

            InitializeUI(_player);

            _player.OnPlayerDie += PlayerDie;

            _player.AttackScript.AddWeapon(_baseWeapon.GetWeapon());
            _player.InventoryScript.LoadItems(SaveController.Load());
        }

        private void InitializeUI(PlayerController player)
        {
            _cameraFollow.ObjectToFollow = player.transform;

            _playerHealthbar.TryInitialize(player.HealthScript);
            _playerInventory.Initialize(player.InventoryScript);
        }

        private void PlayerDie()
        {
            CreatePlayer();
        }

        private void OnDestroy()
        {
            if (_player != null)
                _player.OnPlayerDie -= PlayerDie;
        }

        private void OnApplicationQuit()
        {
            if (_player != null)
                SaveController.Save(_player.InventoryScript.Cells);
        }
    }
}
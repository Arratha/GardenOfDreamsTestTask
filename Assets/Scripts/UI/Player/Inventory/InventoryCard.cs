using UnityEngine;
using UnityEngine.UI;

using TMPro;


namespace UI.Player.Inventory
{
    public delegate void ButtonClickCallback(int id);

    [RequireComponent(typeof(Button))]
    public class InventoryCard : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _counter;

        [SerializeField] private Button _deleteButton;

        private Button _cardButton;

        private Vector2 _cardSize;

        private int _cardID;
        private bool _hasItem;

        public void Initialize(int id, ButtonClickCallback cardCallback, ButtonClickCallback deleteCallback)
        {
            _cardID = id;

            ClearCard();

            _deleteButton.onClick.AddListener(() =>
            {
                deleteCallback.Invoke(_cardID);
                cardCallback.Invoke(-1);
            });
            _deleteButton.gameObject.SetActive(false);

            _cardButton = GetComponent<Button>();
            _cardButton.onClick.AddListener(() =>
            {
                if (_hasItem)
                    cardCallback.Invoke(_cardID);
            });

            _cardSize = GetComponent<RectTransform>().rect.size * 0.8f;
        }

        public void SetItem(Sprite sprite, int count)
        {
            _hasItem = true;

            SetIcon(sprite);
            _counter.text = count.ToString();
            _counter.gameObject.SetActive(count > 1);
        }

        private void SetIcon(Sprite sprite)
        {
            Vector2 iconSize = sprite.bounds.size;
            Vector2 tempSize;

            if (iconSize.x / _cardSize.x > iconSize.y / _cardSize.y)
                tempSize = new Vector2(_cardSize.x, _cardSize.x * iconSize.y / iconSize.x);
            else
                tempSize = new Vector2(_cardSize.y * iconSize.x / iconSize.y, _cardSize.y);

            _icon.GetComponent<RectTransform>().sizeDelta = tempSize;
            _icon.sprite = sprite;
        }

        public void ClearCard()
        {
            _hasItem = false;

            _icon.sprite = null;
            _counter.text = null;
            _counter.gameObject.SetActive(false);
        }

        public void SetDeleteButtonActive(bool isActive)
        {
            _deleteButton.gameObject.SetActive(isActive);
        }
    }
}
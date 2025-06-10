using Inventory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemButton : MonoBehaviour
    {
        public Item CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                _buttonText.text = _currentItem.Name;
            }
        }

        public event Action OnClick;

        private TextMeshProUGUI _buttonText;
        private Button _button;
        private Item _currentItem;

        void Awake()
        {
            _button = GetComponent<Button>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
            _button.onClick.AddListener(() => OnClick?.Invoke());
        }
    }
}
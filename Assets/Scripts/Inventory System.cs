using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using JetBrains.Annotations;

namespace Inventory
{


    public class InventorySystem : MonoBehaviour
    {

        #region Propperties

        #endregion

        #region Fields
        //TODO: Refactor: move this to UIController
        [Header("UI Reffs")]
        [SerializeField] private ItemButton _prefabButton;
        [SerializeField] private Transform _inventoryPanel;
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _sellButton;

        [Header("Object Definition")]
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private Food[] _food;
        [SerializeField] private Other[] _other;
        [Header("Item Pool")]
        [SerializeField] private List<Item> _items = new List<Item>();
        [Header("Item Selected")]
        [SerializeField] private Item _currentItemSelected;
        private int i;

        #endregion

        #region Unity Callbacks
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            InitializeItems();
            InitializeUI();


            //TODO: Refactor


            _useButton.onClick.AddListener(UseCurrentItem);
            _sellButton.onClick.AddListener(SellCurrentItem);

        }

        







        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region Public Methods
        public void AddItem(ItemButton buttonItemToAdd)
        {
            ItemButton newItem = Instantiate(buttonItemToAdd, _inventoryPanel);
            newItem.CurrentItem = buttonItemToAdd.CurrentItem;
            newItem.OnClick += () => SelectItem(newItem.CurrentItem);
        }

        public void SelectItem(Item currentItem)
        {
            _currentItemSelected = currentItem;

            _sellButton.gameObject.SetActive(currentItem is ISellable);
            _useButton.gameObject.SetActive(currentItem is IUsable);
        }

        #endregion

        #region Private Methods
        private void InitializeUI()
        {
            if (_prefabButton == null || _inventoryPanel == null)
            {
                Debug.LogError("Faltan referencias en el Inspector (_prefabButton o _inventoryPanel).");
                return;
            }

            for (int i = 0; i < _items.Count; i++)
            {
                ItemButton newButtonItem = Instantiate(_prefabButton, _inventoryPanel);
                newButtonItem.CurrentItem = _items[i];
                newButtonItem.OnClick += () => AddItem(newButtonItem);
                newButtonItem.gameObject.transform.SetParent(padre, true);
                Debug.Log($"Inicializando UI con {_items.Count} ítems.");
                Debug.Log($"Creando botón para: {_items[i].Name}");
            }

            _prefabButton.gameObject.SetActive(false);
        }
        public Transform padre;
        // private void SelectItem(Button button)
        //{
        //    var itemButton = button.GetComponent<ItemButton>();
        //    if (itemButton != null)
        //    {
        //        SelectItem(itemButton.CurrentItem);
        //   }
        //}

        private void InitializeItems()
        {
            // Weapons
            for (int i = 0; i < _weapons.Length; i++)
                _items.Add(_weapons[i]);

            // Food
            for (int i = 0; i < _food.Length; i++)
                _items.Add(_food[i]);
        }


        //Refactor
        private void UseCurrentItem()
        {
            if (_currentItemSelected == null)
            {
                Debug.LogWarning("No hay ningún ítem seleccionado para usar.");
                return;
            }

            if (!(_currentItemSelected is IUsable usableItem))
            {
                Debug.LogWarning($"El ítem {_currentItemSelected.Name} no es usable.");
                return;
            }

            usableItem.Use();
            (_currentItemSelected as IUsable).Use();
            if (_currentItemSelected is IConsumable)
                Consume(_currentItemSelected);
            {
                //Destroy(_currentItemSelected.gameObject);
                if (_currentItemSelected.gameObject != null)
                {
                    Destroy(_currentItemSelected.gameObject);
                }
                _currentItemSelected = null;
            }
            Debug.Log("Item Usado..");
        }

        private void Consume(Item currentItemSelected)
        {
            Destroy(currentItemSelected.gameObject);
            _currentItemSelected = null;
            _sellButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }

        private void SellCurrentItem()
        {
            if (_currentItemSelected == null)
            {
                Debug.LogWarning("No hay ningún ítem seleccionado para vender.");
                return;
            }

            if (!(_currentItemSelected is ISellable sellableItem))
            {
                Debug.LogWarning($"El ítem {_currentItemSelected.Name} no es vendible.");
                return;
            }

            sellableItem.Sell();

            // Si quieres destruir el objeto del mundo, asegúrate que sea un GameObject válido
            if (_currentItemSelected is IConsumable)
            {
                Destroy(_currentItemSelected.gameObject);
            }

            _currentItemSelected = null;
        }

        #endregion
    }
}
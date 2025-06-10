using System;
using UnityEngine;

namespace Inventory
{


    [Serializable]
    public class Other : Item, IShellable
    {
        [field: SerializeField] public float Price { get; set; }

        public float Sell()
        {
            Debug.Log("Has ganado" + Price + "dineritos!");
            return Price;
        }

    }
}

using UnityEngine;
using System;
using UnityEngine.UI;

namespace Inventory
{


    [Serializable]
    public class Item
    {
        internal UnityEngine.Object gameObject;

        #region Propperties
        [field: SerializeField] public string Name { get; set; }

        public static implicit operator Button(Item v)
        {
            return null;
        }

        public static implicit operator Item(Button v)
        {
            return null;
        }
        #endregion

    }
}

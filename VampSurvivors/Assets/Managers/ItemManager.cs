using System;
using UnityEngine;

namespace Managers
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        [Serializable]
        public class ItemData
        {
            public int id;
            public string name;
            public float health, damage, speed, magic;
        }

        [Serializable]
        public class ItemList
        {
            public ItemData[] items;
        }

        public ItemList itemList = new ItemList();

        private void Start()
        {
            itemList = JsonUtility.FromJson<ItemList>(Resources.Load<TextAsset>("Items/ItemData").text);
        }

        public ItemData GetItemData(int id)
        {
            return itemList.items[id];
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Collectible> currentItems = new();
    private Collision coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.onGround)
        {
            foreach (Collectible item in currentItems.ToList())
            {
                if (item.type == CollectibleTypes.Battery && item.isFollowing)
                {
                    item.RemoveItem();
                    currentItems.Remove(currentItems.Find(i => i == item));
                    Debug.Log("Collect");
                }
            };
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Collectible item = collision.gameObject.GetComponent<Collectible>();
            if (!item.isFollowing)
            {
                currentItems.Add(item);
                item.StartFollowing(gameObject, currentItems.Count());
            }
        }
    }

    public void ReturnItems()
    {
        foreach (Collectible item in currentItems.ToList())
        {
            if (item.type != CollectibleTypes.Green_Key)
            {
                item.ReturnItem();
                currentItems.Remove(currentItems.Find(i => i == item));
            }
        }
    }

    public bool UseItem(CollectibleTypes type)
    {
        Collectible item = currentItems.Find(i => i.type == type);
        if (item != null)
        {
            currentItems.Remove(item);
            item.RemoveItem();
            return true;
        }
        return false;
    }

}

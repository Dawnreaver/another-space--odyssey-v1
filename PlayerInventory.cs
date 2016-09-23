using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
	public List<InventoryItem> m_inventoryItemList;

	void Start()
	{
		m_inventoryItemList = new List<InventoryItem>();
	}

	public void AddResourceToInventory( ResourceBehaviour.ResourceTypes resourceType, float resourceAmount)
	{
        InventoryItem item = m_inventoryItemList.Find(x => x.m_resourceType == resourceType);
        if(item == null)
        {
            CreateNewInventoryResourceItem(resourceType, resourceAmount);
        }
        else if( item != null)
        {
            item.m_resourceAmount += Mathf.RoundToInt(resourceAmount);
        }
    }

    #region Saving and loading the iventory
    /*	void InitializeInventory()
        {
            // load items from save ?
        }

        void SavePlayerInventory()
        {
            // save player inventory ? 
        }
        */
    #endregion

    void CreateNewInventoryResourceItem(ResourceBehaviour.ResourceTypes resourceType, float resourceAmount)
    {
        InventoryItem newItem = new InventoryItem();
        newItem.m_inventoryItemName = SetItemName(resourceType);
        newItem.m_inventoryItemType = InventoryItem.InventoryItemTypes.Resource;
        newItem.m_resourceType = resourceType;
        newItem.m_resourceAmount = (int)Mathf.Round(resourceAmount);
        m_inventoryItemList.Add(newItem);
    }

    string SetItemName(ResourceBehaviour.ResourceTypes resourceType)
    {
        string itemName = "";
        switch (resourceType)
        {
            case ResourceBehaviour.ResourceTypes.AsteroidMetal:
                itemName ="Astereroid Metal";
            break;
            case ResourceBehaviour.ResourceTypes.Crystal:
                itemName = "Crystal";
                break;
            case ResourceBehaviour.ResourceTypes.Ice:
                itemName = "Water";
                break;
            case ResourceBehaviour.ResourceTypes.Rock:
                itemName = "Rock";
                break;
            case ResourceBehaviour.ResourceTypes.ScrapMetal:
                itemName = "Scrap Metal";
                break;
        }
        return itemName;
    } 
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
	public List<InventoryItem> m_inventoryItemList;

	void Start()
	{
		m_inventoryItemList = new List<InventoryItem>();
		InitializeInventory();
	}

	public void AddResourceToInventory( ResourceBehaviour.ResourceTypes resourceType, float resourceAmount)
	{
		if(m_inventoryItemList.Count() == 0 )
		{
			InventoryItem newItem = new InventoryItem();
            newItem.m_inventoryItemName = SetitemName(resourceType);
			newItem.m_inventoryItemType = InventoryItem.InventoryItemTypes.Resource;
			newItem.m_resourceType = resourceType;
			newItem.m_resourceAmount = (int)Mathf.Round(resourceAmount);
			m_inventoryItemList.Add(newItem);
		}
		else if(m_inventoryItemList.Count() >= 1)
		{
			for(int a = 0; a < m_inventoryItemList.Count(); a++)
			{
				if(m_inventoryItemList[a].m_resourceType == resourceType && m_inventoryItemList[a].m_inventoryItemType == InventoryItem.InventoryItemTypes.Resource)
				{
					// destiguish between liquid and solid items
					m_inventoryItemList[a].m_resourceAmount += (int)Mathf.Round(resourceAmount);
				}
			}
		}
	}

	void InitializeInventory()
	{
		// load items from save ?
	}

	void SavePlayerInventory()
	{
		// save player inventory ? 
	}

    string SetitemName(ResourceBehaviour.ResourceTypes resourceType)
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
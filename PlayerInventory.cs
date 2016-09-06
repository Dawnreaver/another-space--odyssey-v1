using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
	public List<InventoryItem>() m_inventoryItemList;

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
			// try name the item by resource type
			newItem.m_inventoryItemName = string(resourceType);
			newItem.m_inventoryItemType = InventoryItem.inventoryItemTypes.Reource;
			newItem.m_resourceType = resourceType;
			newItem.m_resourceAmount = Mathf.Round(resourceAmount);
			m_inventoryItemList.Add(newItem);
		}
		else if(m_inventoryItemList.Count() >= 1)
		{
			for(int a = 0; a < m_inventoryItemList.Count(); a++)
			{
				if(m_inventoryItemList[a].m_resourceType == resourceType && m_inventoryItemList[a].m_inventoryItemType == InventoryItem.inventoryItemTypes.Reource)
				{
					// destiguish between liquid and solid items
					m_inventoryItemList[a].m_resourceAmount += Mathf.Round(resourceAmount);
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
}
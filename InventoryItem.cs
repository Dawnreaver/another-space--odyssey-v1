using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryItem
{
	public enum InventoryItemTypes{ Resource, OtherItem}
	public InventoryItemTypes m_inventoryItemType;
   	string m_inventoryItemName;
	ResourceBehaviour.ResourceTypes m_resourceType;
   	int m_resourceAmount;
}
public class InventoryItem
{
	public enum InventoryItemTypes{ Resource, OtherItem}
	public InventoryItemTypes m_inventoryItemType;
   	public string m_inventoryItemName;
	public ResourceBehaviour.ResourceTypes m_resourceType;
   	public int m_resourceAmount;
}
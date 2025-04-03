using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    
    public Item item; // Текущий предмет в инвентаре
    public GameObject inventoryUI; // Ссылка на UI инвентаря
    public InventorySlot slot; // Ссылка на слот
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    
    public void AddItem(Item newItem)
    {
        item = newItem;
        slot.AddItem(newItem);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
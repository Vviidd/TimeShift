using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // Ссылка на данные предмета
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
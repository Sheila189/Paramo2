using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string itemName; // Nombre único del objeto

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.collectedItems.Add(itemName); // Añadir al inventario
            FindObjectOfType<InventoryUI>().UpdateInventoryUI(); // Actualizar la UI
            Destroy(gameObject); // Eliminar el objeto del mundo
        }
    }
}

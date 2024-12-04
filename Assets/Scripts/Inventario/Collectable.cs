using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string itemName; // Nombre �nico del objeto

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.collectedItems.Add(itemName); // A�adir al inventario
            FindObjectOfType<InventoryUI>().UpdateInventoryUI(); // Actualizar la UI
            Destroy(gameObject); // Eliminar el objeto del mundo
        }
    }
}

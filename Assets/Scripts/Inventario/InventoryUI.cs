using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<Image> inventoryImages; // Lista de imágenes de inventario (Mano, Daga, Bate, Tabla)
    public int equippedIndex = 0;       // Índice del objeto equipado (0 = Mano)

    private void Start()
    {
        UpdateInventoryUI(); // Asegúrate de mostrar el estado inicial del inventario
    }

    private void Update()
    {
        // Control del jugador para cambiar entre Mano y Armas
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipItem(0); // Mano
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipItem(1); // Daga
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipItem(2); // Bate
        if (Input.GetKeyDown(KeyCode.Alpha4)) EquipItem(3); // Tabla
    }

    public void EquipItem(int index)
    {
        equippedIndex = index; // Cambia el índice del objeto equipado
        UpdateInventoryUI();   // Actualiza la interfaz visual
    }

    public void UpdateInventoryUI()
    {
        // Activa solo la imagen correspondiente al objeto equipado
        for (int i = 0; i < inventoryImages.Count; i++)
        {
            inventoryImages[i].enabled = (i == equippedIndex); // Activa la imagen si corresponde al índice actual
        }
    }
}

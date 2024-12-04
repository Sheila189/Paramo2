using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; } // Singleton

    public List<string> collectedItems = new List<string>(); // Lista de nombres de los objetos recogidos
    public int equippedIndex = 0; // Índice del objeto equipado (0 = mano)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
        }
    }

    private void Start()
    {
        // Cargar inventario al iniciar el juego
        LoadInventory();
    }

    private void OnApplicationQuit()
    {
        // Guardar inventario al salir del juego
        SaveInventory();
    }

    // Guardar datos del inventario
    public void SaveInventory()
    {
        // Guardar lista de objetos como una cadena separada por comas
        string inventoryData = string.Join(",", collectedItems);
        PlayerPrefs.SetString("Inventory", inventoryData);
        PlayerPrefs.SetInt("EquippedIndex", equippedIndex);
        PlayerPrefs.Save();
    }

    // Cargar datos del inventario
    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            string inventoryData = PlayerPrefs.GetString("Inventory");
            collectedItems = new List<string>(inventoryData.Split(','));
            equippedIndex = PlayerPrefs.GetInt("EquippedIndex");
        }
    }

    // Reiniciar el inventario
    public void ResetInventory()
    {
        collectedItems.Clear();
        equippedIndex = 0;
        PlayerPrefs.DeleteKey("Inventory");
        PlayerPrefs.DeleteKey("EquippedIndex");
    }
}

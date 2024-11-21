using GameCreator.Runtime.Common;
using GameCreator.Runtime.Inventory;
using GameCreator.Runtime.Inventory.UnityUI;
using GameCreator.Runtime.VisualScripting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[Title("Toggle Inventory")]
[Description("Toggle the Inventory UI")]
[Category("Inventory/UI/Toggle Bag UI")]
[Keywords("Item", "Inventory", "Bag")]
[Version(1, 0, 0)]
[Dependency("inventory", 2, 2, 3)]
[Image(typeof(IconBagSolid), ColorTheme.Type.Purple)]
// Toggles the inventory Open/Close
// Close UI couldnt have been possible without Sphaera Studios' Close UI code
public class ToggleInventory : Instruction
{
    [SerializeField] private PropertyGetGameObject m_Bag = new PropertyGetGameObject(); // Cambiado
    private BagListUI[] m_bagUI;
    private bool inventoryOpen = false;

    protected override Task Run(Args args)
    {
        var bag = this.m_Bag.Get<Bag>(args);
        if (bag == null) return Task.CompletedTask; // Corregido: Devolvemos una tarea completada en lugar de null

        if (inventoryOpen)
        {
            m_bagUI = GameObject.FindObjectsOfType<BagListUI>();
            if (m_bagUI == null) return Task.CompletedTask; // Corregido: Devolvemos una tarea completada en lugar de null
            foreach (var bagUI in m_bagUI)
            {
                if (bagUI.Bag.Wearer == bag.Wearer)
                {
                    bagUI.gameObject.SetActive(false);
                    break;
                }
            }
            inventoryOpen = false;
        }
        else
        {
            bag.OpenUI();
            inventoryOpen = true;
        }

        return Task.CompletedTask; // Corregido: Devolvemos una tarea completada en lugar de null
    }
}

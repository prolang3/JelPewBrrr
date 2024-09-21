using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public HealthComponent healthComponent;
    public EquipmentComponent equipmentComponent;

    private Dictionary<Stat, float> StatBonuses = new();

    // Start is called before the first frame update
    void Start()
    {
        if (!healthComponent)
        {
            healthComponent = gameObject.GetComponent<HealthComponent>();
        }
        if (!equipmentComponent)
        {
            equipmentComponent = gameObject.gameObject.GetComponent<EquipmentComponent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewItem(Item item)
    {
        equipmentComponent.Items.Add(item);
        StatBonuses = equipmentComponent.GetStats();
        ApplyStats();
    }

    void ApplyStats()
    {
        Debug.Log("apply stat");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public HealthComponent healthComponent;
    public EquipmentComponent equipmentComponent;

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

        healthComponent.MaxHealth += equipmentComponent.MaxHealthIncrease;
        healthComponent.Health += equipmentComponent.MaxHealthIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

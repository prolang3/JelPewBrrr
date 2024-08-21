using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class RewardOnKillComponent : MonoBehaviour
{
    HealthComponent healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = gameObject.GetComponent<HealthComponent>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (healthComponent.Health <= 0)
        {
            print("dead");
        }
    }
}

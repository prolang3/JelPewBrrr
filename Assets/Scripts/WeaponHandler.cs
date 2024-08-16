using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private WeaponData Weapon;
    private float cooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z

        if (Input.GetMouseButton(0) && cooldown == 0)
        {
            cooldown = Weapon.UseDelay;
            Weapon.Fire(gameObject.transform.position, mouseWorldPos);
        }

        if (cooldown > 0)
        {
            if (Time.deltaTime > cooldown)
            {
                cooldown = 0f;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}

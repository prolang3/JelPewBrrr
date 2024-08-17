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
        Weapon.Init(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z

        Weapon.UpdateLocalPosition(gameObject.transform.position, mouseWorldPos);

        if (Input.GetMouseButton(0) && cooldown == 0)
        {
            cooldown = Weapon.UseDelay;
            Weapon.Fire(mouseWorldPos);
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

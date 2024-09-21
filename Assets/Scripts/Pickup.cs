using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Item item;

    private SpriteRenderer spriteRenderer;

    public Item Item { get => item; set => item = value; }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Unit playerUnit = collision.gameObject.GetComponent<Unit>();
            //playerUnit.AddNewItem();
        }
    }
}

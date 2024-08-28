using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> NPCs = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject != null) && collision.gameObject.CompareTag("Player"))
        {
            Activate();
        }
    }

    void Activate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy enemy = transform.GetChild(i).GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.CurrentState = "Active";
            }
        }

        /*
        for (int i = 0; i < NPCs.Count; i++)
        {
            if (NPCs[i] != null)
            {
                Enemy enemy = NPCs[i].GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.CurrentState = "Active";
                }
            }
        }
        */
    }
}

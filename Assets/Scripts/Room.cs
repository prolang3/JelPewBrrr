using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Room : MonoBehaviour
{
    public bool hasWave = false;
    public bool isActivated = false;
    public List<Vector2> SpawnLocation = new List<Vector2>();
    public List<GameObject> SpawnedEnemys = new List<GameObject>();
    public WaveData WaveData;

    private int currentWave = 0;
    private int maxWave;

    private void Start()
    {
        if (hasWave && WaveData)
        {
            maxWave = WaveData.WaveDatas.Count;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject != null) && collision.gameObject.CompareTag("Player") && !isActivated)
        {
            Activate();
        }
    }

    bool checkNpcs()
    {
        for (int i = 0; i < SpawnedEnemys.Count; i++)
        {
            if (SpawnedEnemys[i] == null)
            {
                SpawnedEnemys.RemoveAt(i);
            }
        }
        return SpawnedEnemys.Count == 0;
    }

    void Update()
    {
        if (!hasWave || isActivated == false)
        {
            return;
        }

        if (checkNpcs() == true)
        {
            SpawnedEnemys = WaveData.SpawnWave(currentWave++, transform, SpawnLocation);
        }

    }

    void Activate()
    {
        isActivated = true;
        if (hasWave)
        {
        }
        else
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                Enemy enemy = transform.GetChild(i).GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.CurrentState = "Active";
                    SpawnedEnemys.Add(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData
{
    public GameObject Npc;
    public int Amount = 1;
}

public class Room : MonoBehaviour
{
    public bool hasWave = false;
    public bool isActivated = false;
    public List<Vector2> SpawnLocation = new List<Vector2>();
    public List<GameObject> SpawnedEnemys = new List<GameObject>();
    public List<List<SpawnData>> Waves = new List<List<SpawnData>>();

    private int currentWave = 0;
    private int maxWave;

    private void Start()
    {
        if (hasWave)
        {
            maxWave = Waves.Count;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject != null) && collision.gameObject.CompareTag("Player") && !isActivated)
        {
            Activate();
        }
    }

    void Update()
    {
        if (hasWave && isActivated && currentWave == 0)
        {
            for (int i = 0; i < Waves.Count; i++)
            {
                for (int j = 0; j < Waves[i].Count; j++)
                {
                    GameObject newNpc = Instantiate(Waves[i][j].Npc);
                    Enemy enemy = newNpc.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.CurrentState = "Active";
                    }
                }

                while (SpawnedEnemys.Count > 0)
                {
                    for (int j = 0; j < SpawnedEnemys.Count; j++)
                    {
                        if (SpawnedEnemys[j] == null)
                        {
                            SpawnedEnemys.Remove(SpawnedEnemys[j]);
                        }
                    }
                }
            }
        }
    }

    void Activate()
    {
        if (hasWave)
        {
            for (int i = 0; i < Waves.Count; i++)
            {
                for (int j = 0; j < Waves[i].Count; j++)
                {
                    GameObject newNpc = Instantiate(Waves[i][j].Npc);
                    Enemy enemy = newNpc.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.CurrentState = "Active";
                    }
                }

                while (SpawnedEnemys.Count > 0)
                {
                    for (int j = 0; j < SpawnedEnemys.Count; j++)
                    {
                        if (SpawnedEnemys[j] == null)
                        {
                            SpawnedEnemys.Remove(SpawnedEnemys[j]);
                        }
                    }
                }
            }
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

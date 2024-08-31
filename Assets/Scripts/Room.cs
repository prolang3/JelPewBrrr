using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Room : MonoBehaviour
{
    public bool hasWave = false;
    public bool isActivated = false;
    public List<Vector2> SpawnLocation = new List<Vector2>();
    public List<GameObject> SpawnedEnemys = new List<GameObject>();
    public WaveData waveData;

    private int currentWave = 0;
    private int maxWave;

    private void Start()
    {
        if (hasWave && waveData)
        {
            maxWave = waveData.WaveDatas.Count;
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
        if (isActivated == true && hasWave)
        {
            if (checkNpcs() == true && waveData != null && currentWave < maxWave)
            {
                SpawnWave();
                currentWave++;
            }
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < waveData.WaveDatas[currentWave].SpawnDatas.Count; i++)
        {
            Debug.Log(waveData.WaveDatas[currentWave].SpawnDatas[i].Amount);
            for (int j = 0; j < waveData.WaveDatas[currentWave].SpawnDatas[i].Amount; j++)
            {
                GameObject newNpc = Instantiate(waveData.WaveDatas[currentWave].SpawnDatas[i].Npc, transform);
                Vector2 spawnPosition = SpawnLocation[Random.Range(0, SpawnLocation.Count)];
                newNpc.transform.localPosition = spawnPosition;
                Enemy enemy = newNpc.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.CurrentState = "Active";
                }
                SpawnedEnemys.Add(newNpc);
            }
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

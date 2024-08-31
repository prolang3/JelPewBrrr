using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnData
{
    public GameObject Npc;
    public int Amount;
}

[System.Serializable]
public struct Wave
{
    public List<SpawnData> SpawnDatas;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    //public int Waves = 2;
    public List<Wave> WaveDatas = new List<Wave>();
    public List<GameObject> SpawnWave(int wave, Transform Room, List<Vector2> SpawnPositions)
    {
        List<GameObject> spawnedNpcs = new();
        for (int i = 0; i < WaveDatas[wave].SpawnDatas.Count; i++)
        {
            for (int j = 0; j < WaveDatas[wave].SpawnDatas[i].Amount; j++)
            {
                GameObject newNpc = Instantiate(WaveDatas[wave].SpawnDatas[i].Npc, Room);
                Vector2 spawnPosition = SpawnPositions[Random.Range(0, SpawnPositions.Count)];
                newNpc.transform.localPosition = spawnPosition;
                Enemy enemy = newNpc.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.CurrentState = "Active";
                }
                spawnedNpcs.Add(newNpc);
            }
        }

        return spawnedNpcs;
    }
}

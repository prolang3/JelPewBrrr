using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public GameObject Npc;
    public int Amount;
}

[System.Serializable]
public class Wave
{
    public List<SpawnData> SpawnDatas;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    public int Waves = 2;
    public List<Wave> WaveDatas;

}

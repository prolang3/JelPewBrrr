using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileInfo : MonoBehaviour
{
    public GameObject Shooter;
    public float Damage;
    public float Speed;
    public bool usesRigidBody2D;
    public bool doesMove;
}
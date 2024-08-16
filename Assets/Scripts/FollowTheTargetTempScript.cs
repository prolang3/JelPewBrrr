using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheTargetTempScript : MonoBehaviour
{
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Target.transform.position + new Vector3(0,0,-10);
    }
}

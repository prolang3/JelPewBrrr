using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject Target;
    public float ShakeDuration = 0.1f;
    public float ShakeIntensity = 0.1f;

    private Vector3 shakeOffset;

    // Start is called before the first frame update
    void Start()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = shakeOffset + Target.transform.position + new Vector3(0, 0, -10);
    }

    public void Shake()//float? duration, float? intensity
    {
        print("We make the ground shake.");
        StartCoroutine(shake(ShakeDuration, ShakeIntensity)); //duration ?? intensity ?? 
    }

    private IEnumerator shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            shakeOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * magnitude;
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = transform.position;
    }
}

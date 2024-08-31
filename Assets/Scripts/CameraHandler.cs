using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject Target;
    public float ShakeDuration = 0.1f;
    public float ShakeIntensity = 0.1f;

    private Vector3 shakeOffset;
    private Vector3 screenCenter;

    void Awake()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

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

        Vector3 mouseOffset = new Vector3((Input.mousePosition.x - screenCenter.x) / (Screen.width / 10), (Input.mousePosition.y - screenCenter.y) / (Screen.height / 10));

        gameObject.transform.position = mouseOffset + shakeOffset + Target.transform.position + new Vector3(0, 0, -10);
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

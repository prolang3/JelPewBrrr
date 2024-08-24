using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICheckTestScript : MonoBehaviour
{
    public HealthComponent Target;
    public Image Image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Image.fillAmount = Target.Health / Target.MaxHealth;
    }
}

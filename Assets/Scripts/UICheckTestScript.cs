using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICheckTestScript : MonoBehaviour
{
    public HealthComponent Target;
    public Image Image;
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        if (!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Image.fillAmount = Target.Health / Target.MaxHealth;
        Text.text = Target.Health + " / " + Target.MaxHealth;
    }
}

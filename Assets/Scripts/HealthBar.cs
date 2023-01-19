using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthNum;
    public int HealthCurrent;
    private int HealthMax;
    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        HealthMax = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health;
        healthBar = GetComponent<Image>();
        HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            HealthCurrent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health;
        }
        
        healthBar.fillAmount = (float) HealthCurrent / (float)HealthMax;
        healthNum.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
    }
}

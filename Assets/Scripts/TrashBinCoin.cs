using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrashBinCoin : MonoBehaviour
{
    public Text coinText;
    public static int coinCurrent;
    public static int coinMax;
    private Image transhBinBar;
    // Start is called before the first frame update
    void Start()
    {
        transhBinBar = GetComponent<Image>();
        coinCurrent = 0;
        coinMax = 99;
    }

    // Update is called once per frame
    void Update()
    {
        transhBinBar.fillAmount = (float)coinCurrent / (float)coinMax;
        coinText.text = coinCurrent.ToString() + "/" + coinMax.ToString();
    }
}

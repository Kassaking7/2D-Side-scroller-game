using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject sickle;
    public static bool oneExist;
    // Start is called before the first frame update
    void Start()
    {
        oneExist = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !oneExist)
        {
            Instantiate(sickle, transform.position, transform.rotation);
        }
    }

}

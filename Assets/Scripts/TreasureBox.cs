using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject treasure;
    public float delayTime;
    private bool canOpen;
    private bool isOpen;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canOpen && !isOpen)
            {
                anim.SetTrigger("Opening");
                isOpen = true;
                Invoke("GetCoin",delayTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }

    void GetCoin()
    {
        //Instantiate(treasure, transform.position, Quaternion.identity);
        GameObject trea = Instantiate(treasure, transform.position, Quaternion.identity);
         trea.GetComponent<Rigidbody2D>().AddForce(transform.up * 150);

    }
}

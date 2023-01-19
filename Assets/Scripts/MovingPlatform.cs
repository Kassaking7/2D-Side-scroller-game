using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movingSpeed;
    public float waitTime;
    public Transform[] movePos;
    private float waitedTime;
    private int i;
    private Transform playerDefTransform;
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        waitedTime = waitTime;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, movingSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) <= 0.1f) {
            if (waitedTime <= 0.0f)
            {
                i = i == 1 ? 0 : 1;
                waitedTime = waitTime;
            } else
            {
                waitedTime -= Time.deltaTime;
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
}

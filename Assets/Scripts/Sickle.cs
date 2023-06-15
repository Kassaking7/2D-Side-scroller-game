using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public float speed;
    public int damage;
    public float rotateSpeed;
    public float tuning;
    private Rigidbody2D rb2d;
    private Transform playerTransform;
    private Transform sickleTransform;
    private Vector2 startSpeed;
    private CameraShake camShake;
    private bool positive;
    private bool curpositive;


    // Start is called before the first frame update
    void Start()
    {
        SickleHit.oneExist = true;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        startSpeed = rb2d.velocity;
        positive = (startSpeed.x > 0);
        curpositive = positive;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransform = GetComponent<Transform>();
        camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        if (curpositive != positive)
        {
            float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);
            transform.position = new Vector3(transform.position.x, y, 0);
        }
        rb2d.velocity -= startSpeed * Time.deltaTime;
        curpositive = (rb2d.velocity.x > 0);
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f)
        {
            SickleHit.oneExist = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }  
    }
}

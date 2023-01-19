using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public int Damage;
    public float attackTime;
    public float startTime;
    public float time;
    private float attackTimeCount;
    private PolygonCollider2D coll2D;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
        attackTimeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimeCount <= 0)
        {
            Attack();
        } else
        {
            attackTimeCount -= Time.deltaTime;
        }

        
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(StartAttack());
            attackTimeCount = attackTime;
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        coll2D.enabled = true;
        StartCoroutine((disableHitBox()));
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        coll2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }

}

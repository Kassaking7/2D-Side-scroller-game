using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Blink;
    public float seconds;
    public float dieTime;
    public float hitBoxTime;
    private Renderer myRender;
    private Animator anim;
    private ScreenFlash sf;
    private PolygonCollider2D polygon2D;
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        polygon2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        sf.FlashScreen();
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);
        } else
        {
            BlinkPlayer(Blink, seconds);
        }
        polygon2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }
    IEnumerator ShowPlayerHitBox() {
        yield return new WaitForSeconds(hitBoxTime);
        polygon2D.enabled = true;
    }
    void KillPlayer()
    {
        Destroy(gameObject);
    }
    void BlinkPlayer(int numBlink, float seconds)
    {
        StartCoroutine(DoBlink(numBlink, seconds));
    }

    IEnumerator DoBlink(int numBlink, float seconds)
    {
        for (int i = 0; i < 2 * numBlink; ++i)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}

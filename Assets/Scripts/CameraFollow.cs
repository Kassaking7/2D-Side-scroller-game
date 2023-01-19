using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothFactor;
    public Vector2 minPos;
    public Vector2 maxPos;

    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothFactor);
            }
        }
    }

    public void SetCamPosLimit(Vector2 minp, Vector2 maxp)
    {
        minPos = minp;
        maxPos = maxp;
    }
}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public bool isStunned = false;

    private Rigidbody rb;
    private GameObject player;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned) return;

        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        rb.AddForce(dir * speed);
    }
    public void Stun(float stunDuration)
    {
        StartCoroutine(StunRoutine(stunDuration));
    }
    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        Debug.Log("Enemy Stunned");
        rb.linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(duration);
        Debug.Log("Enemy Unstunned");
        isStunned = false;
    }
}

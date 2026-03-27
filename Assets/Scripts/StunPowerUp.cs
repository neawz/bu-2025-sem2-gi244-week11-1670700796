using System.Collections;
using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    public float stunDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemyObj in enemies)
                {
                    Enemy enemy = enemyObj.GetComponent<Enemy>();
                    enemy.Stun(stunDuration);
                }
                Destroy(gameObject);
            }
        }
    }
}

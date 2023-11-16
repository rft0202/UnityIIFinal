using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackTransform;
    public float attackRadius, attackStrength=10;

    public void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(attackTransform.position, attackRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
                hit.GetComponent<PlayerHealth>().TakeDamage(attackStrength);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
    }
}

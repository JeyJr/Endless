using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : MonoBehaviour
{
    public float damage;
    public LayerMask target;

    Vector3 boxSize;


    private void Awake()
    {
        boxSize = new Vector3(7.3f, 7.3f, 2);

        Invoke("RingOfFireDMG", .3f);
    }

    void RingOfFireDMG()
    {
        damage = damage < 1 ? 1 : damage;

        StartCoroutine(HitTarget());
        StartCoroutine(FadeOut());
    }

    IEnumerator HitTarget()
    {
        if (target == LayerMask.GetMask("Player"))
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector3.up, 1, target);
            if (hit.collider != null)
            {
                hit.collider.GetComponentInParent<PlayerStatus>().LoseLife(damage, false);
            }
        }
        else if (target == LayerMask.GetMask("Enemy"))
        {
            RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector3.up, 1, target);

            if (hit != null)
            {
                foreach (var item in hit)
                {
                    item.collider.GetComponent<EnemyStatus>().LoseLife(damage, false);
                }
            }
        }

        yield return null;
    }


    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.5f);

        for (float i = 1; i > -0.2f; i -= .1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(this.gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}

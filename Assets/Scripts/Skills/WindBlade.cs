using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlade : MonoBehaviour
{
    public float damage;
    public LayerMask target;
    public bool mirrored;

    public Vector3 boxSize;
    Vector3 targetPos;

    private void OnEnable()
    {
        damage = damage < 1 ? 1 : damage; 
        targetPos = new Vector3(transform.position.x * 20, transform.position.y, 2);
        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        if (mirrored)
            transform.localEulerAngles = new Vector3(0, 180, 0);
        else
            transform.localEulerAngles = new Vector3(0, 0, 0);
        
        transform.localPosition += transform.right * 8 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStatus>().LoseLife(damage, false);
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3);

        for (float i = 1; i > -0.2f; i -= .1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(this.gameObject);
    }

}

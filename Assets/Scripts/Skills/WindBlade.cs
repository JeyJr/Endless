using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindBlade : MonoBehaviour
{

    [Header("Initial valuese")]
    public bool mirrored;
    public float damage;
    public LayerMask target;

    [Header("Move control")]
    public float mSpeed;
    public float distance;
    public float delayToDestroy;

    [Header("Collision")]
    public Vector3 boxSize;


    private void OnEnable()
    {
        mSpeed = ManagerData.Load().MoveSpeed * 2;
        damage = damage < 1 ? 1 : damage; 
        transform.localEulerAngles = new Vector3(0, mirrored ? 180 : 0, 0);
        GameObject.FindWithTag("SFX").GetComponent<SFXControl>().PlayClip(SFXClip.windBlade);
        StartCoroutine(FadeOut());
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        Vector3 endPos = transform.position + transform.right * distance;

        while (elapsedTime < delayToDestroy)
        {
            transform.position = Vector3.Lerp(startingPos, endPos, (elapsedTime / delayToDestroy));
            elapsedTime += Time.deltaTime * mSpeed;
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            StartCoroutine(Dmg(collision));
    }

    IEnumerator Dmg(Collider2D collider)
    {
        collider.gameObject.GetComponentInChildren<EnemyStatus>().LoseLife(damage, false);
        yield return null;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delayToDestroy / 1.5f);

        for (float i = 1; i > -0.2f; i -= .1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(this.gameObject);
    }

}

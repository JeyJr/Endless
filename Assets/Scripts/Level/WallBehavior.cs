using System.Collections;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;

    [Header("Detect Player")]
    [SerializeField] private float maxDist;
    [SerializeField] private LayerMask target;

    public bool BossIsDead { get; set; }
    bool wallIsOpen = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.Play("Base Layer.Wall_Idle");
    }

    private void Update()
    {
        if (BossIsDead && !wallIsOpen)
        {
            Vector3 pos = transform.position;
            pos.y += 1; 
            RaycastHit2D hit = Physics2D.Raycast(pos, -transform.right, maxDist, target);

            if(hit.collider != null)
            {
                wallIsOpen = true;
                WallActiveHand();
            }
        }
    }

    public void WallActiveHand()
    {
        anim.Play("Base Layer.Wall_HandGrab");
    }

    //Called in last frame Wall_HandGrab
    public void WallEnd()
    {
        StartCoroutine(DestroyWall());
        StartCoroutine(MoveWall());
        anim.Play("Base Layer.Wall_End");
    }
    IEnumerator MoveWall()
    {
        while (this.gameObject.activeSelf)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(.01f);
        }
    }
    IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos.y += 1;
        Debug.DrawRay(pos, -transform.right * maxDist, Color.yellow);
    }
}

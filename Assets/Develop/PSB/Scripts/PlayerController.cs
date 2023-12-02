using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    private float v = 0;
    private float h = 0;

    Rigidbody rb;
    public float moveSpeed = 8f;
    public float curHp = 100;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0f, v).normalized;
        Vector3 move = transform.TransformDirection(moveDirection) * moveSpeed;

        // y 속도는 현재 Rigidbody의 y 속도를 유지
        move.y = rb.velocity.y;

        rb.velocity = move;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("EnemyAttack"))
        {
            curHp -= collision.gameObject.GetComponent<Projectile>().Damage;

            if(curHp <= 0)
            {
                //플레이어 죽음
            }
        }
    }
}

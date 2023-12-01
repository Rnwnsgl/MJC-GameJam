using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private float v = 0;
    private float h = 0;
    private Vector3 moveDir;

    [SerializeField]
    private float _moveSpeed = 5.0f;

    private void Awake()
    {
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

    public void Move()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");

        moveDir = new Vector3(h, 0, v);

        transform.Translate(moveDir.normalized * Time.deltaTime * _moveSpeed);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PortalManager pm;
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PortalManager>();
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(pm.IsLinkPortal)
        {
            if (other.CompareTag("Bullet"))
            {
                //Vector3 contactPoint = other.

                //if (gameObject.CompareTag("Portal1"))
                //{
                //    other.transform.position = pm.curPortals[1].GetComponentInChildren<Portal>().transform.position;
                //}
                //else if (gameObject.CompareTag("Portal2"))
                //{
                //    other.transform.position = pm.curPortals[0].GetComponentInChildren<Portal>().transform.position;
                //}
                if (gameObject.CompareTag("Portal1"))
                {
                    other.transform.GetComponent<Projectile>().Teleport(pm.curPortals[1].GetComponentInChildren<tpPos>().transform);
                }
                else if (gameObject.CompareTag("Portal2"))
                {
                    other.transform.GetComponent<Projectile>().Teleport(pm.curPortals[0].GetComponentInChildren<tpPos>().transform);
                }

            }
            else if(other.CompareTag("Player") && pm.CanUsePortal)
            {
                pm.UsePortal();
                if (gameObject.CompareTag("Portal1"))
                {
                    print("1");
                    other.transform.position = pm.curPortals[1].GetComponentInChildren<tpPos>().transform.position;
                }
                else if (gameObject.CompareTag("Portal2"))
                {
                    print("2");
                    other.transform.position = pm.curPortals[0].GetComponentInChildren<tpPos>().transform.position;

                }
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PortalManager pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PortalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("1");
        if(pm.IsLinkPortal)
        {
            print("2");
            if (other.CompareTag("Bullet"))
            {

            }
            else if(other.CompareTag("Player") && pm.CanUsePortal)
            {
                print("3");
                if (gameObject.CompareTag("Portal1"))
                {
                    print("4");
                    pm.UsePortal();
                    other.transform.position = pm.curPortals[1].GetComponentInChildren<tpPos>().transform.position;
                }
                else if (gameObject.CompareTag("Portal2"))
                {
                    print("5");
                    pm.UsePortal();
                    other.transform.position = pm.curPortals[0].GetComponentInChildren<tpPos>().transform.position;
                }
            }
        }
    }
}

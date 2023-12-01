using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalProjectile : MonoBehaviour
{
    
    public float Timer = 5f;
    public float TimerEXP = 300f;

    public bool Detach = false;

    PortalManager pm;

    private void Awake()
    {
        pm = FindObjectOfType<PortalManager>();
    }
    void Start()
    {
        Destroy(gameObject, Timer);
    }

    void Exp(Collision col)
    {
        if (Detach)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject, 1f);
                child.parent = null;
            }
        }

        if (pm.PortalNum > 1)
        {
            pm.ClearPortal();
        }

        GameObject portalObj = Instantiate(pm.PortalPrefabs[pm.PortalNum], transform.position, Quaternion.LookRotation(col.GetContact(0).normal));
        pm.GenPortal(portalObj);

        Destroy(gameObject);

        Destroy(portalObj, TimerEXP);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Exp(collision);
    }


}

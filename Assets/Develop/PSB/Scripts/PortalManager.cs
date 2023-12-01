using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    int portalNum = 0;
    float portalCooltime = 2f;
    bool canUsePortal = true;

    public GameObject[] PortalPrefabs;

    public int PortalNum { get { return portalNum; } }
    public bool IsLinkPortal { get { return curPortals.Count == 2; } }
    public float PortalCooltime { get { return portalCooltime; } }
    public bool CanUsePortal { get { return canUsePortal; } }

    public List<GameObject> curPortals;

    private void Update()
    {

    }

    //포탈을 생성
    public void GenPortal(GameObject portalObj)
    { 
        curPortals.Add(portalObj);
        portalNum++;
    }

    //게임 상의 모든 포탈을 소멸
    public void ClearPortal()
    {
        foreach (var portal in curPortals)
        {
            Destroy(portal);
        }

        curPortals.Clear();
        portalNum = 0;
    }

    public void UsePortal()
    {
        StartCoroutine("CoolDownPortal");
    }

    IEnumerator CoolDownPortal()
    {
        canUsePortal = false;
        yield return new WaitForSeconds(portalCooltime);
        canUsePortal = true;
    }

}

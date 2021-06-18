using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public static FxManager obj;
    public GameObject pop;

    void Awake()
    {
        obj = this;
    }

    public void showPop(Vector3 pos)
    {
        pop.gameObject.GetComponent<Pop>().show(pos);
    }

    void OnDestroy()
    {
        obj = null;
    }


}

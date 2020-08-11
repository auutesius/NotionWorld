using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;

public class RecycleMe : MonoBehaviour
{
    public float DistoryTime = 1f;
    void OnEnable()
    {
        StartCoroutine(Recycle());
    }
    IEnumerator Recycle()
    {
        yield return new WaitForSeconds(DistoryTime);
        ObjectPool.RecycleObject(gameObject);

    }
}

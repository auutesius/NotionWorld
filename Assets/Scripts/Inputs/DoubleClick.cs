using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick : MonoBehaviour
{
    private void Update() 
    {
        if(Input.touchCount == 2)
        {
            Debug.Log("Double Clicked");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPosition : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 position;

    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
    }
}

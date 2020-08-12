using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;

public class JoyStick : MonoBehaviour
{
    public RectTransform stick;
    public float radius = 100;
    private bool hide = false;
    private float hideTimer = 1.0f;
    public Vector2 Vector => stick.anchoredPosition / radius;

    private JoyStickMovedEventArgs eventArgs;

    private void Awake()
    {
        eventArgs = new JoyStickMovedEventArgs(Vector);
    }

    public void OnStickMoved()
    {
        stick.anchoredPosition = LimitRadius(stick.anchoredPosition);

        eventArgs = new JoyStickMovedEventArgs(Vector);
        if (eventArgs != null)
        {
            eventArgs.Vector = Vector;
        }
        EventCenter.DispatchEvent(eventArgs);
    }

    private Vector2 LimitRadius(Vector2 position)
    {
        if (position.magnitude > radius)
        {
            position = position.normalized * radius;
        }
        return position;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = Input.mousePosition;
            transform.GetChild(0).gameObject.SetActive(true);
            hide = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            hide = true;
        }
        if (hide && !Input.GetMouseButton(0))
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0)
            {
                hideTimer = 1.0f;
                hide = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            transform.position = Input.mousePosition;
            transform.GetChild(0).gameObject.SetActive(true);
            hide = false;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            hide = true;
        }
        if (hide && (Input.touchCount < 0 || Input.GetTouch(0).phase != TouchPhase.Moved))
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0)
            {
                hideTimer = 1.0f;
                hide = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
#endif
    }
}
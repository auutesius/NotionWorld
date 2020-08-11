using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;

public class JoyStick : MonoBehaviour
{
    public RectTransform stick;
    public float radius = 200;
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
}
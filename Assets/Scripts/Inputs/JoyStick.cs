using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;

public class JoyStick : MonoBehaviour
{
    // public RectTransform stick;
    // public float radius = 100;
    // private bool hide = false;
    // private float hideTimer = 1.0f;
    public Vector2 Vector;// => stick.anchoredPosition / radius;

    private JoyStickMovedEventArgs eventArgs;
    //获取到场景中的Joystick
    public ETCJoystick controlETCJoystick;
    //获取场景中的Button
    //public ETCButton controlETCButton;
    private void Awake()
    {

        eventArgs = new JoyStickMovedEventArgs(Vector);
        //controlETCJoystick = ETCInput.GetControlJoystick("Joystick");
        Vector = new Vector2(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue);
#if UNITY_EDITOR
        controlETCJoystick.joystickType = ETCJoystick.JoystickType.Static;
#endif
        //controlETCButton = ETCInput.GetControlButton("Thumb");
    }


    private void Update()
    {
        Vector = new Vector2(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue);
        eventArgs = new JoyStickMovedEventArgs(Vector);
        if (eventArgs != null)
        {
            eventArgs.Vector = Vector;
        }
        EventCenter.DispatchEvent(eventArgs);
    }

}
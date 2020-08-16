using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;

public class JoyStick : MonoBehaviour
{

    public Vector2 Vector;

    private JoyStickMovedEventArgs eventArgs;
    //获取到场景中的Joystick
    public ETCJoystick controlETCJoystick;
    //获取场景中的Button
    //public ETCButton controlETCButton;
    private void Awake()
    {

        eventArgs = new JoyStickMovedEventArgs(Vector);
        Vector = new Vector2(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue);
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
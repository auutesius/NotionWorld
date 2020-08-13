using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;

public class JoyStick : MonoBehaviour
{

    public Vector2 Vector;
    public SkillControllerForPlayer skillController;

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
// #if UNITY_EDITOR
//         controlETCJoystick.joystickType = ETCJoystick.JoystickType.Static;
// #endif
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

    public void LongTap(){
        Debug.Log("LongTap");
       
    }
    public void DoubleTap(){
        Debug.Log("DoubleTap");
        skillController.InvincibleButton();
        skillController.ripple.Emit(new Vector2(0f,0f));
        Animator playerAnim = GameObject.Find("Player").transform.GetChild(0).GetComponent<Animator>();
        playerAnim.Play("muteki");
        Debug.Log("Muteki!!!!");
    }

}
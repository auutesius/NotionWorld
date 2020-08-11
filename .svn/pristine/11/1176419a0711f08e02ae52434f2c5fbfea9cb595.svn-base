using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;

public class PlayerController : MonoBehaviour, ISubscriber<JoyStickMovedEventArgs>
{
    public EntityMovement movement;
    public WeaponController weaponController;

    private void Awake()
    {
        EventCenter.Subscribe(this);
    }

    public void OnEventOccurred(JoyStickMovedEventArgs eventArgs)
    {
        movement.Direction = eventArgs.Vector;
        weaponController.forward = eventArgs.Vector.normalized;
    }
}

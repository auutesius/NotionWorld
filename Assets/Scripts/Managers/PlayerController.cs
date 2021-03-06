﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;
using NotionWorld.Entities;
using NotionWorld.Capabilities;

public class PlayerController : MonoBehaviour, ISubscriber<JoyStickMovedEventArgs>
{
    public Entity entity;
    [Tooltip("可以充能的连击数")]
    public int ChargeEnergyCombo;
    [Tooltip("每次充能的能量数")]
    public int EnergyPerCharge;
    public SkillControllerForPlayer skillController;

    private ComboController comboController;
    private EntityMovement movement;
    private WeaponController weaponController;

    private void Awake()
    {
        EventCenter.Subscribe(this);
    }

    private void Start()
    {
        weaponController = entity.transform.GetComponent<WeaponController>();
        movement = entity.transform.GetComponent<EntityMovement>();
        comboController = transform.GetComponent<ComboController>();
    }

    public void OnEventOccurred(JoyStickMovedEventArgs eventArgs)
    {
        movement.Direction = eventArgs.Vector;
        weaponController.forward = eventArgs.Vector.normalized;
    }

    private void Update()
    {
        if (entity != null)
        {
            EnergyUpdate(); 

        }
    }

    private void EnergyUpdate()
    {
        var energy = entity.GetCapability<Energy>();

        if (comboController.ComboValue != 0 && comboController.ComboValue % ChargeEnergyCombo == 0)
        {
            energy.Value += EnergyPerCharge;
        }

        if (energy.Value > energy.MaxValue)
        {
            energy.Value = energy.MaxValue;
        }
        else if (energy.Value == energy.MaxValue)
        {
            skillController.SetSkillAvailable(true);
        }

        entity.GetComponent<UIController>().UpdateCombo(comboController.ComboValue);
    }
    public void BuildUpStrength(){
        var energy = entity.GetCapability<Energy>();
        energy.Value += EnergyPerCharge;
        entity.transform.GetChild(3).gameObject.SetActive(true);
        //entity.transform.GetChild(3).GetComponent<Animator>().Play("BuildUpStrength");
        StartCoroutine(CloseBuildUpStrngth(entity));
    }
    IEnumerator CloseBuildUpStrngth(Entity entity){
        yield return new WaitForSeconds(1f);
        entity.transform.GetChild(3).gameObject.SetActive(false);
    }

}

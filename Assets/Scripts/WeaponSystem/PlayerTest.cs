using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using UnityEngine.UI;
/// <summary>
/// 暂时用于部分未使用摇杆操作时的Player行为
/// </summary>
public class PlayerTest : MonoBehaviour
{
    public float speed;
    public SkillControllerForPlayer skillController;
    public WeaponController WC;
    private Rigidbody2D RB;
    private Entity entity;
    Energy energy;



    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();
        energy = entity.GetCapability<Energy>();


    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChargeEnergy();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 attackDir = Vector2.right;
            WC.transform.rotation = WC.transform.rotation * Quaternion.Euler(new Vector3(0f,0f,45f));
            // Vector3 UpDir = 
        }
        // Debug.Log(enmey.GetCapability<Health>().Value);

        if (skillController.GetIsWorking())
        {
            energy.Value = 0f;
        }
    }

    // TODO 等待角色控制架构确认如何与交互控制
    private void ChargeEnergy()
    {
        energy.Value++;
        if (energy.Value > 10)
        {
            energy.Value = 10;
        }
        else if (energy.Value == 10)
        {
            skillController.SetSkillAvailable(true);
        }
    }
    


}

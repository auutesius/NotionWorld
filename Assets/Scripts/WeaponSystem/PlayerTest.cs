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
    private Rigidbody2D RB;
    public Entity enmey;
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
            Animator animator = entity.transform.GetChild(0).GetComponent<Animator>();
            Vector3 s = animator.transform.localScale;
            animator.transform.localScale = new Vector3(s.x * -1, s.y, s.z);
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

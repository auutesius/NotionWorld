using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using BehaviorDesigner.Runtime;

public class UIController : MonoBehaviour
{
    public Camera MainCamera;
    public Image HealthSlider;
    public Image EnergySlider;
    public Text ComboCount;
    public Image AimImage;
    public Image AimRange;
    public bool HasArrowPointer;
    private Image ArrowImage;
    Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (HasArrowPointer)
        {
            GameObject arrow = NotionWorld.Worlds.ObjectPool.GetObject("Arrow", "UI");
            ArrowImage = arrow.GetComponent<Image>();
            ArrowImage.transform.SetParent(GameObject.Find("MainCanvas").transform);
            arrow.transform.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        EditSlider();
        AimingTracing();
        AttackRadius();
        ArrowTracing();
    }

    public void UpdateCombo(int value)
    {
        if (ComboCount != null)
        {
            ComboCount.text = value.ToString();
        }
    }

    public void EditSlider()
    {
        if (HealthSlider != null)
        {
            var health = entity.GetCapability<Health>();
            HealthSlider.fillAmount = (float)health.Value / health.MaxValue;
        }

        if (EnergySlider != null)
        {
            var energy = entity.GetCapability<Energy>();
            EnergySlider.fillAmount = (float)energy.Value / energy.MaxValue;
        }
    }

    /// <summary>
    /// 有武器时对最近的对象瞄准的UI
    /// </summary>
    public void AimingTracing()
    {
        // AimImage是否存在，减少检查
        if (AimImage != null)
        {
            WeaponController WC = GetComponentInChildren<WeaponController>();
            // 以免挂载到无武器的对象上空引用
            if (WC != null)
            {
                AimImage.gameObject.SetActive(WC.CheckAttackTarget() != null);
                // 是否有瞄准目标
                if (WC.CheckAttackTarget() != null)
                {
                    AimImage.transform.position = MainCamera.WorldToScreenPoint(WC.CheckAttackTarget().transform.position);
                }
            }
        }
    }

    /// <summary>
    /// 有武器时展示攻击范围
    /// </summary>
    public void AttackRadius()
    {
        if (AimRange != null)
        {
            AimRange.transform.localScale = Vector3.one * entity.GetCapability<Attack>().Range / 0.26f; // 魔法数字0.26是UI与场景比例的转换
            AimRange.transform.position = MainCamera.WorldToScreenPoint(entity.transform.position);
        }
    }

    /// <summary>
    /// 从屏幕边缘指向屏幕外对象
    /// </summary>
    /// <returns></returns>
    void ArrowTracing()
    {
        if (!HasArrowPointer) return;

        if (IsInScreen())
        {
            ArrowImage.gameObject.SetActive(false);
            return;
        }
        ArrowImage.gameObject.SetActive(true);
        Vector3 cameraPoint = MainCamera.WorldToScreenPoint(MainCamera.transform.position);
        Vector3 thisPoint = MainCamera.WorldToScreenPoint(transform.position);
        if (!IsAtCorner(thisPoint))
        {
            // 魔法数字8是图标偏移量
            Vector2 targetCoeffient = GetCoeffient(cameraPoint, thisPoint);
            Vector2 screenCoeffient = Vector2.zero;
            if (thisPoint.x > Screen.width)
            {
                ArrowImage.transform.position = GetCrossPosByX(targetCoeffient, Screen.width - 8f);
            }
            else if (thisPoint.x < 0f)
            {
                ArrowImage.transform.position = GetCrossPosByX(targetCoeffient, 8f);
            }
            else if (thisPoint.y < 0f)
            {
                ArrowImage.transform.position = GetCrossPosByY(targetCoeffient, 8f);
                //screenCoeffient = GetCoeffient(new Vector2(0, 0f), new Vector2(Screen.width, 0f));
                //ArrowImage.transform.position = GetCrossPosByCoeffient(targetCoeffient, screenCoeffient); Debug.Log(targetCoeffient + "," + screenCoeffient + "," + GetCrossPosByCoeffient(targetCoeffient, screenCoeffient));
            }
            else if (thisPoint.y > Screen.height)
            {
                ArrowImage.transform.position = GetCrossPosByY(targetCoeffient, Screen.height - 8f);
                //screenCoeffient = GetCoeffient(new Vector2(0, Screen.height), new Vector2(Screen.width, Screen.height));
                //ArrowImage.transform.position = GetCrossPosByCoeffient(targetCoeffient, screenCoeffient);

            }
        }
        else
        {
            if (thisPoint.x > Screen.width && thisPoint.y > Screen.height)
            {
                ArrowImage.transform.position = new Vector2(Screen.width - 8f, Screen.height - 8f);
            }
            else if (thisPoint.x > Screen.width && thisPoint.y < 0f)
            {
                ArrowImage.transform.position = new Vector2(Screen.width - 8f, 8f);
            }
            else if (thisPoint.x < 0f && thisPoint.y > Screen.height)
            {
                ArrowImage.transform.position = new Vector2(8f, Screen.height - 8f);
            }
            else
            {
                ArrowImage.transform.position = new Vector2(8f, 8f);
            }
        }

        // 旋转
        Vector3 yDir = ArrowImage.transform.up;
        Vector3 targetDir = thisPoint - cameraPoint;
        float euler = Vector3.Angle(yDir, targetDir);
        ArrowImage.transform.Rotate(new Vector3(0f, 0f, Vector3.Cross(yDir, targetDir).z > 0 ? euler : -euler));
    }

    /// <summary>
    /// 判断此物体是否位于摄像机内
    /// </summary>
    /// <returns></returns>
    bool IsInScreen()
    {
        Vector2 thisPoint = MainCamera.WorldToScreenPoint(transform.position);
        return (thisPoint.x < Screen.width && thisPoint.x>0f && thisPoint.y>0f && thisPoint.y < Screen.height);
    }

    /// <summary>
    /// 判断此物体是否位于对角区域
    /// </summary>
    /// <param name="d">物体的屏幕坐标</param>
    /// <returns></returns>
    bool IsAtCorner(Vector3 d)
    {
        return (d.x > Screen.width && (d.y > Screen.height || d.y < 0f)) || (d.x<0 && (d.y > Screen.height || d.y < 0));
    }
    /// <summary>
    /// 获得ab两点连线方程的斜率与截距
    /// </summary>
    /// <param name="a">a点</param>
    /// <param name="b">b点</param>
    /// <returns>Vector2(斜率,y轴截距)</returns>
    Vector2 GetCoeffient(Vector2 a, Vector2 b)
    {
        if (a.x == b.x)
        {
            return Vector2.zero;
        }
        else if (a.y == b.y)
        {
            return new Vector2(0f,a.y);
        }
        else
        {
            return new Vector2((a.y - b.y) / (a.x - b.x), a.y - (b.y - a.y) / (b.x - a.x) * a.x);
        }
    }
    /// <summary>
    /// 获得a，b两直线方程的相交点坐标(斜截式)
    /// </summary>
    /// <param name="a">a方程的系数(x,y)</param>
    /// <param name="b">b方程的系数(x,y)</param>
    /// <returns>相交点坐标</returns>
    Vector2 GetCrossPosByCoeffient(Vector2 a,Vector2 b)
    {
        if (a.x != b.x)
        {
            return new Vector2((b.y - a.y) / (a.x - b.x), (a.y * b.x - a.x - b.y) / (b.x - a.x));
        }
        else
        {
            return Vector2.zero;
        }
    }

    /// <summary>
    /// 已知y与点截式求x得交点坐标
    /// </summary>
    /// <param name="coeffient">直线方程点截式系数</param>
    /// <param name="x">已知的y值</param>
    /// <returns>交点坐标</returns>
    Vector2 GetCrossPosByY(Vector2 coeffient, float y)
    {
        return new Vector2((y-coeffient.y)/coeffient.x, y);
    }
    /// <summary>
    /// 已知x与点截式求y得交点坐标
    /// </summary>
    /// <param name="coeffient">直线方程点截式系数</param>
    /// <param name="x">已知的x值</param>
    /// <returns>交点坐标</returns>
    Vector2 GetCrossPosByX(Vector2 coeffient, float x)
    {
        return new Vector2(x, x * coeffient.x + coeffient.y);
    }
}


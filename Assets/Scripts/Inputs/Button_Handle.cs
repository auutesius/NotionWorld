using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Button_Handle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    //UI_Button 控制
    public GameObject pad;//手柄圆
    public GameObject stick;//触摸圆
    public Camera mainCamera;//摄像机

    bool isonCon = false;//是否正在控制
                         //接口
    public void OnBeginDrag(PointerEventData eventData)//滑动开始
    {

    }

    public void OnDrag(PointerEventData eventData)//滑动中
    {
        //获取ViewPoint
        Vector3 viewPoint = Camera.main.ScreenToViewportPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
        Vector2 panelViewPoint = changeInBase(viewPoint, gameObject);
        //
        Vector2 button_base_ViewPoint = changeInBase(panelViewPoint, pad.gameObject);

        Vector2 toMiddlePoint = new Vector2(button_base_ViewPoint.x - 0.5f, button_base_ViewPoint.y - 0.5f);
        float disToMiddlePoint = Mathf.Sqrt(toMiddlePoint.x * toMiddlePoint.x + toMiddlePoint.y * toMiddlePoint.y);
        if (disToMiddlePoint - 0.5 < 0.001)//在范围内
        {
            //按键设置
            setToViewPoint(panelViewPoint, pad.gameObject, stick.gameObject, true);

        }
        else//不在范围内
        {
            //按键设置
            Vector2 goToViewPoint = toMiddlePoint.normalized * 0.5f;
            setToViewPoint(new Vector2(goToViewPoint.x + 0.5f, goToViewPoint.y + 0.5f), pad.gameObject, stick.gameObject, false);

        }
    }

    public void OnPointerDown(PointerEventData eventData)//触摸开始
    {
        pad.SetActive(true);//显示

        //根据初始点击点 设置【手柄圆】的位置
        Vector3 viewPoint = Camera.main.ScreenToViewportPoint(new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0));
        setToViewPoint(viewPoint, gameObject, pad, true);
        //根据初始点击点 设置【触点圆】的位置
        setToViewPoint(new Vector2(0.5f, 0.5f), pad, stick, false);
    }
    void setToViewPoint(Vector2 viewPoint, GameObject objbase, GameObject objset, bool model)
    //根据base中view点 设置set RectTrans位置 model true:由点击触发考虑baseView偏移值(进行ViewPoint转入) false:不考虑baseView偏移值(硬性设置）
    {
        //获取RectTransform
        RectTransform objset_rt = objset.GetComponent<RectTransform>();
        //获取Img Anchor占比距离 Panel Anchor占比距离
        float objset_x = objset_rt.anchorMax.x - objset_rt.anchorMin.x;
        float objset_y = objset_rt.anchorMax.y - objset_rt.anchorMin.y;
        Vector2 topanel_Point;//最终需要的坐标位置
        if (model)
        {
            topanel_Point = changeInBase(viewPoint, objbase);//将高一级的ViewPoint转入低一级中
        }
        else
        {
            topanel_Point = viewPoint;
        }
        objset_rt.anchorMin = new Vector2(topanel_Point.x - objset_x / 2, topanel_Point.y - objset_y / 2);
        objset_rt.anchorMax = new Vector2(topanel_Point.x + objset_x / 2, topanel_Point.y + objset_y / 2);
    }
    Vector2 changeInBase(Vector2 viewPoint, GameObject objbase)//将高一级的ViewPoint 转入低一级
    {
        //获取RectTransform
        RectTransform objbase_rt = objbase.GetComponent<RectTransform>();

        float objbase_x = objbase_rt.anchorMax.x - objbase_rt.anchorMin.x;
        float objbase_y = objbase_rt.anchorMax.y - objbase_rt.anchorMin.y;
        //获取ViewPoint转换至objbase中的对应的View点

        return new Vector2((viewPoint.x - objbase_rt.anchorMin.x) / objbase_x, (viewPoint.y - objbase_rt.anchorMin.y) / objbase_y);

    }

    public void OnPointerUp(PointerEventData eventData)//触摸结束
    {
        pad.gameObject.SetActive(false);//取消显示
    }
    //Base
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
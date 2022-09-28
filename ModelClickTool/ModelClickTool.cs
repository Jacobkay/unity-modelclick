using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelClickTool : MonoBehaviour
{
    public event Action<GameObject> ModelClick;
    /// <summary>
    /// 实例化
    /// </summary>
    private static ModelClickTool instance = null;
    private static object oLock = new object();
    public static ModelClickTool Instance
    {
        get
        {
            if (null == instance)
            {
                lock (oLock)
                {
                    GameObject obj = new GameObject();
                    obj.name = "ModelClickController(Singleton)";
                    ModelClickTool modelClickController = obj.AddComponent<ModelClickTool>();
                    ModelClickTool.instance = modelClickController;
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    long startTime;
    bool click;
    Vector3 inputPosition;
    GameObject downObj;
    GameObject upObj;
    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PointDown(Input.GetTouch(0).position);
            }
            else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                PointUp(Input.GetTouch(0).position);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointDown(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                PointUp(Input.mousePosition);
            }
        }
    }
    /// <summary>
    /// 按下
    /// </summary>
    void PointDown(Vector3 position)
    {
        inputPosition = position;
        downObj = IsClick(inputPosition);
        if (null != downObj)
        {
            click = true;
            startTime = Utils.GetMilliseconds();
        }
    }
    /// <summary>
    /// 抬起
    /// </summary>
    void PointUp(Vector3 position)
    {
        upObj = IsClick(position);
        bool isSameModel = upObj.GetHashCode() == downObj.GetHashCode();
        bool isClickTime = (Utils.GetMilliseconds() - startTime <= 220);
        bool isSamePlace = Vector3.Distance(position, inputPosition) <= 10;
        if (null != upObj && isSameModel && click && isClickTime && isSamePlace)
        {
            if (null != ModelClick)
            {
                ModelClick.Invoke(upObj);
            }
        }
        click = false;
    }
    /// <summary>
    /// 判断点击的对象是否为本体
    /// </summary>
    /// <param name="clickPos"></param>
    /// <returns></returns>
    private GameObject IsClick(Vector2 clickPos)
    {
        if (Utils.IsPointerOverUIGameObject())
        {
            return null;
        }
        Ray ray = Camera.main.ScreenPointToRay(clickPos);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit))
        {
            return rayHit.collider.gameObject;
        }
        return null;
    }
    private void OnDestroy()
    {
        downObj = null;
        upObj = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZTools
{
    public class ModelClickTool : MonoBehaviour
    {
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
        /// <summary>
        /// 模型点击 
        /// </summary>
        public event Action<GameObject> ModelClick;

        /// <summary>
        /// 设置射线有效距离
        /// </summary>
        public float rayMaxDis = 3000;
        public float RayMaxDistance
        {
            set { rayMaxDis = value; }
        }

        long startTime;
        bool click;
        Vector3 inputPosition;
        GameObject downObj;
        GameObject upObj;
        private void Update()
        {
            if (null != ModelClick)
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
                startTime = GetMilliseconds();
            }
        }
        /// <summary>
        /// 抬起
        /// </summary>
        void PointUp(Vector3 position)
        {
            upObj = IsClick(position);
            if (null != upObj && upObj.GetHashCode() == downObj.GetHashCode() && click && (GetMilliseconds() - startTime <= 220) && Vector3.Distance(position, inputPosition) <= 10)
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
            if (IsPointerOverUIGameObject())
            {
                return null;
            }
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, rayMaxDis))
            {
                return rayHit.collider.gameObject;
            }
            return null;
        }
        bool IsPointerOverUIGameObject()
        {
            if (!Application.isMobilePlatform)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return true;
                }
                return false;
            }
            else
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (EventSystem.current == null)
                    {
                        continue;
                    }

                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        long unixBaseMillis = new DateTime(1970, 1, 1, 0, 0, 0).ToFileTimeUtc() / 10000;
        DateTime lastCacheDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
        long lastCacheMillis = 0;
        long GetMilliseconds()
        {
            DateTime now = DateTime.Now;
            if (lastCacheDateTime != now)
            {
                lastCacheDateTime = now;
                lastCacheMillis = (now.ToFileTime() / 10000) - unixBaseMillis;
            }
            return lastCacheMillis;
        }
        private void OnDestroy()
        {
            downObj = null;
            upObj = null;
        }
    }
}

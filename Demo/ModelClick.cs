using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTools;

public class ModelClick : MonoBehaviour
{

    void Start()
    {
        ModelClickTool.Instance.ModelClick += obj =>
        {
            Debug.Log(obj.name);
        };
    }
}

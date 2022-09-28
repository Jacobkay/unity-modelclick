using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

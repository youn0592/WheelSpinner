using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToParent : MonoBehaviour
{
    public WheelManager WM;
    // Start is called before the first frame update
    void Start()
    {
        WM.childObj.Add(gameObject);
    }

}

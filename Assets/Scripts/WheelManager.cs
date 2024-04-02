using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public struct CircleAngle
{
    public CircleAngle(float min, float max)
    {
        minAngle = min;
        maxAngle = max;
    }

    public float minAngle;
    public float maxAngle;
}
public class WheelManager : MonoBehaviour
{
    ValueToPercent valToPercent;
    InputFieldManager InputManager;

    [HideInInspector]
    public List<GameObject> childObj;

    [SerializeField]
    public List<CircleAngle> angles = new List<CircleAngle>();

    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        valToPercent = GameObject.Find("GameManager").GetComponent<ValueToPercent>();
        InputManager = GameObject.Find("Prompts").GetComponent<InputFieldManager>();
        if(valToPercent == null || InputManager == null)
        {
            Debug.LogError("NULL");
        }

        RectTransform[] child = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform children in child)
        {
            childObj.Add(children.gameObject);
            children.gameObject.SetActive(false);
        }

        startPos = child[0].position;

        InputManager.PositiveIncrement += CreateUI;
        InputManager.NegativeIncrement += CreateUI;
        InputManager.NumberChanged += CreateUI;

        CreateUI();
    }

    public void CreateUI()
    {
        ResetValues();
        int numOfVals = valToPercent.NumberOfValues();
        float moveNext = 0;
        float threesixty = 360.0f;
        for (int i = 0; i < numOfVals; i++)
        {
            Vector3 vecRotate = new Vector3(0, 0, moveNext);
            childObj[i].GetComponent<RectTransform>().Rotate(vecRotate);
            Image img = childObj[i].GetComponent<Image>();
            float num = valToPercent.GetDecimalPercentageAt(i);
            img.fillAmount = num;
            float angle = 360 * num;
            CalculateMinMaxAngle(threesixty, angle);
            threesixty -= angle;
            moveNext = threesixty;
            childObj[i].SetActive(true);
        }
        GetAtAngle(0);
    }

    void ResetValues()
    {
        foreach (GameObject gO in childObj)
        {
            Quaternion quat = new Quaternion(0, 0, 0, 0);
            gO.GetComponent<RectTransform>().SetPositionAndRotation(startPos, quat);
            gO.GetComponent<Image>().fillAmount = 1.0f;
            gO.SetActive(false);
            if (angles.Count > 0)
            {
                angles.Clear();
            }
        }
    }
    void CalculateMinMaxAngle(float threesity, float angle)
    {
        //360, 16% 
        float max = threesity;
        float min = threesity - angle;
        if(min < 0.01)
        {
            min = 0;
        }
        angles.Add(new CircleAngle(min, max));
    }

    public GameObject GetAtAngle(float atAngle)
    {
        int i = 0;
        foreach(CircleAngle circ in angles)
        {
            //Debug.Log("OBJ: " + childObj[i] + " Angle of " + circ.minAngle + " " + circ.maxAngle);
            if(atAngle >= circ.minAngle && atAngle <= circ.maxAngle)
            {
                return childObj[i];
            }
            i++;
            continue;
        }
        Debug.LogError("Angle never hit");
        return null;
    }

    private void OnDestroy()
    {
        InputManager.PositiveIncrement -= CreateUI;
        InputManager.NegativeIncrement -= CreateUI;
        InputManager.NumberChanged -= CreateUI;
    }
}

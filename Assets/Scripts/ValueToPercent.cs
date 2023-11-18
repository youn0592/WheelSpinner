using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ValueToPercent : MonoBehaviour
{

    public List<float> values;
    public List<float> percent;
    float totalValue = 0;


    public void GatherPercent()
    {
        totalValue = 0;
        percent.Clear();
        foreach(float val in values)
        {
            totalValue += val;
        }
        foreach(float val in values)
        {
            float num = (val / totalValue) * 100;
            percent.Add(num);
            //Debug.Log("Number: " + val + " Percentage: " + num + " Total Value: " + totalValue);
        }
    }

    public int NumberOfValues()
    {
        return values.Count;
    }

    public float GetDecimalPercentageAt(int val)
    {
        /*if(val > percent.Max() || val < percent.Min())
        {
            Debug.LogError("percent was out of range");
            return 0; 
        }*/
        GatherPercent();
        return percent[val] / 100;
    }

    public void AddValue(float val)
    {
        values.Add(val);
        GatherPercent();
    }
    public void RemoveValue()
    {
        int i = values.Count - 1;
        values.RemoveAt(i);
        GatherPercent();
    }

    public void SetValue(int colorNum, float val)
    {
        values[colorNum] = val;
        GatherPercent();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputChange : MonoBehaviour
{

    TMP_InputField inputField;

    public int colorNum;

    ValueToPercent numberManager;
    InputFieldManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(delegate { InputChanged(); });
        inputField.onDeselect.AddListener(delegate { FieldDeselected(); });

        numberManager = GameObject.Find("GameManager").GetComponent<ValueToPercent>();
        inputManager = GetComponentInParent<InputFieldManager>();

        if (numberManager == null || inputManager == null)
        {
            Debug.LogError("NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InputChanged()
    {
        float num;
        if (inputField.text.Length <= 0)
        {
            num = 0;
        }
        else
        {
            num = float.Parse(inputField.text);
        }
        numberManager.SetValue(colorNum, num);
        inputManager.ValueChanged();
    }

    void FieldDeselected()
    {
        if(inputField.text.Length <= 0)
        {
            inputField.text = "0";
        }
    }
}

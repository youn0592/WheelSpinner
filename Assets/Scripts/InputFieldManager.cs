using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{
    ValueToPercent numberManager;

    List<GameObject> childObj = new List<GameObject>();

    public event Action PositiveIncrement;
    public event Action NegativeIncrement;
    public event Action NumberChanged;

    int numberOfFields;

    Button PositiveButton;
    Button NegativeButton;



    // Start is called before the first frame update
    void Start()
    {
        InputChange[] child = GetComponentsInChildren<InputChange>();
        for (int i = 0; i < child.Length; i++)
        {
            childObj.Add(child[i].gameObject);
            child[i].colorNum = i;

            if (i == 0 || i == 1)
            {
                continue;
            }
            child[i].gameObject.SetActive(false);
        }


        PositiveButton = GameObject.Find("PlusButton").GetComponent<Button>();
        NegativeButton = GameObject.Find("NegativeButton").GetComponent<Button>();

        PositiveButton.onClick.AddListener(delegate { PostivePressed(); });
        NegativeButton.onClick.AddListener(delegate { NegativePressed(); });

        numberManager = GameObject.Find("GameManager").GetComponent<ValueToPercent>();
        numberOfFields = 2; ;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PostivePressed()
    {
        if (PositiveButton.interactable == false)
        {
            return;
        }
        NegativeButton.interactable = true;
        childObj[numberOfFields].SetActive(true);
        ++numberOfFields;

        numberManager.AddValue(1.0f);

        if (PositiveIncrement != null)
        {
            PositiveIncrement();
        }
        if (numberOfFields == childObj.Count)
        {
            PositiveButton.interactable = false;
        }
    }

    public void NegativePressed()
    {
        if (NegativeButton.interactable == false)
        {
            return;
        }
        PositiveButton.interactable = true;
        --numberOfFields;
        childObj[numberOfFields].SetActive(false);

        numberManager.RemoveValue();

        if (NegativeIncrement != null)
        {
            NegativeIncrement();
        }

        if (numberOfFields <= 2)
        {
            NegativeButton.interactable = false;
        }
    }

    public void ValueChanged()
    {
        if (NumberChanged != null)
        {
            NumberChanged();
        }
    }
}

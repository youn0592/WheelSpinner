using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.Net.Http.Headers;
using UnityEditor.UIElements;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{

    public GameObject winnerText;

    WheelManager wheelManager;
    InputFieldManager fieldManager;

    float ZRot;
    [SerializeField]
    float minSpeed, maxSpeed;

    int timePassed = 0;

    bool bIsSpun = false;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        wheelManager = GetComponent<WheelManager>();
        fieldManager = GameObject.Find("Prompts").GetComponent<InputFieldManager>();
        if(wheelManager == null || fieldManager == null)
        {
            Debug.LogError("NULL");
        }
        fieldManager.PositiveIncrement += ResetWheel;
        fieldManager.NegativeIncrement += ResetWheel;
        
        pos = transform.position;
        winnerText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (bIsSpun == true)
        {
            winnerText.SetActive(false);
            timePassed++;
            float SpinSpeed = Random.Range(minSpeed, maxSpeed);
            ZRot += SpinSpeed * Time.deltaTime;
            Quaternion rot = new Quaternion(0, 0, ZRot, 0);
            float num = Mathf.Cos(ZRot);
            transform.Rotate(new Vector3(0, 0, num));

            if (num <= 0)
            {
                bIsSpun = false;
                Win();
            }
        }
    }

    public void Spin()
    {
        ZRot = 0;
        bIsSpun = true;
    }

    void Win()
    {
        float rot = transform.rotation.eulerAngles.z;
        if (rot <= 0)
        {
            rot += 360.0f;
        }
        if (rot > 360)
        {
            rot -= 360;
        };

        float num = 0;
        if (rot <= 90)
        {
            num = Mathf.Abs(90 - rot);
        }
        else
        {
            float bigger90 = 360 + 90;
            num = Mathf.Abs(bigger90 - rot);
        }

        //if (num > 360)
        //{
        //    num -= 360.0f;
        //}
        GameObject gO = wheelManager.GetAtAngle(num);
        winnerText.SetActive(true);
        winnerText.GetComponent<TextMeshProUGUI>().SetText("Winner: " + gO.name);
        //Debug.Log("Difference: " + num);
        //Debug.Log(rot);
        //Debug.Log(gO);
    }

    void ResetWheel()
    {
        ZRot = 0;
        bIsSpun = false;
        Quaternion quat = new Quaternion(0, 0, 0, 0);
        transform.SetPositionAndRotation(pos, quat);
    }

    private void OnDestroy()
    {
        fieldManager.PositiveIncrement -= ResetWheel;
        fieldManager.NegativeIncrement -= ResetWheel;
    }
}

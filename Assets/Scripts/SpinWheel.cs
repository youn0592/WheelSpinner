using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.Net.Http.Headers;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{

    public GameObject winnerText;

    WheelManager wheelManager;
    InputFieldManager fieldManager;

    float ZRot;
    [SerializeField]
    float minSpeed, maxSpeed, damping;


    float spinSpeed;

    Vector3 pos;

    Coroutine spinCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        wheelManager = GetComponent<WheelManager>();
        fieldManager = GameObject.Find("Prompts").GetComponent<InputFieldManager>();
        if (wheelManager == null || fieldManager == null)
        {
            Debug.LogError("NULL");
        }
        fieldManager.PositiveIncrement += ResetWheel;
        fieldManager.NegativeIncrement += ResetWheel;

        pos = transform.position;
        winnerText.SetActive(false);

    }

    public void Spin()
    {
        ZRot = 0;
        spinSpeed = Random.Range(minSpeed, maxSpeed);
        StopCoroutine(WheelCoroutine());
        spinCoroutine = StartCoroutine(WheelCoroutine());
        //bIsSpun = true;
    }

    void Win()
    {
        spinSpeed = 0;
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
        winnerText.GetComponent<TextMeshProUGUI>().SetText(gO.name + " Wins");
        //Debug.Log("Difference: " + num);
        //Debug.Log(rot);
        //Debug.Log(gO);
    }

    void ResetWheel()
    {
        ZRot = 0;
        Quaternion quat = new Quaternion(0, 0, 0, 0);
        transform.SetPositionAndRotation(pos, quat);
    }

    private void OnDestroy()
    {
        fieldManager.PositiveIncrement -= ResetWheel;
        fieldManager.NegativeIncrement -= ResetWheel;
    }

    IEnumerator WheelCoroutine()
    {
        while (true)
        {
            winnerText.SetActive(false);
            ZRot = spinSpeed * Time.deltaTime;
            spinSpeed -= damping * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, ZRot));

            if (spinSpeed <= 0)
            {
                Win();
                yield break;
            }


            yield return null;

        }
    }
}

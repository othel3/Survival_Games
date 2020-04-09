using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour {

    [SerializeField] private float secondPerRealTimeSecond; // 게임 세계의 100초 = 현실세계의 1초
    [SerializeField] private float fogDensityCalc; //증감량 비율
    [SerializeField] private float nightFogDestiny; // 밤 상태의 Fog 밀도
    private float dayFogDesity; //낮 상태의 fog 밀도
    private float currentFogDensity; //계산
    
	// Use this for initialization
	void Start () {
        dayFogDesity = RenderSettings.fogDensity;
	}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        float dotV = Vector3.Dot(transform.forward, Vector3.down);
        if (dotV<0)
            GameManager.isNight = true;
        else
            GameManager.isNight = false;

        Debug.Log(transform.eulerAngles.x);
        Debug.Log(currentFogDensity);

        if (GameManager.isNight)
        {
            if (currentFogDensity <= nightFogDestiny)
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
            else if (currentFogDensity >= dayFogDesity)
            {
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }

        }
        else
        {
            RenderSettings.fogDensity = 0;
        }
    }
}

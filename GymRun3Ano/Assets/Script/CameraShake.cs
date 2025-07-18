using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public float shakeTimer;
    public CinemachineVirtualCamera cameraVar;
    private void Awake()
    {
       // cameraVar = GetComponent<CinemachineVirtualCamera>();
        instance = this;
    }

    public void ShakeCamera(float intensity, float time)
    {
        Cinemachine.CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cameraVar.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                Cinemachine.CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cameraVar.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }


}

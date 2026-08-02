using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    public enum ShakeType
    {
        Light,
        Medium,
        Heavy,
    }

    [SerializeField] AnimationCurve curve;

    static ShakeType currentShakeType = ShakeType.Light;
    static readonly Dictionary<ShakeType, float> typeToStrength = new() {
        { ShakeType.Light, 0.1f },
        { ShakeType.Medium, 0.5f },
        { ShakeType.Heavy, 1.5f }
    };

    static bool shakeStarted = false;
    Coroutine currentCoroutine;


    public static void StartLightShake()
    {
        // Stay or go higher
        if ((int)ShakeType.Light > (int)currentShakeType)
            currentShakeType = ShakeType.Light;

        shakeStarted = true;
    }
    public static void StartMediumShake()
    {
        // Stay or go higher
        if ((int)currentShakeType < (int)ShakeType.Medium)
            currentShakeType = ShakeType.Medium;

        shakeStarted = true;
    }
    public static void StartHeavyShake()
    {
        currentShakeType = ShakeType.Heavy;
        shakeStarted = true;
    }

    private void Update()
    {
        if (shakeStarted)
        {
            shakeStarted = false;
            float str = typeToStrength[currentShakeType];
            Shake(str);
        }
    }

    public void Shake(float t_strength, float t_duration = 1f)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(ShakeCoroutine(t_strength));
    }

    IEnumerator ShakeCoroutine(float t_strength, float t_duration = 1f)
    {
        Vector3 originalPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < t_duration)
        {
            elapsedTime += Time.deltaTime;
            float amount = curve.Evaluate(elapsedTime / t_duration);
            transform.position = originalPos + (Random.insideUnitSphere * (amount * t_strength));

            yield return null;
        }

        transform.position = originalPos;
    }
}

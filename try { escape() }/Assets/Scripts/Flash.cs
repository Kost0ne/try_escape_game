using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Serialization;

[System.Serializable]
public class LightSource
{

    public string name;
    public GameObject gameObject;
    
    public float MinIntensity = 0.3f;
    public float MaxIntensity = 1.5f;
    public float NoiseSpeed = 0.15f;
    
    private Light2D source;
    
    public void SetSource()
    {
        source = gameObject.GetComponent<Light2D>();
    }

    public void SetIntensity(float intensity)
    {
        source.intensity = intensity;
    }

    public void SetRandomIntensity()
    {
        var intensity = Mathf.Lerp(MinIntensity, 
            MaxIntensity, 
            Mathf.PerlinNoise(10, Time.time / NoiseSpeed));
        SetIntensity(intensity);
    }
}

public class Flash : MonoBehaviour
{
    [SerializeField]
    LightSource[] lightSources;

    public bool FlickerON;
    
    public bool RandomTimer;
    public float RandomTimerMIN = 5f;
    public float RandomTimerMAX = 20f;
    public float RandomTimerValue;
    
    public float StartTimerValue = 0.1f;
    
    
    IEnumerator Start()
    {
        foreach (var lightSource in lightSources)
            lightSource.SetSource();
        yield return new WaitForSeconds(StartTimerValue);

        while (RandomTimer)
        {
            RandomTimerValue = Random.Range(RandomTimerMIN, RandomTimerMAX);
            yield return new WaitForSeconds(RandomTimerValue);
            FlickerON = !FlickerON;
        }
    } 
    
    void Update()
    {
        if (!FlickerON) return;
        foreach (var lightSource in lightSources)
            lightSource.SetRandomIntensity();
    }
    
    
}

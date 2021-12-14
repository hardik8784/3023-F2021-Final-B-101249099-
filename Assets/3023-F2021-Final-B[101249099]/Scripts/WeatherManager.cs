using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Header("Weather Management")]
    [SerializeField] private int TickBetweenWeather = 10;
    [SerializeField] private int WeatherQueueSize = 5;
    private int CurrentWeatherTick = 0;
    [SerializeField] private Weather currentWeather = Weather.Sunny;

    public Weather CurrentWeather => currentWeather;
    private Queue<Weather> WeatherQueue;
    [Header("Weather VFX")]
    [SerializeField] ParticleSystem RainParticle;
    [SerializeField] ParticleSystem SnowParticle;
    [SerializeField] ParticleSystem LightningParticle;
    [SerializeField] ParticleSystem CloudParticle;

    public static Action<Weather, Queue<Weather>> OnWeatherChange;


    // Start is called before the first frame update
    void Start()
    {
        RainParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        SnowParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        LightningParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        CloudParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        FillWeatherQueue();
        ChangeWeather();
    }

    private void OnEnable()
    {
        GameManager.OnTick += Tick;
    }

    private void OnDisable()
    {
        GameManager.OnTick -= Tick;
    }

    private void Tick()
    {
        CurrentWeatherTick++;

        if(CurrentWeatherTick >= TickBetweenWeather)
        {
            CurrentWeatherTick = 0;
            ChangeWeather();
        }
    }

    void FillWeatherQueue()
    {
        WeatherQueue = new Queue<Weather>();

        for (int i = 0; i < WeatherQueueSize; i++)
        {
            Weather TempWeather = GetRandomWeather();
            WeatherQueue.Enqueue(TempWeather);
            Debug.Log("Weather is " + TempWeather + " at index : " + i);
        }
    }

    private Weather GetRandomWeather()
    {
        int RandomWeather = 0;

        RandomWeather = UnityEngine.Random.Range(0, (int)Weather.WEATHER_MAX + 1);

        return (Weather)RandomWeather;
    }

    void ChangeWeather()
    {
        currentWeather = WeatherQueue.Dequeue();
        WeatherQueue.Enqueue(GetRandomWeather());

        OnWeatherChange?.Invoke(currentWeather, WeatherQueue);

        switch (currentWeather)
        {
            case Weather.Sunny:
                RainParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                SnowParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                LightningParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                CloudParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                break;
            case Weather.Cloudy:
                RainParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                SnowParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                LightningParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                CloudParticle.Play();
                break;
            case Weather.Rain:
                RainParticle.Play();
                SnowParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                LightningParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                CloudParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                break;
            case Weather.Lightning:
                RainParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                SnowParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                LightningParticle.Play();
                CloudParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                break;
            case Weather.Snow:
                RainParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                SnowParticle.Play();
                LightningParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                CloudParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Weather
{ 
    Sunny = 0,
    Cloudy = 1,
    Rain = 2,
    Lightning=3,
    Snow =4,
    WEATHER_MAX = Snow
}

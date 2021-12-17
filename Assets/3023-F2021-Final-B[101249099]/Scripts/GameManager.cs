/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 14,2021
 * File : GameManager.cs
 * Description : This is Game Manager Script
 * Revision History : v0.1 > Increasing the Time and Updating it
 *              
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float TickFrequency = 1.0f;

    private static int currentTick = 0;

    public static int CurrentTick => currentTick;

    private float LastTickTime = 0;

    [SerializeField]
    private static float currentGameTime;
    public static float CurrentGameTime => currentGameTime;

    public static Action OnTick;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }

    private void Tick()
    {
        currentGameTime += Time.deltaTime;

        if(currentGameTime >= LastTickTime + TickFrequency)
        {
            LastTickTime = currentGameTime;
            OnTick?.Invoke();
            currentTick++;
        }
    }
}

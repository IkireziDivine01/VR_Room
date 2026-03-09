using UnityEngine;
using System;

public class AnalogClock : MonoBehaviour
{
    [Header("Clock Hands")]
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    [Header("Settings")]
    public bool smoothSeconds = true; // Smooth vs ticking second hand

    void Update()
    {
        DateTime now = DateTime.Now;

        float hours        = now.Hour % 12;
        float minutes      = now.Minute;
        float seconds      = now.Second;
        float milliseconds = now.Millisecond;

        // Smooth interpolation within each unit
        float smoothSec  = smoothSeconds ? seconds + milliseconds / 1000f : seconds;
        float smoothMin  = minutes + smoothSec / 60f;
        float smoothHour = hours + smoothMin / 60f;

        // Rotating around Y axis because the clock face is rotated -90 on Y
        secondHand.localRotation = Quaternion.Euler(0f, -smoothSec   * 6f,   0f);  // 360/60 = 6 deg per second
        minuteHand.localRotation = Quaternion.Euler(0f, -smoothMin   * 6f,   0f);  // 360/60 = 6 deg per minute
        hourHand.localRotation   = Quaternion.Euler(0f, -smoothHour  * 30f,  0f);  // 360/12 = 30 deg per hour
    }
}
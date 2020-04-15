using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;

/// <summary>
/// Counting Time until start
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textField;

    string timerFormat = "00.00";

    private void Start()
    {
        setText(0);
    }

    public void StartTimer(float seconds, GenerateButtonField field = null)
    {
        setText(seconds);

        StartCoroutine(DoTiming(seconds, field));
    }


    IEnumerator DoTiming(float seconds, GenerateButtonField field = null)
    {
        do
        {
            yield return new WaitForEndOfFrame();
            seconds -= Time.deltaTime;
                setText(seconds);
        } while (seconds >= 0);

        setText(0);

        field?.DelselectRandomButton();
    }

    /// <summary>
    /// Helperfunction for setting Timertextfield
    /// </summary>
    /// <param name="seconds">Seconds remaining until start</param>
    public void setText(float seconds)
    {
        textField.text = seconds.ToString(timerFormat, CultureInfo.InvariantCulture);
    }
}

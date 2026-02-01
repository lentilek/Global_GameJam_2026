using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskUI : MonoBehaviour
{
    public GameObject inactive, active, number, fillcounter;
    public Image fillImage;
    [HideInInspector] public float amount, max;
    private void Update()
    {
        if (fillcounter.activeSelf)
        {
            amount -= Time.deltaTime;
            GetCurrentFill(amount, max);
        }
    }
    private void GetCurrentFill(float amount, float max)
    {
        float fill = amount / max;
        fillImage.fillAmount = fill;
    }
}

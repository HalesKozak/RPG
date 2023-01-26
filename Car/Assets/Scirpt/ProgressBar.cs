using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExcuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int minimum;
    public int currentHP;
    public int currentMP;
    public Image maskHP;
    public Image maskMP;

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentHPOffset = currentHP - minimum;
        float currentMPOffset = currentMP - minimum;
        float maximumOffset = maximum - minimum;
        float fillHPAmount = currentHPOffset /maximumOffset;
        float fillMPAmount = currentMPOffset / maximumOffset;
        maskHP.fillAmount = fillHPAmount;
        maskMP.fillAmount = fillMPAmount;
    }
}

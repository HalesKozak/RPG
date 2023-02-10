using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //[SerializeField] private StatsPlayer _statsPlayer;
    //private int maximum = 100;
    //private int minimum = 0;
    public Image maskHP;
    public Image maskMP;

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        //float currentHPOffset = _statsPlayer.HP - minimum;
        //float currentMPOffset = _statsPlayer.MP - minimum;
        //float maximumOffset = maximum - minimum;
        //float fillHPAmount = currentHPOffset / maximumOffset;
        //float fillMPAmount = currentMPOffset / maximumOffset;
        //maskHP.fillAmount = fillHPAmount;
        //maskMP.fillAmount = fillMPAmount;
    }
}

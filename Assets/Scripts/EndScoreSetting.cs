using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScoreSetting : MonoBehaviour
{
    public Text endScoreText;
    public void ScoreSetter()
    {
        endScoreText.text = PlayerPrefs.GetString("EndScore");
    }
    public void Start()
    {
        ScoreSetter();
    }
}

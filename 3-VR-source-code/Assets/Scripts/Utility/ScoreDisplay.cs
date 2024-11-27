using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text timeScore;
    public TMP_Text ingredientsScore;
    public TMP_Text ovenScore;
    public TMP_Text cutsScore;

    public void SetTimeScore(string score)
    {
        timeScore.text = score;
    }

    public void SetIngredientsScore(string score)
    {
        ingredientsScore.text = score;
    }

    public void SetOvenScore(string score)
    {
        ovenScore.text = score;
    }

    public void SetCutsScore(string score)
    {
        cutsScore.text = score;
    }
}
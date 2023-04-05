using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * This component increases a given "score" field whenever it is triggered.
 */
public class ScoreAdder : MonoBehaviour
{
    [Tooltip("Every object tagged with this tag will trigger adding score to the score field.")]
    [SerializeField] List<string> triggeringTags;
    [SerializeField] NumberField scoreField;
    [SerializeField] List<int> pointsToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        int index = triggeringTags.IndexOf(other.tag);
        if (index != -1 && scoreField != null)
        {
            scoreField.AddNumber(pointsToAdd.ElementAt(index));
        }
    }

    public void SetScoreField(NumberField newTextField)
    {
        this.scoreField = newTextField;
    }
}

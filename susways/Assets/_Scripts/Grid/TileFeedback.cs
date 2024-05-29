using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFeedback : MonoBehaviour
{
    [SerializeField] Transform _feedbackTransform;

    public void ShowFeedback()
    {
        _feedbackTransform.gameObject.SetActive(true);
    }

    public void HideFeedback()
    {
        _feedbackTransform.gameObject.SetActive(false);
    }
}

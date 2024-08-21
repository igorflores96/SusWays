using UnityEngine;

public class TileFeedback : MonoBehaviour
{
    [SerializeField] Transform _feedbackTransform;
    [SerializeField] Transform _walkFeedback;


    public void ShowFeedback()
    {
        _feedbackTransform.gameObject.SetActive(true);
    }

    public void HideFeedback()
    {
        _feedbackTransform.gameObject.SetActive(false);
    }

    public void ShowWalkFeedback()
    {
        _walkFeedback.gameObject.SetActive(true);
    }

    public void HideWalkFeedback()
    {
        _walkFeedback.gameObject.SetActive(false);
    }
}

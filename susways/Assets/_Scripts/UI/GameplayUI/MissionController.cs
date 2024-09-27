using UnityEngine;

public class MissionController : MonoBehaviour
{
    [SerializeField] private GameObject _missionCanvas;

    public void ActiveCanvas()
    {
        _missionCanvas.SetActive(true);
    }

    public void DesactiveCanvas()
    {
        _missionCanvas.SetActive(false);
    }
}

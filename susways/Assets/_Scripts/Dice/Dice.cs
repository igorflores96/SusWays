using UnityEngine;

public class Dice : MonoBehaviour
{
    public static Dice Instance { get; private set; }
    private bool _isMegaDice;
    public bool IsMegaDice => _isMegaDice;
    private void Awake() 
    {
        Instance = this;
        _isMegaDice = false;
        
    }

    private void OnEnable() 
    {
        EventManager.OnPlayerNeedMegaDice += ActiveMegaDice;
    }

    private void OnDisable() 
    {
        EventManager.OnPlayerNeedMegaDice -= ActiveMegaDice;
    }

    public static int RollSixDice()
    {
        if(Instance == null) Debug.LogError("Dice don't exist.");

        int result = Instance._isMegaDice ? 60 : Instance.RollSixDice_Instance();
        return result;
    }

    public static void ActiveMegaDice()
    {
        if(Instance == null) Debug.LogError("Dice don't exist.");

        Instance._isMegaDice = true;
    }

    private int RollSixDice_Instance()
    {
        return Random.Range(1, 7);
    }

}

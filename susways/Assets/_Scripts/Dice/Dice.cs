using UnityEngine;

public class Dice : MonoBehaviour
{
    public static Dice Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public static int RollSixDice()
    {
        if(Instance == null) Debug.LogError("Dice don't exist.");

        int result = Instance.RollSixDice_Instance();
        return result;
    }

    private int RollSixDice_Instance()
    {
        return Random.Range(1, 7);
    }

}

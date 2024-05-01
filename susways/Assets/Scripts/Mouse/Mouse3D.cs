using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance { get; private set; }

    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

    private void Awake() {
        Instance = this;
        Debug.Log("MOuse instanciado." + Instance);
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            transform.position = raycastHit.point;
        }
    }

    public static Vector3 GetMouseWorldPosition() {
        if (Instance == null) {
            Debug.LogError("Mouse3D Object does not exist!");
        }

        //Vector3 result = EventSystem.current.IsPointerOverGameObject() ?  Vector3.zero : Instance.GetMouseWorldPosition_Instance();
        Vector3 result = Instance.GetMouseWorldPosition_Instance();
        return result;
    }

    private Vector3 GetMouseWorldPosition_Instance() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            return raycastHit.point;
        } else {
            return Vector3.zero;
        }
    }

}

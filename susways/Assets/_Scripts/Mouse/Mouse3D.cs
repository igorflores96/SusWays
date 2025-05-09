using UnityEngine;

public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance { get; private set; }

    [SerializeField] private LayerMask floorColliderLayerMask = new LayerMask();
    [SerializeField] private LayerMask playerColliderLayerMask = new LayerMask();
    [SerializeField] private LayerMask feedbackColliderLayerMask = new LayerMask();



    private void Awake() {
        Instance = this;
    }

    /*private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            transform.position = raycastHit.point;
        }
    }*/

    public static Vector3 GetMouseWorldPosition() {
        if (Instance == null) {
            Debug.LogError("Mouse3D Object does not exist!");
        }

        //Vector3 result = EventSystem.current.IsPointerOverGameObject() ?  Vector3.zero : Instance.GetMouseWorldPosition_Instance();
        Vector3 result = Instance.GetMouseWorldPosition_Instance();
        return result;
    }

    public static GameObject GetPlayer() {
        if (Instance == null) {
            Debug.LogError("Mouse3D Object does not exist!");
        }

        //Vector3 result = EventSystem.current.IsPointerOverGameObject() ?  Vector3.zero : Instance.GetMouseWorldPosition_Instance();
        GameObject player = Instance.GetPlayer_Instance();
        return player;
    }

    public static BuildingInfo GetBuilding() {
        if (Instance == null) {
            Debug.LogError("Mouse3D Object does not exist!");
        }

        //BuildingInfo result = EventSystem.current.IsPointerOverGameObject() ?  null : Instance.GetFeedback_Instance();
        BuildingInfo feedback = Instance.GetFeedback_Instance();
        return feedback;
    }

    private Vector3 GetMouseWorldPosition_Instance() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, floorColliderLayerMask)) {
            return raycastHit.point;
        } else {
            return Vector3.zero;
        }
    }

    private GameObject GetPlayer_Instance() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, playerColliderLayerMask)) {
            return raycastHit.transform.gameObject;
        } else {
            return null;
        }
    }

        private BuildingInfo GetFeedback_Instance() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, feedbackColliderLayerMask)) {
            raycastHit.transform.gameObject.TryGetComponent<BuildingInfo>(out BuildingInfo feedback);
            return feedback;
        } else {
            return null;
        }
    }
}

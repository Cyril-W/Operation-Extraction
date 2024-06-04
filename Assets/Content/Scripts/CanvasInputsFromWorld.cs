using UnityEngine;
using UnityEngine.UI;

public class CanvasInputsFromWorld : MonoBehaviour {
    public static CanvasInputsFromWorld Instance { get; private set; }

    [SerializeField] Transform target;
    [SerializeField] RectTransform self;
    [SerializeField] Image image;

    void Start() {
        Instance = this;
    }

    public void RegisterTarget(Transform newTarget) {
        target = newTarget;        
    }

    void Update() {
        if (!image) { return; }
        image.gameObject.SetActive(target != null);
        if (!target || !self || !Camera.main) { return; }
        self.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
    }
}

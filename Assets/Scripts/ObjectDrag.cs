using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void OnMouseDown()
    {
        Debug.Log(gameObject.name + " tıklandı!"); // Hangi nesnenin tıklandığını gösterir
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        Debug.Log(gameObject.name + " hareket ettiriliyor!"); // Hangi nesnenin hareket ettiğini kontrol et
        transform.position = GetMouseWorldPos() + offset;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
    

}
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private Rigidbody rb;

    public int pairID; // Eşleşme kimliği
    private bool isPlaced = false; // Yerleştirme durumu

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Başlangıçta fizik motorunu devre dışı bırak
    }

    void OnMouseDown()
    {
        if (isPlaced) return; // Yerleştirildiyse hareket ettirme
        rb.isKinematic = true; // Fizik motorunu devre dışı bırak
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (isPlaced) return; // Yerleştirildiyse hareket ettirme
        Vector3 targetPosition = GetMouseWorldPos() + offset;
        targetPosition.y = 0.5f; // Zemine sabit yükseklik
        transform.position = targetPosition;
    }

    void OnMouseUp()
    {
        if (isPlaced) return; // Yerleştirildiyse işlem yapma
        rb.isKinematic = false; // Fizik motorunu tekrar etkinleştir
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z; // Z mesafesi
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    public void PlaceObject()
    {
        isPlaced = true; // Yerleştirildi olarak işaretle
        rb.isKinematic = true; // Fizik motorunu sabitle
    }
}



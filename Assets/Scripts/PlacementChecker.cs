using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    private GameObject placedObject = null;
    private Renderer placementAreaRenderer;

    public GameManager gameManager; // Skoru yönetmek için GameManager referansı

    void Start()
    {
        placementAreaRenderer = GetComponent<Renderer>();
        if (placementAreaRenderer != null)
        {
            placementAreaRenderer.material.color = Color.red; // Başlangıçta boş (kırmızı)
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Draggable")) return;

        ObjectDrag draggable = other.GetComponent<ObjectDrag>();

        if (placedObject == null) // Placement alanı boşsa
        {
            placedObject = other.gameObject;
            draggable.PlaceObject();
            placementAreaRenderer.material.color = Color.green; // Alanı dolu göster
        }
        else
        {
            ObjectDrag placedDraggable = placedObject.GetComponent<ObjectDrag>();

            if (draggable.pairID == placedDraggable.pairID) // Eşleşme kontrolü
            {
                Debug.Log("Eşleşme başarılı!");
                gameManager.IncreaseScore(); // Skoru artır
                Destroy(placedObject); // Eski nesneyi yok et
                Destroy(other.gameObject); // Yeni nesneyi yok et
                placedObject = null;
                placementAreaRenderer.material.color = Color.red; // Alanı tekrar boş göster
            }
            else
            {
                Debug.Log("Eşleşme başarısız!");
                RejectObject(other.gameObject); // Yanlış nesneyi geri it
            }
        }
    }

    private void RejectObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(new Vector3(3f, 3f, 0f), ForceMode.Impulse); // Yanlış nesneyi geri it
        }
    }
}




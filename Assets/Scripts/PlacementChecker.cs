using UnityEngine;
using UnityEngine.UI;


public class PlacementChecker : MonoBehaviour
{
    private GameObject placedObject = null; // Yerleştirilen nesneyi takip eder
    private Renderer placementAreaRenderer; // Yerleştirme alanının görseli için Renderer

    void Start()
    {
        // Placement Area'nın Renderer bileşenini al
        placementAreaRenderer = GetComponent<Renderer>();
        if (placementAreaRenderer != null)
        {
            placementAreaRenderer.material.color = Color.red; // Başlangıçta kırmızı (boş)
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter tetiklendi: " + other.name);

        // Eğer yerleştirme alanı boşsa ve nesne "Draggable" tag'ine sahipse
        if (placedObject == null && other.CompareTag("Draggable"))
        {
            placedObject = other.gameObject; // Yerleştirilen nesneyi takip etmeye başla
            Debug.Log(placedObject.name + " yerleştirildi!");

            // Alanı dolu olarak göster (yeşil renk)
            if (placementAreaRenderer != null)
            {
                placementAreaRenderer.material.color = Color.green;
            }
        }
        else if (placedObject != null && other.CompareTag("Draggable"))
        {
            Debug.Log("Yerleştirme alanı dolu! " + other.name + " yerleştirilemez.");
            RejectObject(other.gameObject); // Fazladan nesneyi reddet
        }
       

       



    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit tetiklendi: " + other.name);

        // Eğer çıkış yapan nesne yerleştirilen nesne ise, alanı tekrar boşalt
        if (placedObject == other.gameObject)
        {
            placedObject = null;
            Debug.Log(other.name + " alanı terk etti!");

            // Alanı boş olarak göster (kırmızı renk)
            if (placementAreaRenderer != null)
            {
                placementAreaRenderer.material.color = Color.red;
            }
        }
        
    }

    void RejectObject(GameObject obj)
    {
        // Fazla nesneyi fiziksel olarak dışarı it veya pozisyonunu değiştir
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            
            rb.angularVelocity = Vector3.zero; // Dönme hareketini sıfırla
            rb.AddForce(new Vector3(5f, 5f, 0f), ForceMode.Impulse); // Fiziksel kuvvet uygula
        }
        else
        {
            // Rigidbody yoksa pozisyonu zorla değiştir
            obj.transform.position = new Vector3(obj.transform.position.x + 3f, obj.transform.position.y + 3f, obj.transform.position.z);
        }

    

    }
   
  


}

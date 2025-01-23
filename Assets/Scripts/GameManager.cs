using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0; // Skor
    public Text scoreText; // Skor UI
    public GameObject[] objectPrefabs; // Çift nesneler için Prefabler
    public Transform spawnArea; // Nesnelerin doğacağı alan
    public int objectCount = 12; // Toplam doğacak nesne sayısı (örneğin 6 çift)

    void Start()
    {
        UpdateScoreUI(); // Skoru güncelle
        SpawnObjects(); // Nesneleri sahneye yerleştir
    }

    public void IncreaseScore()
    {
        score += 10; // Skoru artır
        UpdateScoreUI(); // UI'yi güncelle
    }

    public void ResetGame()
    {
        score = 0; // Skoru sıfırla
        UpdateScoreUI();

        // Sahnedeki tüm Draggable nesneleri sil
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Draggable"))
        {
            Destroy(obj);
        }

        // Yeni nesneleri oluştur
        SpawnObjects();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < objectCount / 2; i++) // Çiftler oluştur
        {
            // Rastgele pozisyonlarda iki nesne oluştur
            GameObject obj1 = Instantiate(objectPrefabs[i], GetRandomPosition(), Quaternion.identity);
            GameObject obj2 = Instantiate(objectPrefabs[i], GetRandomPosition(), Quaternion.identity);

            // Çiftler için aynı pairID değerini atayın
            obj1.GetComponent<ObjectDrag>().pairID = i;
            obj2.GetComponent<ObjectDrag>().pairID = i;
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnArea.position.x - 5, spawnArea.position.x + 5);
        float z = Random.Range(spawnArea.position.z - 5, spawnArea.position.z + 5);
        return new Vector3(x, 0.5f, z); // Yükseklik zemin seviyesinde sabitlenir
    }
}





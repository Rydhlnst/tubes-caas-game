using UnityEngine;
using System.Collections.Generic; // Dibutuhkan untuk menggunakan List

public class PlayerController : MonoBehaviour
{
    public float laneSpeed = 10f;
    public float xLimit = 5f;

    [Header("Crowd Settings")]
    public GameObject soldierPrefab; // Masukkan Prefab Soldier (biru) ke sini
    public int currentArmyCount = 1; 
    
    // List untuk menyimpan semua prajurit yang sudah muncul
    private List<GameObject> armyList = new List<GameObject>();

    void Start()
    {
        // Masukkan karakter utama ke dalam list sebagai anggota pertama
        armyList.Add(this.gameObject);
    }

    void Update()
    {
        // Kontrol A & D (Sama seperti sebelumnya)
        float horizontalInput = 0;
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1;
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1;

        Vector3 newPosition = transform.position + new Vector3(horizontalInput, 0, 0) * laneSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);
        transform.position = newPosition;
    }

    public void MultiplyPower(int multiplier)
    {
        // 1. Hitung berapa jumlah prajurit baru yang harus dibuat
        int amountToAdd = (currentArmyCount * multiplier) - currentArmyCount;
        currentArmyCount *= multiplier;

        // 2. Munculkan prajurit baru
        for (int i = 0; i < amountToAdd; i++)
        {
            SpawnNewSoldier();
        }
    }

    void SpawnNewSoldier()
    {
        // Munculkan prajurit di posisi pemain saat ini
        // Beri sedikit posisi acak agar tidak menumpuk di satu titik (Random.insideUnitSphere)
        Vector3 randomPos = transform.position + (Random.insideUnitSphere * 1.5f);
        randomPos.y = transform.position.y; // Tetap di atas lantai

        GameObject newSoldier = Instantiate(soldierPrefab, randomPos, Quaternion.identity);
        
        // JADIKAN ANAK (Parenting) agar prajurit baru ikut bergerak saat Player bergerak
        newSoldier.transform.SetParent(this.transform);
        
        armyList.Add(newSoldier);
    }
}
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Pergerakan Player Utama")]
    public float laneSpeed = 10f;
    public float xLimit = 8f; // Lebar jalan total (sesuaikan dengan floor kamu)

    [Header("Pengaturan Kerumunan Masif")]
    public GameObject soldierPrefab;
    public int currentArmyCount = 1;
    public float smoothSpeed = 5f; // Sedikit diturunkan agar efek "kenyal" kerumunan lebih terasa
    
    // VARIABEL KUNCI: Seberapa lebar pasukan boleh menyebar dari posisi Player
    [Range(1f, 10f)]
    public float maxCrowdSpread = 6f; 

    private List<GameObject> armyList = new List<GameObject>();
    private List<float> armyRelativeX = new List<float>(); // Hanya simpan offset X (Kiri/Kanan)
    private List<float> armyRelativeZ = new List<float>(); // Hanya simpan offset Z (Depan/Belakang)

    void Update()
    {
        // 1. Gerakkan Player Utama (Instan mengikuti Input A/D)
        MoverPlayer();

        // 2. Gerakkan Pasukan (Mengikuti secara halus dengan batasan jalan)
        MoveMassiveCrowd();
    }

    void MoverPlayer()
    {
        float horizontalInput = 0;
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1;
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1;

        Vector3 newPos = transform.position + new Vector3(horizontalInput, 0, 0) * laneSpeed * Time.deltaTime;
        
        // Player utama dibatasi agar tidak terlalu pinggir
        newPos.x = Mathf.Clamp(newPos.x, -xLimit, xLimit);
        transform.position = newPos;
    }

    void MoveMassiveCrowd()
    {
        for (int i = 0; i < armyList.Count; i++)
        {
            if (armyList[i] != null)
            {
                // A. Tentukan Posisi Tujuan Global prajurit tersebut
                float desiredX = transform.position.x + armyRelativeX[i];
                float desiredZ = transform.position.z + armyRelativeZ[i];
                
                // B. LOGIKA KUNCI: Batasi Posisi Tujuan agar selalu di dalam jalan
                // Kita beri sedikit padding (0.5f) agar collider pasukan tidak tembus dinding
                float crowdPadding = 0.5f;
                desiredX = Mathf.Clamp(desiredX, -(xLimit + crowdPadding), (xLimit + crowdPadding));

                // C. Buat posisi Vector3 tujuan yang sudah aman
                Vector3 safeTargetPos = new Vector3(desiredX, transform.position.y, desiredZ);
                
                // D. Gerakkan secara halus dengan Lerp (seperti pegas)
                armyList[i].transform.position = Vector3.Lerp(
                    armyList[i].transform.position, 
                    safeTargetPos, 
                    smoothSpeed * Time.deltaTime
                );
            }
        }
    }

    public void MultiplyPower(int multiplier)
    {
        int amountToAdd = (currentArmyCount * multiplier) - currentArmyCount;
        currentArmyCount *= multiplier;

        for (int i = 0; i < amountToAdd; i++)
        {
            SpawnNewSoldier();
        }
    }

    void SpawnNewSoldier()
    {
        // BARU: Menentukan posisi acak di dalam PERSEGI (Kotak)
        float randomX = Random.Range(-maxCrowdSpread, maxCrowdSpread);
        float randomZ = Random.Range(-maxCrowdSpread, maxCrowdSpread);

        // Posisi lahir relatif terhadap Player
        Vector3 spawnPos = transform.position + new Vector3(randomX, 0, randomZ);

        GameObject newSoldier = Instantiate(soldierPrefab, spawnPos, Quaternion.identity);
        
        // Simpan offset (posisi relatif) secara terpisah
        armyList.Add(newSoldier);
        armyRelativeX.Add(randomX); // Simpan X acak
        armyRelativeZ.Add(randomZ); // Simpan Z acak
    }
}
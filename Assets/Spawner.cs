using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] daftarGatePrefab; 
    
    [Header("Pengaturan Waktu")]
    public float minInterval = 1f;  // Detik paling cepat
    public float maxInterval = 15f; // Detik paling lama
    
    private float timer;
    private float nextSpawnTime;

    void Start()
    {
        // Tentukan waktu spawn pertama kali saat game dimulai
        SetRandomNextSpawn();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSpawnTime)
        {
            SpawnRandomGate();
            
            // Setelah spawn, reset timer dan tentukan waktu acak baru
            timer = 0;
            SetRandomNextSpawn();
        }
    }

    void SetRandomNextSpawn()
    {
        // Menghasilkan angka acak antara 1 sampai 15 (sesuai input di Inspector)
        nextSpawnTime = Random.Range(minInterval, maxInterval);
    }

    void SpawnRandomGate()
    {
        if (daftarGatePrefab.Length > 0)
        {
            int indexAcak = Random.Range(0, daftarGatePrefab.Length);
            Instantiate(daftarGatePrefab[indexAcak], transform.position, Quaternion.identity);
        }
    }
}
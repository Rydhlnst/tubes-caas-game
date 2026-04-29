using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Menggunakan [] berarti kita membuat daftar (Array) untuk banyak objek
    public GameObject[] daftarGatePrefab; 
    
    public float spawnInterval = 3f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnRandomGate();
            timer = 0;
        }
    }

    void SpawnRandomGate()
    {
        if (daftarGatePrefab.Length > 0)
        {
            // Logika memilih angka acak antara 0 sampai jumlah isi daftar
            int indexAcak = Random.Range(0, daftarGatePrefab.Length);
            
            // Munculkan blok yang terpilih secara acak
            Instantiate(daftarGatePrefab[indexAcak], transform.position, Quaternion.identity);
        }
    }
}
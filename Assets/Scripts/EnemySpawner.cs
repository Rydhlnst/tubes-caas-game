using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnInterval = 2f; 

    void Start()
    {
        // Memanggil fungsi Spawn setiap interval
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Menggunakan posisi objek Spawner ini sebagai titik lahir
        // Posisi X akan diacak otomatis oleh script EnemyBehavior nanti
        Vector3 spawnPos = transform.position; 
        
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
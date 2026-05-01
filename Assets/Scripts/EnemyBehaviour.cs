using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 12f; // Kecepatan musuh mendekati pemain

    [Header("RNG Spawn Settings")]
    public float xRange = 25f; // Batas acak jalan (antara -25 sampai 25)

    [Header("Combat Settings")]
    public int health = 1; // Sekali tembak mati

    void Start()
    {
        // 1. RNG hanya untuk menentukan posisi awal saat spawn
        float randomX = Random.Range(-xRange, xRange);
        
        // 2. Set posisi musuh di jalur yang diacak, 
        // tetap mempertahankan tinggi (Y) dan posisi depan (Z) dari spawner
        transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // 3. Musuh bergerak lurus ke arah Z negatif (mendekati posisi 0 atau pemain)
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // 4. Optimasi Memori: Hancurkan jika sudah melewati batas pemain
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }

    // FUNGSI KUNCI: Akan dipanggil oleh script peluru nanti
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Tambahkan efek partikel atau suara di sini jika perlu
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Deteksi jika musuh menabrak objek dengan Tag "Player"
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            
            if (player != null)
            {
                // Gunakan fungsi ExecuteGateLogic yang sudah kita buat sebelumnya
                // Musuh bertindak seperti gerbang "Kurang" (mengurangi pasukan)
                player.ExecuteGateLogic(GateLogic.GateType.Kurang, 1);
                
                // Musuh hancur setelah menabrak player/pasukan
                Die();
            }
        }
    }
}
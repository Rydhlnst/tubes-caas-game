using UnityEngine;

public class GateLogic : MonoBehaviour
{
    [Header("Pergerakan")]
    public float moveSpeed = 10f; // Kecepatan box mendekat
    public float destroyBoundary = -15f; // Batas untuk hapus objek agar tidak numpuk di RAM

    [Header("Logika Gate")]
    public int multiplierValue = 2; // Nilai pengali (misal x2)
    private bool sudahDiklaim = false; // Pengunci agar tidak kena berkali-kali

    void Update()
    {
        // 1. Membuat box bergerak maju melewati pemain (sumbu Z negatif)
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // 2. Jika box sudah jauh di belakang pemain, hapus dari game
        if (transform.position.z < destroyBoundary)
        {
            Destroy(gameObject);
        }
    }

    // 3. Logika saat bersentuhan dengan pemain
    private void OnTriggerEnter(Collider other)
    {
        // Pastikan Player punya Tag "Player"
        if (other.CompareTag("Player") && !sudahDiklaim)
        {
            sudahDiklaim = true; // Kunci biar cuma sekali klaim
            
            Debug.Log("Box Berhasil Diklaim! Efek: x" + multiplierValue);
            
            // Di sini nanti kita bisa tambahkan kode untuk menambah jumlah peluru
            // Untuk sekarang, kita beri warna berbeda agar terlihat sudah diklaim
            GetComponent<Renderer>().material.color = Color.green; 
        }
    }
}

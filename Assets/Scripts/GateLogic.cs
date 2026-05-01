using UnityEngine;

public class GateLogic : MonoBehaviour
{
    public enum GateType { Tambah, Kurang, Kali, Bagi }
    
    [Header("RNG Settings")]
    public GateType tipeGerbang;
    public int nilaiOperasi;

    [Header("Movement")]
    public float moveSpeed = 10f;
    private bool sudahDiklaim = false;

    // Komponen untuk memastikan visual tetap terang
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            // Tetap paksa pakai Shader Unlit agar tidak gelap
            // Tapi kita tidak akan menyentuh bagian .color-nya
            spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }

        SetupGateRNG();
}

    void SetupGateRNG()
    {
        // Acak tipenya (0=Tambah, 1=Kurang, 2=Kali, 3=Bagi)
        tipeGerbang = (GateType)Random.Range(0, 4);

        // Tentukan nilainya saja, TANPA mengubah warna (Gambling)
        switch (tipeGerbang)
        {
            case GateType.Tambah:
                nilaiOperasi = Random.Range(1, 51);
                break;
            case GateType.Kurang:
                nilaiOperasi = Random.Range(1, 51);
                break;
            case GateType.Kali:
                nilaiOperasi = Random.Range(2, 5);
                break;
            case GateType.Bagi:
                nilaiOperasi = Random.Range(2, 4);
                break;
        }
    }

    void Update()
    {
        // Balok bergerak mendekati player
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Hancurkan jika sudah lewat belakang player
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Deteksi tabrakan dengan Player
        if (other.CompareTag("Player") && !sudahDiklaim)
        {
            // Cari script PlayerController di parent atau objek itu sendiri
            PlayerController playerScript = other.GetComponentInParent<PlayerController>();

            if (playerScript != null)
            {
                sudahDiklaim = true;
                
                // Jalankan logika matematika ke player
                playerScript.ExecuteGateLogic(tipeGerbang, nilaiOperasi);
                
                // Hilangkan balok setelah ditabrak
                gameObject.SetActive(false);
            }
        }
    }
}
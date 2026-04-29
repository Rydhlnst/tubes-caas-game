using UnityEngine;

public class GateLogic : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int multiplierValue = 2; // Nilai pengali
    private bool sudahDiklaim = false;

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        if (transform.position.z < -15f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other){
        // 1. Cek Tag
        if (other.CompareTag("Player") && !sudahDiklaim)
        {
            // 2. Ambil script PlayerController dari objek yang menabrak
            PlayerController playerScript = other.GetComponentInParent<PlayerController>();

            if (playerScript != null)
            {
                sudahDiklaim = true;
                // 3. PANGGIL fungsinya
                playerScript.MultiplyPower(multiplierValue); 
                
                GetComponent<Renderer>().material.color = Color.green;
            }
            else 
            {
                // Jika muncul ini di Console, berarti script PlayerController 
                // tidak terpasang di objek yang punya Tag "Player"
                Debug.LogError("Script PlayerController tidak ditemukan pada objek Player!");
            }
        }
    }
}
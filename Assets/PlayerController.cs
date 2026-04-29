using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneSpeed = 10f; // Kecepatan geser pemain
    public float xLimit = 5f;    // Batas agar pemain tidak keluar dari jalan (kiri/kanan)

    void Update()
    {
        // 1. Ambil Input dari Keyboard
        float horizontalInput = 0;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1; // Bergerak ke Kiri
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;  // Bergerak ke Kanan
        }

        // 2. Hitung posisi baru
        Vector3 newPosition = transform.position + new Vector3(horizontalInput, 0, 0) * laneSpeed * Time.deltaTime;

        // 3. Batasi posisi agar tidak keluar jalan (Clamp)
        // Ini seperti membatasi tegangan dalam sirkuit agar tidak overload
        newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);

        // 4. Terapkan posisi baru ke objek pemain
        transform.position = newPosition;

    
    }
}
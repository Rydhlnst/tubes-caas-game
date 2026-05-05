using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;
    private float timer;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        // safety check
        if (player == null || bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("REFERENCE HILANG! Cek Inspector!");
            return;
        }

        // follow X
        transform.position = new Vector3(
            player.position.x,
            fixedY,
            fixedZ
        );

        // shooting
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        BulletBehaviour bb = bullet.GetComponent<BulletBehaviour>();

        if (bb != null)
        {
            bb.direction = Vector3.forward;
        }
    }
}
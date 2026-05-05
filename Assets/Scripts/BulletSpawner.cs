using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 0.8f;
    [SerializeField] private Vector3 shootDirection = Vector3.forward;

    private float timer;

    private void Awake()
    {
        if (firePoint == null)
        {
            Transform fp = transform.Find("FirePoint");

            if (fp != null)
            {
                firePoint = fp;
            }
            else
            {
                Debug.LogWarning("FirePoint child tidak ditemukan!", this);
            }
        }

        if (bulletPrefab == null)
        {
            bulletPrefab = Resources.Load<GameObject>("Bulletkuy");

            if (bulletPrefab == null)
            {
                Debug.LogWarning("Bullet prefab tidak ada di folder Resources!", this);
            }
        }
    }

    private void Update()
    {
        if (bulletPrefab == null || firePoint == null) return;

        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject bulletObject = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        BulletBehaviour bullet = bulletObject.GetComponent<BulletBehaviour>();

        if (bullet != null)
        {
            bullet.direction = shootDirection.normalized;
        }
    }
}
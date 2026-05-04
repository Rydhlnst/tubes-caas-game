using UnityEngine;
//public Transform player; // Russian_Soldier1


public class BulletSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Shooting")]
    public float fireRate = 0.2f;
    private float timer;

    [Header("Bullet Config")]
    public float bulletSpeed = 20f;
    public Vector3 shootDirection = Vector3.back;

    [Header("Visual")]
    public Color bulletColor = Color.red;

    void Update()
    {
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
            bb.direction = shootDirection.normalized;
            bb.speed = bulletSpeed;
            bb.bulletColor = bulletColor;
        }
    }

    //void LateUpdate()
    //{
    //    if (player == null) return;

    //    Vector3 pos = transform.position;

    //    pos.x = player.position.x; // follow X player
    //                               // pos.y tetap
    //                               // pos.z tetap

    //    transform.position = pos;
    //}
}
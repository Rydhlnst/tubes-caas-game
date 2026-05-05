using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20f;
    public Vector3 direction = Vector3.forward; // Z+

    [Header("Visual")]
    public Color bulletColor = Color.red;

    private Renderer rend;
    private MaterialPropertyBlock mpb;

    void Start()
    {
        // ambil renderer dari child (bullet_2)
        rend = GetComponentInChildren<Renderer>();

        if (rend != null)
        {
            mpb = new MaterialPropertyBlock();
            rend.GetPropertyBlock(mpb);
            mpb.SetColor("_White", bulletColor); // sesuai shader kamu
            rend.SetPropertyBlock(mpb);
        }

        Destroy(gameObject, 3f); // auto destroy
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
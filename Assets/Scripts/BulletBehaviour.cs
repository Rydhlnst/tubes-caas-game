using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20f;
    public Vector3 direction = Vector3.forward;

    [Header("Visual")]
    public Color bulletColor = Color.red; // warna kontras default

    Renderer rend;
    MaterialPropertyBlock mpb;

    void Start()
    {
        // ambil renderer dari child (bullet_2)
        rend = GetComponentInChildren<Renderer>();

        // pakai MaterialPropertyBlock (biar ringan)
        mpb = new MaterialPropertyBlock();

        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_White", bulletColor);
        rend.SetPropertyBlock(mpb);

        // auto destroy
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("KENA: " + other.name);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("KENA MUSUH!");
            Destroy(gameObject);
        }
    }

}
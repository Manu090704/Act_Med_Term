using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;

    void Start()
    {
        // La bala se crea → registramos
        BulletHellGameManager.singleton.RegisterBullet();
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Aquí destruyes la bala si choca
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // La bala se destruye → desregistramos
        if (BulletHellGameManager.singleton != null)
            BulletHellGameManager.singleton.UnregisterBullet();
    }
}

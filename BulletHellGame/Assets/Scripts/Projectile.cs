using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;

    void Start()
    {
        // La bala se crea y se registra
        BulletHellGameManager.singleton.RegisterBullet();
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Aquí se destruye la bala si choca
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // cuando la bala se destruye se quita del contador
        if (BulletHellGameManager.singleton != null)
            BulletHellGameManager.singleton.UnregisterBullet();
    }
}

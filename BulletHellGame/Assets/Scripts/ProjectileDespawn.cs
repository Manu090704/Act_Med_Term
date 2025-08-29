    using UnityEngine;

    public class ProjectileDespawn : MonoBehaviour
    {
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Projectile")) // detecta cualquier bala
    {
        Destroy(collision.gameObject);
        BulletHellGameManager.singleton.UnregisterBullet(); // actualiza contador
    }
}

    }

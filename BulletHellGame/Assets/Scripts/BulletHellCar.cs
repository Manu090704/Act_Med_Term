using UnityEngine;

public class BulletHellCar : MonoBehaviour
{
    public ShipType shipType;
    protected float health;
    protected Vector2 acceleration;
    protected float projectileCooldown;
    protected float TimeLastShot;
    protected float projectileSpeed;
    protected float projectileDamage;


    public GameObject projectilePrefab;
    public Rigidbody2D rb;

    public enum Direction
    {
        Up,
        Down
    }
    [HideInInspector]
    public Direction direction = Direction.Up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        IntialiseShip();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    private void IntialiseShip()
    {
        ShipData data = BulletHellGameManager.shipDataDictionary[shipType];
        acceleration = data.acceleration;
        projectileSpeed = data.projectileSpeed;
        projectileCooldown = data.projectileCooldown;
        projectileDamage = data.projectileDamage;
        health = data.health;
    }

    protected virtual void TakeInput()
    {

    }

    protected virtual void Shoot()
    {

    }

    protected void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }
}

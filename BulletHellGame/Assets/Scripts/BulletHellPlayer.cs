using UnityEngine;

public class BulletHellPlayer : BulletHellCar
{
    private Vector2 moveInput;
    public float moveSpeed;

    public float normalSpeed = 5f;
    public float slowSpeed = 2f;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed; // Corregido
    }

    protected override void TakeInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        if (Input.GetMouseButton(0)) Shoot();

        moveInput = new Vector2(moveX, moveY).normalized;

        // Shift = movimiento lento
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : normalSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoadTrigger"))
        {
            RoadController.singleton.SpawnRoad();
        }
        else if (other.gameObject.layer == 11) 
        {
            TakeDamage(other.GetComponent<Projectile>().damage); // Example damage value
            Destroy(other.gameObject);
        }
    }

    protected override void Shoot()
    {
        if (Time.time > TimeLastShot + projectileCooldown)
        {
        TimeLastShot = Time.time;
        Vector3 to = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 14));
        GameObject projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
        projectile.transform.rotation = Util.LookAt2D(this.transform.position, to);

        Projectile p = projectile.GetComponent<Projectile>();
        p.speed = projectileSpeed;
        p.damage = projectileDamage; 
        }
    }
}

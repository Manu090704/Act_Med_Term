using UnityEngine;
using System.Collections.Generic;

public class BulletHellEnemy : BulletHellCar
{
    private Vector2 moveInput;
    List<Move> moveSet;
    Move currentMove;
    int moveIndex;
    float moveStartTime;

    public enum ShootPatternType { Direct, Circular, Spiral }

    private ShootPatternType currentPattern = ShootPatternType.Direct;
    private float patternDuration = 10f; 
    private float patternStartTime;


    private bool initialMoveDone = false; // bandera para saber si ya se movió hacia abajo
    private float initialMoveSpeed = 5f; // velocidad inicial hacia abajo
    private float initialMoveDuration = 1f; // tiempo que dura el movimiento inicial
    private float initialMoveStartTime;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        // Inicializar temporizador de movimiento inicial
        initialMoveStartTime = Time.time;
        currentPattern = ShootPatternType.Direct;
        patternStartTime = Time.time;

        // Solo inicializamos moveSet si luego queremos que se mueva más
        moveSet = BulletHellGameManager.enemyMoveSetDictionary[shipType];
        currentMove = moveSet[0];
        moveStartTime = Time.time;
    }

    private void Update()
    {
        DecideMove();
        TakeInput();
    }

    void FixedUpdate()
    {
        if (!initialMoveDone)
        {
            // Movimiento inicial hacia abajo
            rb.linearVelocity = Vector2.down * initialMoveSpeed;

            if (Time.time >= initialMoveStartTime + initialMoveDuration)
            {
                initialMoveDone = true; // ya terminó el movimiento inicial
                rb.linearVelocity = Vector2.zero; // detenerlo
                moveStartTime = Time.time; // reiniciamos timer para futuros movimientos
            }
        }
        else
        {
            // Aquí puedes decidir si quieres que haga otros movimientos o disparos
            rb.linearVelocity = moveInput * acceleration;
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            TakeDamage(collision.GetComponent<Projectile>().damage);
            Destroy(collision.gameObject);
        }
    }

    private void DecideMove()
    {
        if (Time.time > moveStartTime + currentMove.duration)
        {
            moveIndex++;
            if (moveIndex >= moveSet.Count)
            {
                moveIndex = 0;
            }
            currentMove = moveSet[moveIndex];
            moveStartTime = Time.time;
        }
    }

    protected override void TakeInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (currentMove.wichKey == KeyCode.W) moveY = 1f;
        if (currentMove.wichKey == KeyCode.S) moveY = -1f;
        if (currentMove.wichKey == KeyCode.A) moveX = -1f;
        if (currentMove.wichKey == KeyCode.D) moveX = 1f;

        moveInput = new Vector2(moveX, moveY).normalized;
        Shoot();
    }

    protected override void Shoot()
    {
        
    if (Time.time > patternStartTime + patternDuration)
    {
        currentPattern = (ShootPatternType)(((int)currentPattern + 1) % System.Enum.GetValues(typeof(ShootPatternType)).Length);
        patternStartTime = Time.time;
        Debug.Log($"{name} cambió patrón a {currentPattern}");
    }

    if(shipType == ShipType.BasicEnemy2){
        currentPattern = ShootPatternType.Spiral;   
    }

        // --- Disparo según cooldown ---
        if (Time.time > TimeLastShot + projectileCooldown)
        {
            TimeLastShot = Time.time;

            switch (currentPattern)
            {
                case ShootPatternType.Direct: 
                    ShootDirectAtPlayer();
                    break;

                case ShootPatternType.Circular: 
                    ShootCircular(12);
                    break;

                case ShootPatternType.Spiral: 
                    ShootCross();
                    break;
            }
        }
    }


    void ShootDirectAtPlayer()  
    {
        if (GameObject.FindWithTag("Player") == null) return;
        Vector3 to = GameObject.FindWithTag("Player").transform.position;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.rotation = Util.LookAt2D(transform.position, to);
        projectile.layer = 11;
    }

    void ShootCircular(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount);
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            GameObject projectile = Instantiate(projectilePrefab, transform.position, rot);
            projectile.layer = 11;
        }
    }


    void ShootCross()
    {
        // Coordenadas de los ángulos para formar un cruz
        float[] angles = { 0f, 90f, 180f, 270f };

        foreach (float angle in angles)
        {
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            GameObject projectile = Instantiate(projectilePrefab, transform.position, rot);
            projectile.layer = 11;
        }
    }


}



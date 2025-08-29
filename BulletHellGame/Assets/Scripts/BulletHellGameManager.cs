using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BulletHellGameManager : MonoBehaviour
{
    public static BulletHellGameManager singleton;

    // ----------------------------
    // Diccionarios de configuración
    // ----------------------------
    public static Dictionary<ShipType, ShipData> shipDataDictionary = new Dictionary<ShipType, ShipData>()
    {
        { ShipType.Player, new ShipData(new Vector2(14,15), 20f, 0.1f, 15f, 150f) },
        { ShipType.BasicEnemy, new ShipData(new Vector2(5,5), 15f, 2f, 10f, 100f) },
        { ShipType.BasicEnemy2, new ShipData(new Vector2(7,7), 15f, 0.5f, 20f, 100f) }
    };

    public static Dictionary<ShipType, List<Move>> enemyMoveSetDictionary = new Dictionary<ShipType, List<Move>>()
    {
        { ShipType.BasicEnemy, new List<Move>()
            {
                new Move(KeyCode.A,1),
                new Move(KeyCode.D,1),
                new Move(KeyCode.A,1),
                new Move(KeyCode.A,1),
                new Move(KeyCode.D,1),
                new Move(KeyCode.A,1),
                new Move(KeyCode.D,1),
            }
        },
        { ShipType.BasicEnemy2, new List<Move>()
            {
                new Move(KeyCode.W,2),
                new Move(KeyCode.A,1),
                new Move(KeyCode.D,1),
                new Move(KeyCode.S,2),
                new Move(KeyCode.W,2),
                new Move(KeyCode.A,1),
                new Move(KeyCode.D,1),
                new Move(KeyCode.S,2)
            }
        }
    };

    public static List<SpawnAction> spawnActions = new List<SpawnAction>()
    {
        new SpawnAction(ShipType.BasicEnemy, new Vector3(0,8,0), 2f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(0,8,0), 4f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(1,8,0), 4f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(2,8,0), 6f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(2,8,0), 6f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(0,8,0), 2f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(1,8,0), 4f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(4,8,0), 4f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(-2,8,0), 6f),
        new SpawnAction(ShipType.BasicEnemy, new Vector3(2,8,0), 6f),
        new SpawnAction(ShipType.BasicEnemy2, new Vector3(0,8,0), 8f),
        new SpawnAction(ShipType.BasicEnemy2, new Vector3(-1,8,0), 10f),
        new SpawnAction(ShipType.BasicEnemy2, new Vector3(2,8,0), 10f),
        new SpawnAction(ShipType.BasicEnemy2, new Vector3(0,8,0), 12f),
        new SpawnAction(ShipType.BasicEnemy2, new Vector3(-1,8,0), 14f)
    };  

    public static Dictionary<ShipType, GameObject> shipPrefabDictionary;
    // ----------------------------
    // BulletManager integrado
    // ----------------------------
    public TextMeshProUGUI bulletCounterText; // <- Cambiado a TMP

    private void Awake()
    {
        shipPrefabDictionary = new Dictionary<ShipType, GameObject>()
    {
        { ShipType.BasicEnemy, Resources.Load<GameObject>("Prefabs/BasicEnemy") as GameObject },
        { ShipType.BasicEnemy2, Resources.Load<GameObject>("Prefabs/BasicEnemy2") as GameObject },
    };

        singleton = this;
    }

    // ----------------------------
    // Métodos de balas
    // ----------------------------

    public void RegisterBullet()
    {
        UpdateUI(); // actualiza el contador al crear bala
    }

    public void UnregisterBullet()
    {
        UpdateUI(); // actualiza el contador al destruir bala
    }

    private void UpdateUI()
    {
        if (bulletCounterText != null)
        {
            int currentBullets = GameObject.FindGameObjectsWithTag("Projectile").Length;
            bulletCounterText.text = "Balas: " + currentBullets;
        }
    }
}

// ----------------------------
// Clases auxiliares
// ----------------------------
public enum ShipType
{
    Player,
    BasicEnemy,
    BasicEnemy2
}

public class ShipData
{
    public Vector2 acceleration;
    public float projectileSpeed;
    public float projectileCooldown;
    public float projectileDamage;
    public float health;

    public ShipData(Vector2 acc, float pSpeed, float pCooldown, float pDamage, float hp)
    {
        this.acceleration = acc;
        this.projectileSpeed = pSpeed;
        this.projectileCooldown = pCooldown;
        this.projectileDamage = pDamage;
        this.health = hp;
    }
}

public class Move
{
    public KeyCode wichKey;
    public float duration;

    public Move(KeyCode wichKey, float duration)
    {
        this.wichKey = wichKey;
        this.duration = duration;
    }
}

public class SpawnAction
{
    public ShipType enemy;
    public Vector3 coord;
    public float delay;

    public SpawnAction(ShipType enemy, Vector3 coord, float delay)
    {
        this.enemy = enemy;
        this.coord = coord;
        this.delay = delay;
    }
}

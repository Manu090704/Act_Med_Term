using UnityEngine;

public class BulletHellSpawner : MonoBehaviour
{
    private SpawnAction currentAction;
    private int spawnActionIndex = 0;
    private float lastSpawnTime;
    private bool hasAllSpawned = false;

    void Start()
    {
        // Verifica que la lista exista y tenga elementos
        if (BulletHellGameManager.spawnActions == null || BulletHellGameManager.spawnActions.Count == 0)
        {
            Debug.LogError("No hay SpawnActions definidos en BulletHellGameManager.");
            return;
        }

        // Inicializa el primer spawn
        currentAction = BulletHellGameManager.spawnActions[0];
        Spawn(currentAction);
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (hasAllSpawned) return;

        if (Time.time > lastSpawnTime + currentAction.delay)
        {
            spawnActionIndex++;

            if (spawnActionIndex >= BulletHellGameManager.spawnActions.Count)
            {
                hasAllSpawned = true;
                return;
            }

            currentAction = BulletHellGameManager.spawnActions[spawnActionIndex];
            Spawn(currentAction);
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn(SpawnAction spawnAction)
    {
        // Verifica que el diccionario exista y tenga la clave
        if (BulletHellGameManager.shipPrefabDictionary == null)
        {
            Debug.LogError("shipPrefabDictionary no está inicializado en BulletHellGameManager.");
            return;
        }

        if (!BulletHellGameManager.shipPrefabDictionary.ContainsKey(spawnAction.enemy))
        {
            Debug.LogError("No se encontró el prefab para enemy: " + spawnAction.enemy);
            return;
        }

        GameObject prefab = BulletHellGameManager.shipPrefabDictionary[spawnAction.enemy];
        if(prefab == null)
        {
            Debug.LogError("Prefab null para " + spawnAction.enemy + ". Revisa Resources/Prefabs/");
            return;
        }

        GameObject enemy = Instantiate(prefab, spawnAction.coord, Quaternion.identity);


        // Ajusta su dirección si aparece arriba
        if (spawnAction.coord.y > 0)
        {
            BulletHellCar car = enemy.GetComponent<BulletHellCar>();
            if (car != null)
            {
                car.direction = BulletHellCar.Direction.Down;
                enemy.transform.Rotate(0, 0, 180);
            }
            else
            {
                Debug.LogWarning("El prefab " + spawnAction.enemy + " no tiene BulletHellCar.");
            }
        }
    }
}

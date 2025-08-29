using UnityEngine;

public class RoadController : MonoBehaviour
{
    private float speed = 20f;
    public GameObject roadPrefab;
    private GameObject lastRoad;
    public static RoadController singleton;

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        // Primer bloque de carretera como hijo del controlador
        lastRoad = Instantiate(roadPrefab, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        ScrollRoad();
    }

    void ScrollRoad()
    {
        foreach (Transform t in this.transform)
        {
            Vector2 pos = t.transform.position;
            pos.y -= speed * Time.deltaTime;
            t.transform.position = pos;
        }
    }

    public void SpawnRoad()
    {
        // Calcula la altura real del sprite
        float roadHeight = roadPrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        // Nueva posición = última carretera + altura en Y
        Vector3 newPos = lastRoad.transform.position + new Vector3(0, roadHeight, 0);

        // Instancia como hijo del controlador
        lastRoad = Instantiate(roadPrefab, newPos, Quaternion.identity, this.transform);
    }
}

using UnityEngine;

public class ScreenBoundaryController : MonoBehaviour
{
    public GameObject bottom, left, right;
    public GameObject enemyDespawnTop, enemyDespawnBottom, enemyDespawnLeft, enemyDespawnRight;

    void Start()
    {
        // Paredes con posiciones fijas
        left.transform.position = new Vector2(-4.17f, 0.37f);
        right.transform.position = new Vector2(3.8f, 0.73f);

        left.transform.localScale = new Vector3(1, 10, 1);   // altura 10 unidades
        right.transform.localScale = new Vector3(1, 10, 1);

        // Bottom fijo
        bottom.transform.position = new Vector2(0, -5f);
        bottom.transform.localScale = new Vector3(10, 1, 1); // ancho 10 unidades

        // Paredes de despawn de enemigos igual a las originales
        enemyDespawnBottom.transform.position = bottom.transform.position;
        enemyDespawnBottom.transform.localScale = bottom.transform.localScale;

        enemyDespawnLeft.transform.position = left.transform.position;
        enemyDespawnLeft.transform.localScale = left.transform.localScale;

        enemyDespawnRight.transform.position = right.transform.position;
        enemyDespawnRight.transform.localScale = right.transform.localScale;
    }
}

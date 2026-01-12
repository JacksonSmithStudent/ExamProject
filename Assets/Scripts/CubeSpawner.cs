using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public Vector3 spawnPosition;
    public Vector3 endPosition;

    public void SpawnCube()
    {
        GameObject cube = Instantiate(
            cubePrefab,
            spawnPosition,
            Quaternion.identity
        );

        cube.GetComponent<Cube>().SetTarget(endPosition);
    }
}

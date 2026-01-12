using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Object scene;   
    [SerializeField] string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (scene == null)
        {
            Debug.LogError("Scene not assigned!", this);
            return;
        }

        SceneManager.LoadScene(scene.name);
    }
}

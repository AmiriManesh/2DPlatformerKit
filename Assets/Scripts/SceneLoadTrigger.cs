using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private string loadSceneString;
    [SerializeField] private bool destroyPlayer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            SceneManager.LoadScene(loadSceneString);
            //NewPlayer.Instance.SetSpawnPosition();
            if(destroyPlayer)
            {
                Destroy(NewPlayer.Instance.gameObject);
                Destroy(GameManager.Instance.gameObject);
            }
        }
    }
}

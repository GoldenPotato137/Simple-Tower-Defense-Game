using Helper;
using Manager;
using UnityEngine;

public class Core : MonoBehaviour
{
    private static Core Instance { get; set; }
    
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameManager.Instance = new GameManager();
    }

    private void Update()
    {
        CoroutineManager.Update();
        GameManager.Instance.Update();
    }
}
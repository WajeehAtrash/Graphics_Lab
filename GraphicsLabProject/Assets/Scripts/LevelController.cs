using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] FinishPlate f1;
    [SerializeField] FinishPlate f2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (f1.GetIsOnFinish() && f2.GetIsOnFinish())
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Application.Quit();
        }
    }
}

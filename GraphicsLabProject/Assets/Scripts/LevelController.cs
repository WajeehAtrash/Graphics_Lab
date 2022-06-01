using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] FinishPlate f1;
    [SerializeField] FinishPlate f2;

    // Update is called once per frame
    void Update()
    {
        if (f1.GetIsOnFinish() && f2.GetIsOnFinish())
        {
            int lvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (lvlIndex < 2)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }
    }
}

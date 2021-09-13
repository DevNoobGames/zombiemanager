using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject[] nextCam;

    public GameManager gameMan;
    
    public int active = 0;
    
    public void DisableAllCams()
    {
        mainCam.SetActive(false);
        foreach (GameObject cam in nextCam)
        {
            cam.SetActive(false);
        }

        foreach (GameObject enCam in gameMan.enemies)
        {
            enCam.GetComponent<EnemyAI>().cam.SetActive(false);
        }
    }

    public void mainCamSel()
    {
        DisableAllCams();
        mainCam.SetActive(true);
    }

    public void nextCamSel()
    {
        DisableAllCams();

        if (nextCam.Length == (active + 1))
        {
            active = 0;
        }
        else
        {
            active += 1;
        }
        nextCam[active].SetActive(true);
    }

    public void enemyCam()
    {
        if (gameMan.enemies.Count > 0)
        {
            DisableAllCams();
            int randEne = Random.Range(0, gameMan.enemies.Count);
            gameMan.enemies[randEne].GetComponent<EnemyAI>().cam.SetActive(true);
        }
    }
}

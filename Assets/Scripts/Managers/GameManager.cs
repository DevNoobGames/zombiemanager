using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public GameObject introText;
    public GameObject introBlackPanel;

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> zombies = new List<GameObject>();
    public GameObject[] UI;

    public List<GameObject> PlacementAreas = new List<GameObject>();

    public List<levels> levelList = new List<levels>();

    public GameObject EndGamePanel;
    public TextMeshProUGUI EndGameText;
    public PlayerManager playman;

    public GameObject introFolder;

    [System.Serializable]
    public class levels
    {
        public int level;
        public int amountOfZombies;
        public GameObject[] TypeOfZombies;
    }

    public void Start()
    {
        StartCoroutine("nextLevel", 1);

        //Clear out the areas
        foreach (GameObject placementArea in GameObject.FindGameObjectsWithTag("placementArea"))
        {
            PlacementAreas.Add(placementArea);
        }
        foreach (GameObject pa in PlacementAreas)
        {
            pa.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    IEnumerator nextLevel(int level)
    {


        if (level == 21)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = "Congrats! Game finished with" + "\n" + "$" + playman.money;
        }
        else
        {

            introText.SetActive(true);
            introBlackPanel.SetActive(true);

            foreach (GameObject ui in UI)
            {
                ui.SetActive(false);
            }
            introText.GetComponent<TextMeshProUGUI>().text = "Level" + "\n" + "-" + level + "-";
            introText.GetComponent<Animation>().Play();
            introBlackPanel.GetComponent<Animation>().Play();

            yield return new WaitForSeconds(3);

            foreach (GameObject zom in zombies)
            {
                Destroy(zom);
            }

            foreach (GameObject ui in UI)
            {
                ui.SetActive(true);
            }
            introText.SetActive(false);
            //After the intro is finished, place the enemies
            EnemyPlacement(level);

            if (level == 1)
            {
                introFolder.SetActive(true);
            }

            //also close blackpanel
            yield return new WaitForSeconds(0.5f);
            introBlackPanel.SetActive(false);
        }
    }

    public void EnemyPlacement(int level)
    {
        //Clean up the list
        PlacementAreas.Clear(); 
        
        //Add all the areas again
        foreach (GameObject placementArea in GameObject.FindGameObjectsWithTag("placementArea"))
        {
            PlacementAreas.Add(placementArea);
        }

        foreach (levels lvl in levelList)
        {
            if (lvl.level == level)
            {
                for (int i = 0; i < lvl.amountOfZombies; i++)
                {
                    int randInt = Random.Range(0, PlacementAreas.Count); //Find placement area - last one is Exclusive so will never b called
                    int randZom = Random.Range(0, lvl.TypeOfZombies.Length); //Find which zombie to place

                    GameObject enemyy = Instantiate(lvl.TypeOfZombies[randZom], PlacementAreas[randInt].transform.position, Quaternion.identity) as GameObject;
                    enemies.Add(enemyy);
                    enemyy.GetComponent<EnemyAI>().gameMan = this.GetComponent<GameManager>();
                    PlacementAreas.RemoveAt(randInt);
                    if (PlacementAreas.Count == 0) //if we run out of placement areas
                    {
                        return;
                    }
                }
                return; //return so it doesn't go through all the other levels
            }
        }
    }

    public void checkEnemiesLeft()
    {
        if (enemies.Count <= 0)
        {
            //start next level
            level += 1;
            StartCoroutine("nextLevel", level);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public List<int> amount = new List<int>();
    public ZombieCreator[] ZombieCreators;
    public int activeAmount;
    public TextMeshProUGUI AmountText;

    public AudioSource clickSound;

    public GameObject pauseMenu;

    private void Start()
    {
        AmountText.text = amount[activeAmount].ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }
    }

    public void quitTheGame()
    {
        Application.Quit();
    }
    public void continuetheGame()
    {
        pauseMenu.SetActive(false);
    }

    public void nextAmount()
    {
        if (activeAmount < (amount.Count- 1))
        {
            activeAmount += 1;
        }
        else
        {
            activeAmount = 0;
        }
        AmountText.text = amount[activeAmount].ToString();

        foreach (ZombieCreator zom in ZombieCreators)
        {
            zom.Cost = zom.Basecost * amount[activeAmount];
            zom.infoText.text = "$" + zom.Cost; //Before it was zom.Name + "\n" + "$" + zom.Cost;
        }

        clickSound.Play();
    }

}

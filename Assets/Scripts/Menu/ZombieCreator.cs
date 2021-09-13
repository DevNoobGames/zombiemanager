using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ZombieCreator : MonoBehaviour, IPointerDownHandler
{
    public GameObject zombieDragger;
    public GameObject zombieToDrop;
    public float Basecost;
    public float Cost;
    public string Name;
    public TextMeshProUGUI infoText;
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        infoText.text = "$" + Cost;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerManager.money >= Cost)
        {
            GameObject zombieDrag = Instantiate(zombieDragger, new Vector3(0, 0, 0), Quaternion.identity);
            zombieDrag.GetComponent<ZombieDragger>().cost = Cost;
            zombieDrag.GetComponent<ZombieDragger>().AmountOfZombies = (Cost / Basecost); //a simple way to decide how many zombies to place
            zombieDrag.GetComponent<ZombieDragger>().zombieToDrop = zombieToDrop;
        }
    }
}

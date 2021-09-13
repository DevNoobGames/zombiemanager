using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDragger : MonoBehaviour
{
    public bool inPlacementArea = false;
    public float cost;
    public Material green;
    public Material red;
    public PlayerManager playerManager;
    public float AmountOfZombies;
    public GameObject zombieToDrop;

    public AudioSource releaseSound;

    public GameManager gameMan;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        gameMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        releaseSound = GameObject.FindGameObjectWithTag("soundClick").GetComponent<AudioSource>();
    }

    void Update()
    {
        followMouse();

        if (Input.GetMouseButtonUp(0))
        {
            if (inPlacementArea && playerManager.money >= cost)
            {
                for (int i = 0; i < AmountOfZombies; i++)
                {
                    Instantiate(zombieToDrop, transform.position, Quaternion.identity);
                }
                releaseSound.Play();
                playerManager.ChangeMoney(-cost);

                gameMan.introFolder.SetActive(false);

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    void followMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            float Y = (Mathf.RoundToInt(hit.point.y) + 1f);
            transform.position = new Vector3(hit.point.x, Y, hit.point.z);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GroundPlacement"))
        {
            if (playerManager.money >= cost)
            {
                inPlacementArea = true;
                GetComponent<Renderer>().material = green;
            }
            else
            {
                inPlacementArea = false;
                GetComponent<Renderer>().material = red;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GroundPlacement"))
        {
            inPlacementArea = false;
            GetComponent<Renderer>().material = red;
        }
    }
}

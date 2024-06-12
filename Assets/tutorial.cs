using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{
    [Header("necessary stuff")]
    [SerializeField] private GameObject ship;
    [SerializeField] private Rigidbody shipBody;
    [SerializeField] private GameObject HUD;
    [SerializeField] private List<GameObject> tutScreens;
    [SerializeField] private List<Vector3> startlocations;
    [SerializeField] private List<GameObject> triggers;
    private GameObject currentScreen;
    private int currentTut;
    

// Start is called before the first frame update
    void Start()
    {
        currentScreen = Instantiate(tutScreens[0],HUD.transform);
        ship.transform.position = startlocations[0];
    }

    private void NextTut()
    {
        if (currentTut < tutScreens.Count-1)
        {
            currentTut++;
            Destroy(currentScreen);
            currentScreen = Instantiate(tutScreens[currentTut],HUD.transform);
            ship.transform.position = startlocations[currentTut];
            shipBody.position = startlocations[currentTut];
            shipBody.velocity = Vector3.zero;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Triangle"))
        {
            NextTut();
        }
    }
    
}

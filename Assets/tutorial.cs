using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{
    [Header("necessary stuff")] [SerializeField]
    private GameObject ship;
    [SerializeField] private Rigidbody shipBody;
    [SerializeField] private GameObject HUD;
    [SerializeField] private List<GameObject> tutScreens;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private List<GameObject> triggers;
    [SerializeField] private List<string> paths;
    [SerializeField] private bool startPaused;
    private GameObject currentScreen;
    private int currentTut;
    private bool tutActive = true;


// Start is called before the first frame update
    void Start()
    {
        currentScreen = Instantiate(tutScreens[0], HUD.transform);
        ship.transform.position = startPos;
        pathGenerator.Instance.LoadPath(paths[0]);
        if (startPaused)
        {
            Time.timeScale = 0f;
        }
    }
    

    private void NextTut()
    {
        if (currentTut < tutScreens.Count - 1)
        {
            currentTut++;
            Destroy(currentScreen);
            currentScreen = Instantiate(tutScreens[currentTut], HUD.transform);
            ship.transform.position = startPos;
            ship.transform.rotation = Quaternion.identity;
            shipBody.position = startPos;
            shipBody.rotation = Quaternion.identity;
            shipBody.velocity = Vector3.zero;
            shipBody.angularVelocity = Vector3.zero;
            pathGenerator.Instance.LoadPath(paths[currentTut]);
        }
        else if (startPaused)
        {
            Destroy(currentScreen);
            tutActive = false;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Reset()
    {
        ship.transform.position = startPos;
        ship.transform.rotation = Quaternion.identity;
        shipBody.position = startPos;
        shipBody.rotation = Quaternion.identity;
        shipBody.velocity = Vector3.zero;
        shipBody.angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Triangle")&&tutActive)
        {
            Time.timeScale = 1;
            NextTut();
        }
    }
}
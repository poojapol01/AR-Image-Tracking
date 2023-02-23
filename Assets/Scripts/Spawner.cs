using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Spawner : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject[] objectToPlaceGameObjects;
    private GameObject objectToPlace;
    public ARRaycastManager raycastManager;
    Camera arCamera;
    GameObject mainCanvas;

    public bool useCursor = true;

    private void Awake()
    {
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        mainCanvas = GameObject.Find("MainCanvas");
    }

    void Start()
    {
        cursorChildObject.SetActive(useCursor);
        arCamera.enabled = false;
        mainCanvas.SetActive(true);
    }

    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    public void ModelClicked()
    {
        arCamera.enabled = true;
        objectToPlace = objectToPlaceGameObjects[0];
        mainCanvas.SetActive(false);
    }

    public void CubeClicked()
    {
        arCamera.enabled = true;
        objectToPlace = objectToPlaceGameObjects[1];
        mainCanvas.SetActive(false);
    }
    public void SphereClicked()
    {
        arCamera.enabled = true;
        objectToPlace = objectToPlaceGameObjects[2];
        mainCanvas.SetActive(false);
    }
    public void BackButtonClicked()
    {
        arCamera.enabled = false;
        mainCanvas.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

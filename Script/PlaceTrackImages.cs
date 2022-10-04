using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackImages : MonoBehaviour
{
    //Reference to AR tracked image manager component
    private ARTrackedImageManager _trackedImagesManager;

    public GameObject[] ARPrefabs;
    public GameObject _CanvasSS;

    //Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    //Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        //Loop through all new tracked images that have been deteched
        foreach (var trackedImage in obj.added)
        {
            //Get the name of the reference image
            var imageName = trackedImage.referenceImage.name;
            foreach (var currPrefab in ARPrefabs)
            {
                if (string.Compare(currPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
                    && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(currPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        foreach (var trackedImage in obj.updated)
        {
            _instantiatedPrefabs[trackedImage.referenceImage.name]
                .SetActive(trackedImage.trackingState == TrackingState.Tracking);
            _CanvasSS.SetActive(true);
        }

        foreach (var trackedImage in obj.removed)
        {
            Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
            _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            _CanvasSS.SetActive(false);
        }
    }
}

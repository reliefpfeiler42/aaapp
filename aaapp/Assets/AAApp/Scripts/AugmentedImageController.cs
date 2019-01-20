using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class AugmentedImageController : MonoBehaviour
{
	public GameObject PrefabDino;
	public GameObject PrefabDrone;
	public GameObject Camera;
    private readonly List<AugmentedImage> _images = new List<AugmentedImage>();
	private bool _dinoIsActive;
	private bool _droneIsActive;

    void Start()
    {
		_dinoIsActive = false;
		_droneIsActive = false;
    }

    void Update()
    {
		if (Session.Status != SessionStatus.Tracking)
		{
			return;
		}

		Session.GetTrackables(_images, TrackableQueryFilter.Updated);
		VisualizeGameObject();
    }

	private void VisualizeGameObject()
	{
		foreach (var image in _images)
		{
			// if (image.DatabaseIndex == 0 && _dinoIsActive == false)
			// {
			// 	Anchor anchor = image.CreateAnchor(image.CenterPose);
			// 	var dino = Instantiate(PrefabDino, anchor.transform);
			// 	_dinoIsActive = true;
			// 	dino.transform.localScale = new Vector3(image.ExtentX, image.ExtentX, image.ExtentX);
			// }
			if (image.DatabaseIndex == 0 && _droneIsActive == false)
			{
				Vector3 offset = new Vector3(0, 0, 2);
				float imageFactor = 2;
				Anchor anchor = image.CreateAnchor(image.CenterPose);
				// anchor.transform.position = Camera.transform.position + offset;
				GameObject drone = Instantiate(PrefabDrone, anchor.transform);
				_droneIsActive = true;
				drone.transform.localScale = new Vector3(image.ExtentX * imageFactor, image.ExtentX *imageFactor, image.ExtentX * imageFactor);
				drone.transform.Rotate(90f, 0, 0);
				drone.transform.position = Camera.transform.position + offset;

			}
			else
			{
				return;
			}
		}
	}

}

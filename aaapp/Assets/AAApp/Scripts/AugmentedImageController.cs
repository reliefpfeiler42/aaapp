using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class AugmentedImageController : MonoBehaviour
{
	public GameObject PrefabDino;
    private readonly List<AugmentedImage> _images = new List<AugmentedImage>();
	private bool _dinoIsActive;

    void Start()
    {
		_dinoIsActive = false;
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
			if (image.DatabaseIndex == 0 && _dinoIsActive == false)
			{
				Anchor anchor = image.CreateAnchor(image.CenterPose);
				var dino = Instantiate(PrefabDino, anchor.transform);
				_dinoIsActive = true;
				dino.transform.localScale = new Vector3(image.ExtentX, image.ExtentX, image.ExtentX);
			}
		}
	}

}

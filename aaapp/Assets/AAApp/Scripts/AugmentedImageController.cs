using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class AugmentedImageController : MonoBehaviour
{

	public GameObject PrefabToInstantiate;
    private readonly List<AugmentedImage> _images = new List<AugmentedImage>();

    void Start()
    {

    }

    void Update()
    {
		if (Session.Status != SessionStatus.Tracking)
		{
			return;
		}

		Session.GetTrackables(_images, TrackableQueryFilter.Updated);
		VisualizeTrackables();
    }

	private void VisualizeTrackables()
	{
		foreach (var image in _images)
		{
			if (image.DatabaseIndex == 0)
			{
				Anchor anchor = image.CreateAnchor(image.CenterPose);
				var myGameObject = Instantiate(PrefabToInstantiate, anchor.transform);
			}
		}
	}

}

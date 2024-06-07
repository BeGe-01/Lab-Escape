using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum CollectibleTypes
{
	Green_Key,
	Red_Key,
	Battery,
}
public class Collectible : MonoBehaviour
{
	public string item_id;
	public CollectibleTypes type = CollectibleTypes.Green_Key;
	[SerializeField] float followDistance = 1.5f;
	[SerializeField] float speed = .5f;
	[Space]
	public bool isFollowing = false;
	public float itemIndex = 0f;
	[HideInInspector] public Vector2 originalPosition;
	private GameObject followTarget;
	private Vector2 velocity = Vector2.zero;
	private SpriteRenderer sr;



	// Start is called before the first frame update
	void Start()
	{
		originalPosition = transform.position;
		sr = GetComponent<SpriteRenderer>();
		if (type == CollectibleTypes.Battery) CheckCollected();
	}

	// Update is called once per frame
	void Update()
	{
		if (isFollowing)
		{
			if (Vector2.Distance(transform.position, followTarget.transform.position) > (followDistance * itemIndex))
			{
				transform.position = Vector2.SmoothDamp(transform.position, new Vector2(followTarget.transform.position.x, followTarget.transform.position.y),
								ref velocity, speed);
			}
		}
	}

	public void RemoveItem()
	{
		if (type == CollectibleTypes.Battery)
		{
			SaveManager.instance.CollectBattery(item_id);
		}
		StartCoroutine(PlayDestroyAnim());
	}

	public void StartFollowing(GameObject target, float index)
	{
		StopCoroutine(ReturnAnimation());
		isFollowing = true;
		followTarget = target;
		itemIndex = index;
	}

	public void ReturnItem()
	{
		isFollowing = false;
		followTarget = null;
		StartCoroutine(ReturnAnimation());
	}

	IEnumerator ReturnAnimation()
	{
		while (Vector2.Distance(transform.position, originalPosition) > 0 && !isFollowing)
		{
			transform.position = Vector2.SmoothDamp(transform.position, new Vector2(originalPosition.x, originalPosition.y),
															ref velocity, speed);
			yield return null;
		}
	}

	public IEnumerator PlayDestroyAnim()
	{
		Color c = sr.color;
		c.a = 0f;
		sr.color = c;
		GetComponent<Light2D>().enabled = false;
		GetComponent<ParticleSystem>().Play();
		while (GetComponent<ParticleSystem>().isPlaying)
		{
			yield return null;
		}
		Destroy(gameObject);
	}

	void CheckCollected()
	{
		if (SaveManager.instance.saveData.batteries.Count > 0)
		{
			SaveManager.instance.saveData.batteries.ForEach(i =>
							{
								if (i == item_id)
								{
									Destroy(gameObject);
								}
							});
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementGenerator : MonoBehaviour
{
	public GameObject[] houses;

	public float radius;
	public float amount;
	public float density;
	public void Awake()
	{
		for (int i = 0; i < amount; i++)
		{
			int index = Random.Range(0, houses.Length);
			Instantiate(houses[index], this.transform.position + GeneratePoint(), GenerateRotations(), this.transform);
		}

	}

	private Vector3 GeneratePoint()
	{
		float distanceFromCenter = RandomGaussian(0, radius);

		float xChance = Random.Range(-1.0f, 1.0f);
		float zChance = Random.Range(-1.0f, 1.0f);
		Vector3 randomDirection = new Vector3(xChance, 0, zChance);
		Vector3 positon = randomDirection * distanceFromCenter;
		return positon;
	}

	private Quaternion GenerateRotations()
	{
		return Quaternion.AngleAxis( Random.Range(0.0f, 360.0f) , Vector3.up);
	}

	public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f) //https://answers.unity.com/questions/421968/normal-distribution-random.html
	{
		float u, v, S;

		do
		{
			u = 2.0f * UnityEngine.Random.value - 1.0f;
			v = 2.0f * UnityEngine.Random.value - 1.0f;
			S = u * u + v * v;
		}
		while (S >= 1.0f);

		// Standard Normal Distribution
		float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

		// Normal Distribution centered between the min and max value
		// and clamped following the "three-sigma rule"
		float mean = (minValue + maxValue) / 2.0f;
		float sigma = (maxValue - mean) / 3.0f;
		return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
	}
}

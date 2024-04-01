using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrain;
using UI;
using UnityEngine;

public class Building : InteractableObject, IProducer, IConsumer
{
	[SerializeField] private ShipLandingQueue shipLandingQueue;
	[SerializeField] protected Transform summaryUIAnchor;
	[SerializeField] public Storage storage;
	[SerializeField] private List<LandingPlatform> landingPlatforms;

	public BuildingDefintion BuildingDefintion { get; set; }
	private LevelBuilding currentLevel;
	public IslandBlock islandBlock;

	private int level = 0;

	public void Init(BuildingDefintion buildingDefintion)
	{
		this.BuildingDefintion = buildingDefintion;
		level = 0;
		PlaceLevelBuilding(level);
	}

	private void PlaceLevelBuilding(int level)
	{
		currentLevel = Instantiate(BuildingDefintion.buildingLevels[level].buildingPrefab, this.transform);
		currentLevel.Init(BuildingDefintion.buildingLevels[level], this);
	}

	private void OnEnable()
	{
		TurnsController.TurnEndedEvent += TurnEndedEvent;
	}

	private void OnDisable()
	{
		TurnsController.TurnEndedEvent -= TurnEndedEvent;
	}

	private void TurnEndedEvent(int turn)
	{
		bool needsFulfilled = currentLevel.AreNeedsFulfilled();
		if (!needsFulfilled)
		{
			LevelDownBuilding();
			return;
		}

		bool wantsFulfilled = currentLevel.AreWantsFulfilled();
		if (!currentLevel.HasWants() || wantsFulfilled)
		{
			LevelUpBuilding();
			return;
		}
		ResetRequest();
	}

	private void LevelDownBuilding()
	{
		if (level == 0)
		{
			ResetRequest();
			return;
		}

		Destroy(currentLevel.gameObject);
		level--;
		PlaceLevelBuilding(level);
	}

	private void LevelUpBuilding()
	{
		if (level + 1 >= BuildingDefintion.buildingLevels.Length)
		{
			ResetRequest();
			return;
		}

		Destroy(currentLevel.gameObject);
		level++;
		PlaceLevelBuilding(level);
	}

	private void ResetRequest()
	{
		currentLevel.ResetRequest();
		storage.ClearStorage();
	}

	internal LevelBuilding GetCurrentLevel()
	{
		return currentLevel;
	}

	public override string GetInteractableObjectName()
	{
		return currentLevel.GetLevelName();
	}

	private LandingPlatform GetMostAvailblePlatform()
	{
		int lowest = int.MaxValue;
		int index = 0;
		for (int i = 0; i < landingPlatforms.Count; i++)
		{
			int waiting = landingPlatforms[i].WaitingShipsCount();
			if (waiting < lowest)
			{
				lowest = waiting;
				index = i;
			}
		}
		return landingPlatforms[index];
	}

	LandingPlatform IProducer.GetProducerLandingPlatform()
	{
		return GetMostAvailblePlatform();
	}

	ResourceDefintion[] IProducer.GetProducedResources()
	{
		List<ResourceDefintion> resources = new List<ResourceDefintion>();
		resources.AddRange(currentLevel.needs.GetResourcesFromRequests<ProduceRequest>());
		resources.AddRange(currentLevel.wants.GetResourcesFromRequests<ProduceRequest>());
		return resources.ToArray();
	}

	bool IProducer.CanProducesResource(ResourceDefintion resourceData)
	{
		List<ResourceDefintion> resources = new List<ResourceDefintion>();
		resources.AddRange(currentLevel.needs.GetResourcesFromRequests<ProduceRequest>());
		resources.AddRange(currentLevel.wants.GetResourcesFromRequests<ProduceRequest>());
		return resources.Contains(resourceData);
	}

	void IProducer.SellResourceToShip(ResourceDefintion resource, Ship ship)
	{
		currentLevel.needs.UpdateResourceRequests<ProduceRequest>(resource, 1);
		currentLevel.wants.UpdateResourceRequests<ProduceRequest>(resource, 1);
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);
		storage.RemoveStorage(resource);
		GetComponent<SettlementGenerator>().AddMiniBuildings();
	}

	Transform IProducer.GetProducerNavigationPostion()
	{
		return summaryUIAnchor;
	}

	ResourceDefintion[] IConsumer.GetConsumedResources()
	{
		List<ResourceDefintion> resources = new List<ResourceDefintion>();
		resources.AddRange(currentLevel.needs.GetResourcesFromRequests<ConsumeRequest>());
		resources.AddRange(currentLevel.wants.GetResourcesFromRequests<ConsumeRequest>());
		return resources.ToArray();
	}

	bool IConsumer.CanConsumeResource(ResourceDefintion resourceData)
	{
		List<ResourceDefintion> resources = new List<ResourceDefintion>();
		resources.AddRange(currentLevel.needs.GetResourcesFromRequests<ConsumeRequest>());
		resources.AddRange(currentLevel.wants.GetResourcesFromRequests<ConsumeRequest>());
		return resources.Contains(resourceData);
	}

	LandingPlatform IConsumer.GetConsumerLandingPlatform()
	{
		return GetMostAvailblePlatform();
	}

	Transform IConsumer.GetConsumerNavigationPostion()
	{
		return summaryUIAnchor;
	}

	void IConsumer.BuyResourceFromShip(ResourceDefintion resource, Ship ship)
	{
		currentLevel.needs.UpdateResourceRequests<ConsumeRequest>(resource, 1);
		currentLevel.wants.UpdateResourceRequests<ConsumeRequest>(resource, 1);
		WalletController.instance.AddSellTransaction(resource.sellPrice, this, ship, resource);
		storage.AddStorage(resource);
		GetComponent<SettlementGenerator>().AddMiniBuildings();
	}
}


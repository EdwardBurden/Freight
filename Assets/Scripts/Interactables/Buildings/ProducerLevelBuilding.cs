using System.Linq;
using UnityEngine;

public class ProducerLevelBuilding : LevelBuilding, IProducer
{
	[SerializeField] private LandingPlatform platform;
	private ProducerLevelDefinition farmDefintion => buildingDefintion as ProducerLevelDefinition;

	LandingPlatform IProducer.GetProducerLandingPlatform()
	{
		return platform;
	}

	ResourceDefintion[] IProducer.GetProducedResources()
	{
		return needs.GetResourcesFromRequests<ProduceRequest>();
	}

	bool IProducer.CanProducesResource(ResourceDefintion resourceData)
	{
		return needs.GetResourcesFromRequests<ProduceRequest>().Contains(resourceData);
	}

	void IProducer.SellResourceToShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);
		storage.RemoveStorage(resource);
		needs.UpdateResourceRequests<ProduceRequest>(resource, 1);
	}

	Transform IProducer.GetProducerNavigationPostion()
	{
		return summaryUIAnchor;//temp
	}
}

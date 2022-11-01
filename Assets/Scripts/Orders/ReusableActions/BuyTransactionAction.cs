using ActionLists;

public class ShipBuyAction : Action
{
	private IProducer producer;
	private ResourceDefintion resource;
	private Ship ship;

	public ShipBuyAction(IProducer producer, ResourceDefintion resourceDefintion, Ship ship)
	{
		this.producer = producer;
		resource = resourceDefintion;
		this.ship = ship;
	}

	public override void OnUpdate()
	{
		producer.SellResourceToShip(resource, ship);
		isFinished = true;
	}
}

using ActionLists;

public class ShipSellAction : Action
{
	private IConsumer consumer;
	private ResourceDefintion resource;
	private Ship ship;

	public ShipSellAction(IConsumer consumer, ResourceDefintion resourceDefintion, Ship ship)
	{
		this.consumer = consumer;
		resource = resourceDefintion;
		this.ship = ship;
	}

	public override void OnUpdate()
	{
		consumer.BuyResourceFromShip(resource, ship);
		isFinished = true;
	}
}

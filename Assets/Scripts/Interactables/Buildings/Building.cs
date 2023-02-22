using OpenCover.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrain;

public class Building : InteractableObject
{
	public BuildingDefintion BuildingDefintion { get; set; }
	public LevelBuilding levelBuilding;
	public IslandBlock islandBlock;

	private void Start()
	{
		levelBuilding = Instantiate(BuildingDefintion.buildingLevels[0].buildingPrefab, this.transform);
	//	levelBuilding.Init(BuildingDefintion.buildingLevels[0],this);
	}


	public override void OnHover()
	{
		base.OnHover();
		levelBuilding.OnHover();
	}

	public override void OffHover()
	{
		base.OffHover();
		levelBuilding.OffHover();
	}

	public override string GetInteractableObjectName()
	{
		return levelBuilding.GetInteractableObjectName();
	}
}


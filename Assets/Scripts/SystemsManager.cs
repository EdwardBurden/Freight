using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemsManager : Singleton<SystemsManager>
{
	public MouseInput mouseInput;
	public ShipSelector shipSelector;
	public Wallet wallet;

	private void Awake()
	{
		_instance = this;
	}
}

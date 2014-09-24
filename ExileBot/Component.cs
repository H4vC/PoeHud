using System;
namespace ExileBot
{
	public abstract class Component : RemoteMemoryObject
	{
		public Poe_Entity Owner
		{
			get
			{
				return base.ReadObject<Poe_Entity>(this.address + 4);
			}
		}
	}
}

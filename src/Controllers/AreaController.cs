using System;
using PoeHUD.Models;
using PoeHUD.Poe;
using PoeHUD.Poe.RemoteMemoryObjects;

namespace PoeHUD.Controllers
{
	public class AreaController
	{

		private readonly GameController Root;

		public AreaController(GameController gameController)
		{
			Root = gameController;
		}

		public event Action<AreaController> OnAreaChange; // TODO rename to AreaChanging

		public AreaInstance CurrentArea { get; private set; }
        
		public void RefreshState()
		{
			var igsd = Root.Game.IngameState.Data;
			AreaTemplate clientsArea = igsd.CurrentArea;
			int curAreaHash = igsd.CurrentAreaHash;

			if (CurrentArea != null && curAreaHash == CurrentArea.Hash)
				return;

            CurrentArea = new AreaInstance(clientsArea, curAreaHash, igsd.CurrentAreaLevel);
		    OnAreaChange?.Invoke(this);
		}
	}
}

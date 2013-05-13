using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.Rule;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class RuleBasedMovementModule: MovementBlockingModule
	{
		private BlockingModule<XmasEntity> block = new BlockingModule<XmasEntity>();
		
		
		public RuleBasedMovementModule()
		{
			
		}


		public override bool IsMovementBlocking(XmasEntity entity)
		{
			return block.IsBlocking(entity);
		}

		public void AddWillBLockRule<TDecider>(Predicate<XmasEntity> rule)
		{
			this.block.AddWillBlockRule<TDecider>(rule);
		}

		public void AddWillNotBLockRule<TDecider>(Predicate<XmasEntity> rule)
		{
			this.block.AddNotBlockRule<TDecider>(rule);
		}

		public void AddNewRuleLayer<TDecider>()
		{
			this.block.AddNewRuleLayer<TDecider>();
		}

	}
}
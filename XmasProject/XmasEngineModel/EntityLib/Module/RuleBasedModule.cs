using System;
using XmasEngineModel.Rule;

namespace XmasEngineModel.EntityLib.Module
{
	public abstract class RuleBasedModule<TEntity> : EntityModule where TEntity : XmasEntity
	{
		private RuleHierarchy<Type,TEntity> ruleHierarchy = new RuleHierarchy<Type, TEntity>();
 

		protected void AddRule(Type toLayer, Predicate<TEntity> rule, Conclusion conclusion)
		{
			TransformationRule<TEntity> tr;

			if (ruleHierarchy.TryGetRule(toLayer, out tr))
			{
				tr.AddPremise(rule, conclusion);
			}
		}


		protected void PushRuleLayer(Type layer)
		{
			ruleHierarchy.AddLayer(layer, new TransformationRule<TEntity>());
		}

		
	}
}
using System;
using System.Collections.Generic;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;
using XmasEngineModel.Rule;
using XmasEngineModel.World;

namespace XmasEngineModel.EntityLib
{
	public abstract class XmasEntity : XmasActor
	{
		private const int DEFAULT_VISION = 4;
		private Conclusion[] conclusions = new Conclusion[2];
		private RuleHierarchy<Type, XmasEntity> movementRules = new RuleHierarchy<Type, XmasEntity>();
		private TriggerManager triggers = new TriggerManager();
		private LinkedList<Predicate<XmasEntity>> visionBlockRules = new LinkedList<Predicate<XmasEntity>>();

		public XmasEntity()
		{
			conclusions[0] = new Conclusion("Non blocking");
			conclusions[1] = new Conclusion("Blocking");
		}

		public virtual int VisionRange
		{
			get { return DEFAULT_VISION; }
		}

		public XmasPosition Position
		{
			get { return World.GetEntityPosition(this); }
		}

		internal event EventHandler<XmasEvent> TriggerRaised;

		protected void AddRuleSuperior<Superior>() where Superior : XmasEntity
		{
			movementRules.AddLayer(typeof (Superior), new TransformationRule<XmasEntity>());
		}


		private void addrule(RuleHierarchy<Type, XmasEntity> h, Predicate<XmasEntity> p, Conclusion c, Type member)
		{
			TransformationRule<XmasEntity> rules;
			if (h.TryGetRule(member, out rules))
			{
				rules.AddPremise(p, c);
			}
			else
				throw new EntityException(this,
				                          "Rules have not been initialized, for this member: \"" + member.Name +
				                          "\". Use AddRuleSuperior Method to add this member in the decision tree");
		}

		protected void AddWillNotBlock_MovementRule<Member>(Predicate<XmasEntity> otherMember) where Member : XmasEntity
		{
			addrule(movementRules, otherMember, conclusions[0], typeof (Member));
		}

		protected void AddWillBlock_MovementRule<Member>(Predicate<XmasEntity> otherMember) where Member : XmasEntity
		{
			addrule(movementRules, otherMember, conclusions[1], typeof (Member));
		}


		public bool IsMovementBlocking(XmasEntity xmasEntity)
		{
			Conclusion c = movementRules.Conclude(xmasEntity);

			if (c == conclusions[0])
				return false;
			if (c == conclusions[1])
				return true;

			//If the object dont care it will not be blocking
			return false;
		}


		public virtual bool IsVisionBlocking(XmasEntity xmasEntity)
		{
			return false;
		}

		public void Register(Trigger trigger)
		{
			lock (this)
			{
				triggers.Register(trigger);
			}
		}

		public void Deregister(Trigger trigger)
		{
			lock (this)
			{
				triggers.Deregister(trigger);
			}
		}

		public void QueueAction(EntityXmasAction action)
		{
			if (action.IsEntitySupported(this))
			{
				action.Source = this;
				ActionManager.QueueAction(action);
			}
			else
				throw new UnacceptableActionException(action, this);
		}

		public void Raise(XmasEvent evt)
		{
			lock (this)
			{
				triggers.Raise(evt);
				if (TriggerRaised != null)
					TriggerRaised(this, evt);
			}
		}

		public ThreadSafeEventQueue ConstructEventQueue()
		{
			return new ThreadSafeEventQueue(triggers);
		}
	}
}
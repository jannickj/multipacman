﻿using NUnit.Framework;
using XmasEngineModel.Entities;
using XmasEngineModel.Entities.Units;
using XmasEngineModel.GameManagement;
using XmasEngineModel.GameManagement.Actions;
using XmasEngineModel.GameManagement.Events;

namespace XmasEngine_Test.Model.GameManagement
{
	[TestFixture]
	public class GameManagerTest
	{
		[Test]
		public void AddEventToUnit_containMultiTrigger_TheAddedEventGetsFired()
		{
			EventManager gem = new EventManager();

			Agent A = new Agent();

			gem.AddEntity(A);

			bool eventfired = false;

			MultiTrigger mt = new MultiTrigger();

			A.Register(mt);

			mt.AddAction<GameEvent>(e => eventfired = true);

			mt.RegisterEvent<UnitTakesDamagePostEvent>();

			A.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

			Assert.IsTrue(eventfired);
		}

		[Test]
		public void AddTrigger_triggerIsAddedToUnitAfterItIsAddedToManagger_EventFired()
		{
			EventManager gem = new EventManager();
			ActionManager actman = new ActionManager();

			Agent A = new Agent();
			Agent B = new Agent();

			A.ActionManager = actman;
			B.ActionManager = actman;

			int dmg = int.MaxValue;

			bool eventFired = false;


			Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => eventFired = true);
			EntityGameAction ga1 = new DamageUnitTarget(B, dmg);

			gem.AddEntity(B);
			B.Register(T);

			A.QueueAction(ga1);
			actman.ExecuteActions();

			Assert.IsTrue(eventFired);
		}

		[Test]
		public void ExecuteActionWithGlobalTrigger_UnitDealsDamageToAnotherUnitWithDamage_EventsWasFiredOnBothActions()
		{
			EventManager gem = new EventManager();
			ActionManager actman = new ActionManager();

			Agent A = new Agent();
			Agent B = new Agent();

			A.ActionManager = actman;
			B.ActionManager = actman;

			int dmg = int.MaxValue;

			int actualTimesFired = 0;


			Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => actualTimesFired++);
			EntityGameAction ga1 = new DamageUnitTarget(B, dmg);
			EntityGameAction ga2 = new DamageUnitTarget(A, dmg);

			gem.Register(T);
			gem.AddEntity(A);
			gem.AddEntity(B);

			A.QueueAction(ga1);
			B.QueueAction(ga2);
			actman.ExecuteActions();

			int expectedTimeFired = 2;

			Assert.AreEqual(expectedTimeFired, actualTimesFired);
		}

		[Test]
		public void
			ExecuteActionWithSpecificTargetEvent_UnitDealsDamageToAnotherUnitWithDamagePrevetionImplemented_TheTargetUnitTakesNoDamage
			()
		{
			EventManager gem = new EventManager();
			ActionManager actman = new ActionManager();

			Agent expectedDealer = new Agent();
			Agent expectedTaker = new Agent();

			expectedDealer.ActionManager = actman;
			expectedTaker.ActionManager = actman;

			int dmg = 10;
			int prevent = 10;
			int expectedDmg = 10;

			//ignore initialization values
			Unit actualDealer = null;
			Unit actualTaker = null;
			int actualDmg = new int();


			Trigger preT = new Trigger<UnitTakesDamagePreEvent>(e => e.ModDmgPreMultiplier(-prevent));
			Trigger postT = new Trigger<UnitTakesDamagePostEvent>(e =>
				{
					actualDealer = e.Source;
					actualTaker = e.Target;
					actualDmg = e.Damage;
				});
			EntityGameAction ga = new DamageUnitTarget(expectedTaker, dmg);

			expectedTaker.Register(preT);
			expectedTaker.Register(postT);
			gem.AddEntity(expectedTaker);

			expectedDealer.QueueAction(ga);
			actman.ExecuteActions();

			Assert.AreEqual(expectedDealer, actualDealer);
			Assert.AreEqual(expectedTaker, actualTaker);
			Assert.AreEqual(expectedDmg, actualDmg);
		}

		[Test]
		public void ExecuteActionWithSpecificTargetEvent_UnitDealsDamageToAnotherUnit_TheOtherUnitTakesDamage()
		{
			EventManager gem = new EventManager();
			ActionManager actman = new ActionManager();


			Agent expectedDealer = new Agent();
			Agent expectedTaker = new Agent();

			expectedDealer.ActionManager = actman;
			expectedTaker.ActionManager = actman;
			int expectedDmg = 10;

			//ignore initialization values
			Unit actualDealer = null;
			Unit actualTaker = null;
			int actualDmg = new int();

			Trigger t = new Trigger<UnitTakesDamagePostEvent>(e =>
				{
					actualDealer = e.Source;
					actualTaker = e.Target;
					actualDmg = e.Damage;
				});
			DamageUnitTarget ga = new DamageUnitTarget(expectedTaker, expectedDmg);

			expectedTaker.Register(t);
			gem.AddEntity(expectedTaker);

			expectedDealer.QueueAction(ga);
			actman.ExecuteActions();

			Assert.AreEqual(expectedDealer, actualDealer);
			Assert.AreEqual(expectedTaker, actualTaker);
			Assert.AreEqual(expectedDmg, actualDmg);
		}

		[Test]
		public void RemoveEventFromUnit_containMultiTrigger_TheEventGetsRemovedAndIsNotFired()
		{
			EventManager gem = new EventManager();

			Agent A = new Agent();

			gem.AddEntity(A);

			bool eventfired = false;

			MultiTrigger mt = new MultiTrigger();
			mt.AddAction<GameEvent>(e => eventfired = true);
			mt.RegisterEvent<UnitTakesDamagePostEvent>();

			A.Register(mt);

			mt.DeregisterEvent<UnitTakesDamagePostEvent>();


			A.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

			Assert.IsFalse(eventfired);
		}

		[Test]
		public void RemoveTrigger_simpleGlobalTrigger_NoEventFired()
		{
			EventManager gem = new EventManager();
			ActionManager actman = new ActionManager();

			Agent A = new Agent();
			Agent B = new Agent();

			A.ActionManager = actman;
			B.ActionManager = actman;

			int dmg = int.MaxValue;

			bool eventFired = false;


			Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => eventFired = true);
			EntityGameAction ga1 = new DamageUnitTarget(B, dmg);


			gem.Register(T);
			gem.Deregister(T);

			A.QueueAction(ga1);

			Assert.IsFalse(eventFired);
		}

		[Test]
		public void RemoveTrigger_triggerIsRemovedFromUnit_NoEventFired()
		{
			EventManager gem = new EventManager();
			ActionManager actman = new ActionManager();

			Agent A = new Agent();
			Agent B = new Agent();

			A.ActionManager = actman;
			B.ActionManager = actman;

			int dmg = int.MaxValue;

			bool eventFired = false;


			Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => eventFired = true);
			EntityGameAction ga1 = new DamageUnitTarget(B, dmg);


			B.Register(T);
			gem.AddEntity(B);

			B.Deregister(T);

			A.QueueAction(ga1);

			Assert.IsFalse(eventFired);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Drawing;
using GameEngine;
using GameEngine.ActionManagement;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Actions;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;
using GooseEngine.Entities;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using GooseEngine.Enum;
using NUnit.Framework;
using GooseEngine.Interfaces;


namespace GooseEngine_Test.GameManagement
{
    [TestFixture]
    public class GameManagerTest
    {
        
        [Test]
        public void ExecuteActionWithSpecificTargetEvent_UnitDealsDamageToAnotherUnit_TheOtherUnitTakesDamage()
        {
            
            IGameManager gem = new GameManager();
            Agent expectedDealer = new Agent();
            Agent expectedTaker = new Agent();
            int expectedDmg = 10;
            
            //ignore initialization values
            Unit actualDealer = null;
            Unit actualTaker = null;
            int actualDmg = new int();

            Trigger t = new Trigger<UnitTakesDamagePostEvent>(e => { actualDealer = e.Source; actualTaker = e.Target; actualDmg = e.Damage; });
            GameAction ga = new DamageUnitTarget(expectedDealer, expectedTaker, expectedDmg);
            
            expectedTaker.Register(t);
            gem.AddEntity(expectedTaker);

            gem.Execute(ga);

            Assert.AreEqual(expectedDealer, actualDealer);
            Assert.AreEqual(expectedTaker, actualTaker);
            Assert.AreEqual(expectedDmg, actualDmg);
            
        }

        [Test]
        public void ExecuteActionWithSpecificTargetEvent_UnitDealsDamageToAnotherUnitWithDamagePrevetionImplemented_TheTargetUnitTakesNoDamage()
        {
            IGameManager gem = new GameManager();
            Agent expectedDealer = new Agent();
            Agent expectedTaker = new Agent();
            int dmg = 10;
            int prevent = 10;
            int expectedDmg = 10;

            //ignore initialization values
            Unit actualDealer = null;
            Unit actualTaker = null;
            int actualDmg = new int();


            Trigger preT = new Trigger<UnitTakesDamagePreEvent>(e => e.ModDmgPreMultiplier(-prevent));
            Trigger postT = new Trigger<UnitTakesDamagePostEvent>(e => { actualDealer = e.Source; actualTaker = e.Target; actualDmg = e.Damage; });
            GameAction ga = new DamageUnitTarget(expectedDealer, expectedTaker, dmg);

            expectedTaker.Register(preT);
            expectedTaker.Register(postT);
            gem.AddEntity(expectedTaker);

            gem.Execute(ga);

            Assert.AreEqual(expectedDealer, actualDealer);
            Assert.AreEqual(expectedTaker, actualTaker);
            Assert.AreEqual(expectedDmg, actualDmg);

        }

        [Test]
        public void ExecuteActionWithGlobalTrigger_UnitDealsDamageToAnotherUnitWithDamage_EventsWasFiredOnBothActions()
        {
            IGameManager gem = new GameManager();
            Agent A = new Agent();
            Agent B = new Agent();
            int dmg = int.MaxValue;

            int actualTimesFired = 0;


            Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => actualTimesFired++);
            GameAction ga1 = new DamageUnitTarget(A, B, dmg);
            GameAction ga2 = new DamageUnitTarget(B, A, dmg);
            
            gem.Register(T);
            gem.AddEntity(A);
            gem.AddEntity(B);

            gem.Execute(ga1);
            gem.Execute(ga2);

            int expectedTimeFired = 2;

            Assert.AreEqual(expectedTimeFired, actualTimesFired);

        }

        [Test]
        public void RemoveTrigger_simpleGlobalTrigger_NoEventFired()
        {
            IGameManager gem = new GameManager();
            Agent A = new Agent();
            Agent B = new Agent();
            int dmg = int.MaxValue;

            bool eventFired = false;


            Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => eventFired = true);
            GameAction ga1 = new DamageUnitTarget(A, B, dmg);


            gem.Register(T);
            gem.Deregister(T);

            gem.Execute(ga1);

            Assert.IsFalse(eventFired);

        }

        [Test]
        public void RemoveTrigger_triggerIsRemovedFromUnit_NoEventFired()
        {
            IGameManager gem = new GameManager();
            Agent A = new Agent();
            Agent B = new Agent();
            int dmg = int.MaxValue;

            bool eventFired = false;


            Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => eventFired = true);
            GameAction ga1 = new DamageUnitTarget(A, B, dmg);


            B.Register(T);
            gem.AddEntity(B);

            B.Deregister(T);

            gem.Execute(ga1);

            Assert.IsFalse(eventFired);

        }


        [Test]
        public void AddTrigger_triggerIsAddedToUnitAfterItIsAddedToManagger_EventFired()
        {
            IGameManager gem = new GameManager();
            Agent A = new Agent();
            Agent B = new Agent();
            int dmg = int.MaxValue;

            bool eventFired = false;


            Trigger T = new Trigger<UnitTakesDamagePostEvent>(_ => eventFired = true);
            GameAction ga1 = new DamageUnitTarget(A, B, dmg);

            gem.AddEntity(B);
            B.Register(T);

            gem.Execute(ga1);

            Assert.IsTrue(eventFired);
        }

        [Test]
        public void AddEventToUnit_containMultiTrigger_TheAddedEventGetsFired()
        {

            GameManager gem = new GameManager();
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
        public void RemoveEventFromUnit_containMultiTrigger_TheEventGetsRemovedAndIsNotFired()
        {

            GameManager gem = new GameManager();

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




    }
}

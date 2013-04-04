using System;
using System.Collections.Generic;
using GooseEngine;
using GooseEngine.ActionManagement;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Actions;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;
using GooseEngine.Entities;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using GooseEngine.Enum;
using NUnit.Framework;


namespace GooseEngine_Test.GameManagement
{
    [TestFixture]
    public class TriggerManagerTest
    {

        [Test]
        public void AddEvent_containMultiTrigger_TheAddedEventGetsFired()
        {

            TriggerManager tm = new TriggerManager();

            bool eventfired = false;

            MultiTrigger mt = new MultiTrigger();

            tm.Register(mt);

            mt.AddAction<GameEvent>(e => eventfired = true);

            mt.RegisterEvent<UnitTakesDamagePostEvent>();

            tm.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

            Assert.IsTrue(eventfired);

        }

        [Test]
        public void RemoveEvent_containMultiTrigger_TheEventGetsRemovedAndIsNotFired()
        {

            TriggerManager tm = new TriggerManager();

            bool eventfired = false;

            MultiTrigger mt = new MultiTrigger();
            mt.AddAction<GameEvent>(e => eventfired = true);
            mt.RegisterEvent<UnitTakesDamagePostEvent>();

            tm.Register(mt);

            mt.DeregisterEvent<UnitTakesDamagePostEvent>();
            

            tm.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

            Assert.IsFalse(eventfired);

        }

        [Test]
        public void RemoveAction_containMultiTrigger_TheActionIsNotFired()
        {

            TriggerManager tm = new TriggerManager();

            bool actionfired = false;

            MultiTrigger mt = new MultiTrigger();

            Action<UnitTakesDamagePostEvent> action =(e => actionfired = true);
            mt.AddAction<UnitTakesDamagePostEvent>(action);
            mt.RegisterEvent<UnitTakesDamagePostEvent>();

            tm.Register(mt);

            mt.RemoveAction(action);


            tm.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

            Assert.IsFalse(actionfired);

        }

        [Test]
        public void AddAction_containMultiTrigger_TheActionIsNotFired()
        {

            TriggerManager tm = new TriggerManager();

            bool actionfired = false;

            MultiTrigger mt = new MultiTrigger();

            Action<UnitTakesDamagePostEvent> action = (e => actionfired = true);
           
            mt.RegisterEvent<UnitTakesDamagePostEvent>();

            tm.Register(mt);

            mt.AddAction<UnitTakesDamagePostEvent>(action);

            tm.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

            Assert.IsTrue(actionfired);

        }

        [Test]
        public void AddCondition_containMultiTrigger_TheActionIsNotFired()
        {

            TriggerManager tm = new TriggerManager();

            bool actionfired = false;

            MultiTrigger mt = new MultiTrigger();

            mt.AddCondition<GameEvent>(e => false);

            Action<UnitTakesDamagePostEvent> action = (e => actionfired = true);

            mt.RegisterEvent<UnitTakesDamagePostEvent>();

            tm.Register(mt);

            mt.AddAction<UnitTakesDamagePostEvent>(action);

            tm.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

            Assert.IsFalse(actionfired);

        }

        [Test]
        public void RemoveCondition_containMultiTrigger_TheActionIsFired()
        {

            TriggerManager tm = new TriggerManager();

            bool actionfired = false;

            MultiTrigger mt = new MultiTrigger();

            Predicate<UnitTakesDamagePostEvent> cond =(e => false);
            mt.AddCondition<UnitTakesDamagePostEvent>(cond);
            mt.AddAction<GameEvent>(e => actionfired = true);
            mt.RegisterEvent<UnitTakesDamagePostEvent>();

            tm.Register(mt);

            mt.RemoveCondition(cond);

            tm.Raise(new UnitTakesDamagePostEvent(null, null, 0, 0));

            Assert.IsTrue(actionfired);

        }

    }
}
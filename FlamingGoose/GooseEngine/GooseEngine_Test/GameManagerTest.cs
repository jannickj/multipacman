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


namespace GooseEngine_Test
{
    [TestFixture]
    public class GameManagerTest
    {
        
        [Test]
        public void ExecuteDamageUnitTargetAction_UnitDealsDamageToAnotherUnit_TheOtherUnitTakesDamage()
        {
            GameManager gem = new GameManager();
            Agent expectedDealer = new Agent();
            Agent expectedTaker = new Agent();
            int expectedDmg = 10;
            
            //ignore initialization values
            Unit actualDealer = null;
            Unit actualTaker = null;
            int actualDmg = new int();

            Trigger t = new Trigger<UnitTakesDamagePostEvent>(e => { actualDealer = e.Source; actualTaker = e.Target; actualDmg = e.Damage; });
            GameAction ga = new DamageUnitTarget(expectedDealer, expectedTaker, expectedDmg);
            
            gem.Register(t);

            gem.Execute(ga);

            Assert.AreEqual(expectedDealer, actualDealer);
            Assert.AreEqual(expectedTaker, actualTaker);
            Assert.AreEqual(expectedDmg, actualDmg);
            
        }

        [Test]
        public void ExecuteDamageUnitTargetAction_UnitDealsDamageToAnotherUnitWithDamagePrevetionImplemented_TheTargetUnitTakesNoDamage()
        {
            GameManager gem = new GameManager();
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

            gem.Register(preT);
            gem.Register(postT);

            gem.Execute(ga);

            Assert.AreEqual(expectedDealer, actualDealer);
            Assert.AreEqual(expectedTaker, actualTaker);
            Assert.AreEqual(expectedDmg, actualDmg);

        }

    }
}

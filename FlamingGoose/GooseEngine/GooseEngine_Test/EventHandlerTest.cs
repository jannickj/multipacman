using System;
using System.Collections.Generic;
using System.Drawing;
using GameEngine;
using GameEngine.ActionManagement;
using GooseEngine;
using GooseEngine.ActionManagement;
using GooseEngine.ActionManagement.Actions;
using GooseEngine.ActionManagement.Events;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using GooseEngine.Enum;
using NUnit.Framework;


namespace GooseEngine_Test
{
    [TestFixture]
    public class EventHandlerTest
    {
        


        [Test]
        public void Move_AgentInMiddleOfMap_ItMoves()
        {
            GameWorld world = new GameWorld(new GameMap(new Size(2,2)));
            GameEventManager gem = new GameEventManager();
            Agent a = new Agent();
            bool eventfired = false;
            gem.Listen<UnitIsMovingEvent>(typeof(UnitIsMovingEvent), a, e => eventfired = true);

            MoveAction mv = new MoveAction(world, a, new Vector(1, 0), 0);

            gem.Queue(mv);
            Assert.IsTrue(eventfired);
            
            
        }

    }
}

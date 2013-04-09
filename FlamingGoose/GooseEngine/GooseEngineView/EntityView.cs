using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using UnityEngine;

namespace GooseEngineView
{
    public class EntityView
    {
        private GameObject gobj;

        public EntityView(GameObject gobj, Entity entity)
        {
            entity.Register(new Trigger<UnitMovePreEvent>(unit_Move));
           
        }

        private void unit_Move(UnitMovePreEvent obj)
        {
            
            
                
        }
    }
}

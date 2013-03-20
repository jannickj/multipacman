using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Entities.MapEntities
{
    public class Wall : Entity
    {

        public Wall()
        {
            InitializeRuleLayer();
            AddWillBlock_MovementRule(_ => true);

        }



        public override bool IsVisionBlocking(Entity entity)
        {
            return true;
        }
    }
}

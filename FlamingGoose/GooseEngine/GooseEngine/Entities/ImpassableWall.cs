using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Entities.MapEntities
{
    public class ImpassableWall : Entity
    {
        public ImpassableWall()
        {
            AddRuleSuperior<ImpassableWall>();
            AddWillBlock_MovementRule<ImpassableWall>(_ => true);

        }
    }
}

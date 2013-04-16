using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;
using GooseEngine.Data;
using GooseEngine.Entities;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;

namespace GooseEngineView.Testing.ConsoleView
{
    public abstract class ConsoleEntityView
    {
		private Entity model;
		protected Point position;

		public abstract char Symbol { get; }

		public Entity Model { 
			get { return model; }
		}

		public Point Position {
			get { return position; }
		}

        public ConsoleEntityView(Entity model)
        {
			this.model = model;
			this.position = model.Position;
			model.Register(new Trigger<UnitMovePostEvent>( ))
        }

		protected virtual void UnitMoved(UnitMovePostEvent evt)
		{
			position = evt.NewPos;
		}
    }
}

using System;
using XmasEngineView;
using XmasEngineModel.World;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineExtensions.TileExtension.Events;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using System.IO;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineExtensions;

namespace ConsoleXmasImplementation.ConsoleLogger
{
	public class LoggerEntityView : EntityView
	{
		private Point pos;
		private Logger logstream;

		public LoggerEntityView (XmasEntity model 
		                       , ThreadSafeEventManager evtman 
		                       , Logger logstream
		) : base(model, evtman)
		{
			this.logstream = logstream;
			Position = model.Position;
			eventqueue.Register (new Trigger<UnitMovePostEvent> (loggerEntityView_UnitMovePostEvent));
			eventqueue.Register (new Trigger<UnitMovePreEvent> (loggerEntityView_UnitMovePreEvent));
		}

		private void loggerEntityView_UnitMovePostEvent (UnitMovePostEvent evt)
		{
			if (pos.Equals (evt.NewPos))
				return;

			string info = String.Format ("{{{0}}} finished moving from {1} to {2}", model, pos, evt.NewPos);
			logstream.LogStringWithTimeStamp (info, DebugLevel.Info);
			pos = evt.NewPos;
		}

		private void loggerEntityView_UnitMovePreEvent (UnitMovePreEvent evt)
		{
			if (pos.Equals (evt.NewPos))
				return;

			string info = String.Format("{{{0}}} started moving from {1} to {2}", model, pos, evt.NewPos);
			logstream.LogStringWithTimeStamp (info, DebugLevel.All);
		}

		#region implemented abstract members of EntityView

		public override XmasPosition Position {
			get { return new TilePosition (pos); }
			protected set { this.pos = ((TilePosition)value).Point; }
		}

		#endregion
	}
}


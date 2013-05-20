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

		public LoggerEntityView( XmasEntity model 
		                       , ThreadSafeEventManager evtman 
		                       , Logger logstream
		                       ) : base(model, evtman)
		{
			this.logstream = logstream;
			Position = model.Position;
			eventqueue.Register(new Trigger<UnitMovePostEvent>(loggerEntityView_UnitMovePostEvent));
		}

		private void loggerEntityView_UnitMovePostEvent(UnitMovePostEvent evt)
		{
			//TODO: add ID, name? when implmented
			logstream.LogStringWithTimeStamp (String.Format ("Unit moved from {0} to {1}", pos, evt.NewPos), 
			                                  DebugLevel.AllInformation
			                                  );
			pos = evt.NewPos;
		}

		#region implemented abstract members of EntityView

		public override XmasPosition Position
		{
			get { return new TilePosition(pos); }
			protected set { this.pos = ((TilePosition)value).Point; }
		}

		#endregion
	}
}


using System;
using System.Collections.Generic;

namespace XmasEngineModel.GameManagement.Interfaces
{
	public interface ITrigger
	{
		ICollection<Type> Events { get; }
	}
}
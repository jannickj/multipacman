using System;
using System.Collections.Generic;

namespace GooseEngine.GameManagement.Interfaces
{
	public interface ITrigger
	{
		ICollection<Type> Events { get; }
	}
}
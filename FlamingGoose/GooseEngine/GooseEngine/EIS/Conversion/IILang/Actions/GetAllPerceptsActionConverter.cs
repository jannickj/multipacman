using System;
using GooseEngine.EIS.Conversion.IILang;
using GooseEngine.GameManagement.Actions;

namespace GooseEngine
{
	public class GetAllPerceptsActionConverter : EISActionConverter<GetAllPercepts>
	{
		public GetAllPerceptsActionConverter ()
		{
		}

		#region implemented abstract members of GooseConverter
		public override GetAllPercepts BeginConversionToGoose (iilang.IILElement fobj)
		{
			return new GetAllPercepts();
		}
		#endregion
	}
}


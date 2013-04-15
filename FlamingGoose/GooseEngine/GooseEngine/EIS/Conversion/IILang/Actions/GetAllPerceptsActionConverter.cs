using System;
using GooseEngine.EIS.ActionTypes;
using GooseEngine.EIS.Conversion.IILang;
using GooseEngine.GameManagement.Actions;

namespace GooseEngine.EIS.Conversion.IILang.Actions
{
	public class GetAllPerceptsActionConverter : EISActionConverter<GetAllPercepts,EISGetAllPercepts>
	{
		public GetAllPerceptsActionConverter ()
		{
		}

        public override GetAllPercepts BeginConversionToGoose(EISGetAllPercepts fobj)
        {
            return new GetAllPercepts();
        }
    }
}


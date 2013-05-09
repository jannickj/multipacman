using System;
using JSLibrary.Data;
using JSLibrary.IiLang.Parameters;
using XmasEngineExtensions.EisExtension.Model.ActionTypes;
using XmasEngineModel.Management.Actions;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Actions
{
	public class MoveUnitActionConverter : EISActionConverter<MoveUnit, EISMoveUnit>
	{
		#region implemented abstract members of XmasConverter

		public override MoveUnit BeginConversionToGoose(EISMoveUnit fobj)
		{
			IilNumeral x_num = (IilNumeral) fobj.Parameters[0];
			IilNumeral y_num = (IilNumeral) fobj.Parameters[1];
			int x = Convert.ToInt32(x_num.Value);
			int y = Convert.ToInt32(y_num.Value);

			return new MoveUnit(new Vector(x, y));
		}

		#endregion
	}
}

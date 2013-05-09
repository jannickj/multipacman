using System;
using GooseEISExtension.Model.ActionTypes;
using GooseEngine.GameManagement.Actions;
using JSLibrary.Data;
using iilang.Parameters;

namespace GooseEISExtension.Model.Conversion.IILang.Actions
{
	public class MoveUnitActionConverter : EISActionConverter<MoveUnit, EISMoveUnit>
	{
		#region implemented abstract members of GooseConverter

		public override MoveUnit BeginConversionToGoose(EISMoveUnit fobj)
		{
			IILNumeral x_num = (IILNumeral) fobj.Parameters[0];
			IILNumeral y_num = (IILNumeral) fobj.Parameters[1];
			int x = Convert.ToInt32(x_num.Value);
			int y = Convert.ToInt32(y_num.Value);

			return new MoveUnit(new Vector(x, y));
		}

		#endregion
	}
}
/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ITakeDamage.cs
 * Date Created: October 22, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

namespace TrenchWars
{
	public interface ITakeDamage
	{
        //METHODS
        #region Public Methods: For External Interactions

        public void TakeDamage(float damage);
		public void HealDamage(float heal);

		#endregion
	}
}
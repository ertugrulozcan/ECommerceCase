using System.Collections.Generic;

namespace ECommerceCase.Core.Models.Categorization
{
	public class EmtiaCategory : EntityBase
	{
		#region Fields

		private IEnumerable<EmtiaCategory> subCategories;

		#endregion
		
		#region Properties

		public string Name { get; set; }
		
		public EmtiaCategory ParentCategory { get; set; }

		public IEnumerable<EmtiaCategory> SubCategories
		{
			get
			{
				return this.subCategories;
			}

			set
			{
				this.subCategories = value;
				if (this.subCategories != null)
				{
					foreach (var subCategory in this.subCategories)
					{
						subCategory.ParentCategory = this;
					}
				}
			}
		}

		#endregion
	}
}
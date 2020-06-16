using System;
using System.Collections.Generic;
using System.Linq;
using EasyCaching.Core;
using ECommerceCase.Core.Models.Shopping;
using ECommerceCase.Infrastructure.Dao;

namespace ECommerceCase.OMS.Dao.Redis
{
	/// <summary>
	/// Redis Implementation
	/// </summary>
	public class ShoppingCartRepository : IRepository<ShoppingCart>
	{
		#region Services

		private readonly IEasyCachingProvider easyCachingProvider;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		protected ShoppingCartRepository(IEasyCachingProviderFactory easyCachingProviderFactory)
		{
			this.easyCachingProvider = easyCachingProviderFactory.GetCachingProvider("ShoppingCartsDistrubutedCacheChannel");
		}

		#endregion
		
		#region Methods

		public ShoppingCart Select(int id)
		{
			return this.easyCachingProvider.Get<ShoppingCart>(id.ToString())?.Value;
		}
		
		public IEnumerable<ShoppingCart> Select()
		{
			return this.easyCachingProvider.Get<IEnumerable<ShoppingCart>>("all")?.Value;
		}

		public IEnumerable<ShoppingCart> Select(int? skip, int? limit, out int totalCount)
		{
			totalCount = 0;
			if (skip != null && limit != null)
			{
				return this.easyCachingProvider.Get<IEnumerable<ShoppingCart>>("all")?.Value.Skip(skip.Value).Take(limit.Value);	
			}
			else if (skip != null)
			{
				return this.easyCachingProvider.Get<IEnumerable<ShoppingCart>>("all")?.Value.Skip(skip.Value);	
			}
			else if (limit != null)
			{
				return this.easyCachingProvider.Get<IEnumerable<ShoppingCart>>("all")?.Value.Take(limit.Value);	
			}
			else
			{
				return this.easyCachingProvider.Get<IEnumerable<ShoppingCart>>("all")?.Value;	
			}
		}

		public void Insert(IEnumerable<ShoppingCart> entities)
		{
			foreach (var entity in entities)
			{
				this.easyCachingProvider.Set(entity.UserId, entity, TimeSpan.FromHours(24));	
			}
		}

		public void Insert(ShoppingCart entity)
		{
			this.easyCachingProvider.Set(entity.UserId, entity, TimeSpan.FromHours(24));
		}

		public IEnumerable<ShoppingCart> Where(Func<ShoppingCart, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ShoppingCart> Where(Func<ShoppingCart, bool> predicate, int? skip, int? limit, out int totalCount)
		{
			throw new NotImplementedException();
		}

		public void Update(ShoppingCart entity)
		{
			this.easyCachingProvider.Set(entity.UserId, entity, TimeSpan.FromHours(24));
		}
		
		#endregion
	}
}
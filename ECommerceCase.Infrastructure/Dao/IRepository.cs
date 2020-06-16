using System;
using System.Collections.Generic;
using ECommerceCase.Core.Models;

namespace ECommerceCase.Infrastructure.Dao
{
	public interface IRepository<TEntity> where TEntity : EntityBase
	{
		TEntity Select(int id);
		
		IEnumerable<TEntity> Select();
		
		IEnumerable<TEntity> Select(int? skip, int? limit, out int totalCount);
		
		void Insert(IEnumerable<TEntity> entities);
		
		void Insert(TEntity entity);
		
		IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);
		
		IEnumerable<TEntity> Where(Func<TEntity, bool> predicate, int? skip, int? limit, out int totalCount);

		void Update(TEntity entity);
	}
}
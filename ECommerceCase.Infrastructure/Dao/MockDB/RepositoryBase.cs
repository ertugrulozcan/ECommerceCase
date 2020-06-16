using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceCase.Core.Models;

namespace ECommerceCase.Infrastructure.Dao.MockDB
{
	public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
	{
		#region Properties

		private List<TEntity> EntityList { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		protected RepositoryBase()
		{
			this.EntityList = new List<TEntity>();
		}

		#endregion
		
		#region Methods

		public virtual TEntity Select(int id)
		{
			return this.EntityList.FirstOrDefault(x => x.Id == id);
		}
		
		public virtual IEnumerable<TEntity> Select()
		{
			return this.EntityList.ToList();
		}

		public virtual IEnumerable<TEntity> Select(int? skip, int? limit, out int totalCount)
		{
			IEnumerable<TEntity> result;
			
			if (skip != null && limit != null)
			{
				result = this.EntityList.Skip(skip.Value).Take(limit.Value);	
			}
			else if (skip != null)
			{
				result = this.EntityList.Skip(skip.Value);
			}
			else if (limit != null)
			{
				result = this.EntityList.Take(limit.Value);
			}
			else
			{
				result = this.EntityList;
			}

			totalCount = this.EntityList.Count;
			return result;
		}

		public virtual void Insert(IEnumerable<TEntity> entities)
		{
			this.EntityList.AddRange(entities);
		}

		public virtual void Insert(TEntity entity)
		{
			this.EntityList.Add(entity);
		}

		public virtual IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
		{
			return this.EntityList.Where(predicate);
		}

		public virtual IEnumerable<TEntity> Where(Func<TEntity, bool> predicate, int? skip, int? limit, out int totalCount)
		{
			var result = this.EntityList.Where(predicate);
			totalCount = result.Count();
			
			if (skip != null && limit != null)
			{
				return result.Skip(skip.Value).Take(limit.Value);	
			}
			else if (skip != null)
			{
				return result.Skip(skip.Value);
			}
			else if (limit != null)
			{
				return result.Take(limit.Value);
			}
			else
			{
				return result;
			}
		}

		public virtual void Update(TEntity entity)
		{
			var found = this.EntityList.FirstOrDefault(x => x.Id == entity.Id);
			int index = this.EntityList.IndexOf(found);
			this.EntityList.Remove(found);
			this.EntityList.Insert(index, entity);
		}
		
		#endregion
	}
}
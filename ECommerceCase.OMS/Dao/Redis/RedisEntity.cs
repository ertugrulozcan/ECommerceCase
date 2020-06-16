using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using StackExchange.Redis.Extensions.Core;

namespace ECommerceCase.OMS.Dao.Redis
{
	public class RedisEntity<TEntity> : ISerializer
	{
		#region Properties

		public TEntity Payload { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor 1
		/// </summary>
		public RedisEntity()
		{
			
		}
		
		/// <summary>
		/// Constructor 2
		/// </summary>
		public RedisEntity(TEntity payload)
		{
			this.Payload = payload;
		}

		#endregion
		
		#region ISerializer Implementation

		public byte[] Serialize(object data)
		{
			MemoryStream streamMemory = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(streamMemory, data);
			return streamMemory.GetBuffer();
		}

		public T Deserialize<T>(byte[] byteArray)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			MemoryStream ms = new MemoryStream(byteArray);
			return (T) formatter.Deserialize(ms);
		}

		#endregion
	}
}
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DX.Data
{
	 public class EntityFrameworkObjectDataSource<TDbContext> /*: IDisposable*/
		  where TDbContext : DbContext, new()
	 {
		  private bool disposed = false;
		  private readonly TDbContext context;

		  #region Constructor

		  public EntityFrameworkObjectDataSource()
		  {
				context = new TDbContext();
		  }

		  #endregion
		  /*
		  #region Disposable Pattern

		  public void Dispose()
		  {
				Dispose(true);
				GC.SuppressFinalize(this);
		  }

		  protected virtual void Dispose(bool disposing)
		  {
				if (!disposed)
				{
					 if (disposing)
					 {
						  context.Dispose();
					 }
					 disposed = true;
				}
		  }
		  #endregion
		  */
		  protected TDbContext DBContext { get { return context; } }
	 }
	 //[DataObject(true)]
	 public abstract class EntityFrameworkObjectDataSource<TDbContext, TEntity> : EntityFrameworkObjectDataSource<TDbContext>
		  where TDbContext : DbContext, new()
	 {
		  //[DataObjectMethod(DataObjectMethodType.Insert, false)]
		  public abstract void Insert(TEntity item);
		  public abstract bool ValidateInsert(TEntity item);
		  //[DataObjectMethod(DataObjectMethodType.Update, false)]
		  public abstract void Update(TEntity item);
		  public abstract bool ValidateUpdate(TEntity item);
		  //[DataObjectMethod(DataObjectMethodType.Delete, false)]
		  public abstract void Delete(TEntity item);
		  public abstract bool ValidateDelete(TEntity item);
	 }
}

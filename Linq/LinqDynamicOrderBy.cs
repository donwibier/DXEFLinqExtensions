using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DX.Data.Linq
{
	 static class LinqDynamicOrderBy
	 {
		  /// <summary>
		  /// <para>This extension method enables proper sorting expressions to be created at runtime by
		  /// specifying the proper boxing conversion for E.F. based columns.</para>
		  /// <para>It will solve issue like:</para>
		  /// <para>Unable to cast the type 'System.Int32' to type 'System.Object'. 
		  /// LINQ to Entities only supports casting EDM primitive or enumeration types.</para>
		  /// <para>Source:</para>
		  /// <para>
		  ///    <see cref="http://stackoverflow.com/questions/7265186/how-do-i-specify-the-linq-orderby-argument-dynamically">
		  ///       stackoverflow: How do I specify the Linq OrderBy argument dynamically?
		  ///    </see>
		  /// </para>
		  /// </summary>
		  /// <typeparam name="TEntity">Entity type</typeparam>
		  /// <param name="source">The IQueryable instance to sort</param>
		  /// <param name="orderByProperty">the name of the property to orderBy</param>
		  /// <param name="sortDescending">When true, does Descending sorting otherwise Ascending</param>
		  /// <returns>IQueryable&lt;TEntity&gt; including the sort expression</returns>
		  public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool sortDescending)
		  {
				string command = sortDescending ? "OrderByDescending" : "OrderBy";
				Type type = typeof(TEntity);
				PropertyInfo property = type.GetProperty(orderByProperty);
				var parameter = Expression.Parameter(type, "p");
				var propertyAccess = Expression.MakeMemberAccess(parameter, property);
				var orderByExpression = Expression.Lambda(propertyAccess, parameter);
				var resultExpression = Expression.Call(typeof(Queryable), command,
										  new Type[] { type, property.PropertyType },
														source.Expression, Expression.Quote(orderByExpression));
				return source.Provider.CreateQuery<TEntity>(resultExpression);
		  }
		  /// <summary>
		  /// Gets an ascending sort expression based on the orderByProperty Name
		  /// </summary>
		  /// <typeparam name="TEntity">Entity type</typeparam>
		  /// <param name="source">The IQueryable instance to sort</param>
		  /// <param name="orderByProperty">the name of the property to orderBy</param>
		  /// <returns>IQueryable&lt;TEntity&gt; including the sort expression</returns>
		  public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty)
		  {
				return OrderBy<TEntity>(source, orderByProperty, false);
		  }
		  /// <summary>
		  /// Gets a descending sort expression based on the orderByProperty Name
		  /// </summary>
		  /// <typeparam name="TEntity">Entity type</typeparam>
		  /// <param name="source">The IQueryable instance to sort</param>
		  /// <param name="orderByProperty">the name of the property to orderBy</param>
		  /// <returns>IQueryable&lt;TEntity&gt; including the sort expression</returns>
		  public static IQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> source, string orderByProperty)
		  {
				return OrderBy<TEntity>(source, orderByProperty, true);
		  }


		  /// <summary>
		  /// <para>This extension method will parse an ObjectDataSource sortExpression which looks like:</para>
		  /// <para>    'Fieldname ASC' for ascending sort or 'Fieldname DESC' for Descending sort</para>
		  /// <para>It will then call the:</para>
		  /// <para>     IQueryable&lt;TEntity&gt; OrderBy&lt;TEntity&gt;(this IQueryable&lt;TEntity&gt; source,
		  /// 		string orderByProperty, bool desc) method</para>
		  /// </summary>
		  /// <typeparam name="TEntity">Entity type</typeparam>
		  /// <param name="source">The IQueryable instance to sort</param>
		  /// <param name="orderByProperty">the name of the property to orderBy</param>
		  /// <param name="defaultSortProperty">Sort field when sortexpression is empty</param>
		  /// <returns>IQueryable&lt;TEntity&gt; including the sort expression</returns>
		  public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source,
				string objectDatasourceSortExpressionByProperty, string defaultSortProperty)
		  {
				string[] sort = objectDatasourceSortExpressionByProperty.Split(' ');
				string sortKey = String.IsNullOrEmpty(sort[0]) ? defaultSortProperty : sort[0];
				bool sortDesc = sort.Length > 1 ? sort[1] == "DESC" : false;

				return OrderBy<TEntity>(source, sortKey, sortDesc);
		  }
	 }
}

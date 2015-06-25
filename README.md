# DXEFLinqExtensions
This repo contains the source of several E.F. related Linq Extension methods

This repo started with finding a way to do Linq querying with OrderBy(..) expression to include a 
fieldname as string instead of a lambda expression. 
The last one can only be set before building the project :-( while I want to be able to determine 
the sort at runtime.

The problems began when I started to build Lambda expression programatically specially with 
Entity Framework simple type fields like int, DateTime etc.
Runtime exceptions showed up like:
  Unable to cast the type 'System.Int32' to type 'System.Object'. 
	LINQ to Entities only supports casting EDM primitive or enumeration types.
Expression.Convert(...) will not help in these cases when the provider appears to be an E.F. one.

The extension methods in this package will construct things properly with the correct boxing conversion.

Usage:
  Simple include the namespace: DX.Data.Linq

The following extensions will then be available:
	OrderBy(string orderByProperty, bool sortDescending)
	OrderBy(string orderByProperty)
	OrderByDescending(string orderByProperty)
	OrderBy(string objectDatasourceSortExpressionByProperty, string defaultSortProperty)

If you want to use WebForms with an ObjectDataSource component and use E.F. with Linq for data access,
you can derive you implementation class from: EntityFrameworkObjectDataSource<TDbContext>
Like:
  [DataObject(true)]
  public class MyClass : EntityFrameworkObjectDataSource<MyEFContext> 
  {
    //.. your methods ..
    // don't forget the DataObjectMethodType attribute on you methods:
  }
  
Or for strongly typed CRUD and Validation methods use the abstract class:
EntityFrameworkObjectDataSource<TDbContext, TEntity> 
Like:
  [DataObject(true)]
  public class MyClass : EntityFrameworkObjectDataSource<MyEFContext, MyEntity> 
  {
    //.. implement abstract and add your methods
    // don't forget the DataObjectMethodType attribute on you methods:
  }

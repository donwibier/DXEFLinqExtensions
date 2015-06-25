To use the CodeRush Template for quick creation of ObjectDatasource classes:

Open menu DevExpress/CodeRush/Options...
Navigate to Editor/Templates in tree (Make sure Level: Expert / Language: CSharp)
Add a template

Name: 
	 .odsef
	 (to activate in editor, type .odsef<space>)
Comment: 
	 Create an ObjectDatasource class for using Entity Framework and Linq

Dependent Namespaces:
	 System.ComponentModel
	 System.Linq
	 DX.Data
	 DX.Data.Linq

Expansion:

	 //ATTENTION: Please reference the package DXEFLinqExtensions from NuGet!
	 [DataObject(true)]
	 public class «BlockAnchor»«FieldStart»«Link("EntityClass")»«FieldEnd»«Caret»Datasource : DX.Data.EntityFrameworkObjectDataSource<«FieldStart»«Link("DbContextClass")»«FieldEnd», «FieldStart»«Link("EntityClass")»«FieldEnd»>
	 {
		  [DataObjectMethod(DataObjectMethodType.Select, false)]
		  public IEnumerable<«Link("EntityClass")»> Select()
		  {
				var result = from n in DBContext.«Link("EntityClass")»
								 select n;
				return result;
		  }
		  
		  public int SelectCount()
		  {
				var result = DBContext.«Link("EntityClass")»;
				return result.Count();
		  }
			
			#region Custom Selection methods
			
			#endregion

		  #region CRUD Operations

		  [DataObjectMethod(DataObjectMethodType.Insert, false)]
		  public override void Insert(«Link("EntityClass")» item)
		  {
				// Validate your input here			
				if (!ValidateInsert(item))
					 throw new Exception("Validation failed on insert");
				//insert your item in the dataStore
			  DBContext.«Link("EntityClass")».Add(item);
				DBContext.SaveChanges();
		  }

		  [DataObjectMethod(DataObjectMethodType.Update, false)]
		  public override void Update(«Link("EntityClass")» item)
		  {
				// Validate your input here
				if (!ValidateUpdate(item))
					 throw new Exception("Validation failed on update");
				// update your item in the dataStore
				var m = DBContext.«Link("EntityClass")».Find(item.«FieldStart»«Link("EntityId")»«FieldEnd»);
				if (m != null)
				{
					 //TODO: Update properties
					 //m.Name = item.Name;
					 DBContext.SaveChanges();
				}
			}

		  [DataObjectMethod(DataObjectMethodType.Delete, false)]
		  public override void Delete(«Link("EntityClass")» item)
		  {
				// Validate your input here 
			  if (!ValidateDelete(item))
					 throw new Exception("Validation failed on delete");
				// delete your item in the dataStore
				var m = DBContext.«Link("EntityClass")».Find(item.«Link("EntityId")»);
				if (m != null)
				{
					 DBContext.«Link("EntityClass")».Remove(m);
					 DBContext.SaveChanges();
				}
		  }
		  
		  #endregion
		  
		  #region CRUD Validation
		  
		  public override bool ValidateInsert(«Link("EntityClass")» item) { return true; }

		  public override bool ValidateUpdate(«Link("EntityClass")» item) { return true; }

		  public override bool ValidateDelete(«Link("EntityClass")» item) { return true; }
		  		  
		  #endregion
	 }

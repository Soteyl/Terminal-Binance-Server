```c#
// usings

namespace Ixcent.ProjectName.Dir1.Dir2.Dir3
{
	public/internal class ClassName<TGeneric>: IInterface
	{
		private/protected const string CONST_NAME = "some const";
		
		private/protected readonly string _readonlyName = "readonly";
		
		private/protected string _privateName = "private";
		
		static ClassName()
		{
			// some actions
		}
		
		public/internal/private ClassName(Class1 someArgument, Class2 secondArgument)
		{
			// init
		}
		
		public/internal Class1 SomeProperty { get; set; }
		
		public event EventHandler SomeEvent;
		
		public/internal void SomeMethod()
		{
			var variable = new SpecificType();
			SpecificType secondVariable = SomeExtension.GetInstance();
		}
		
		private/protected void SomePrivateMethod()
		{
		}
		
		public/internal/private/protected class ChildClass
		{
		}
	}
}
```

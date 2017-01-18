using System;
using Monodoc.Ecma;

namespace EcmaUrlParserSample
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var parser = new EcmaUrlParser();
			string someMethod = "M:MyNamespace.MyType.MyMethod<T,K>(string,MyOtherNamespace.OtherType)";

			EcmaDesc desc;
			parser.TryParse(someMethod, out desc);

			Console.WriteLine($"namespace {desc.Namespace}, name {desc.MemberName}, number of arguments {desc.MemberArgumentsCount}");

			foreach (var genericParameter in desc.GenericMemberArguments)
				Console.WriteLine($"generic parameter: {genericParameter.TypeName}");


			foreach (var parameter in desc.MemberArguments)
				Console.WriteLine($"namespace: {parameter.Namespace}, name: {parameter.TypeName}");
		}
	}
}

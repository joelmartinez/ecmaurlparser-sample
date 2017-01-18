You can download a [Xamarin Workbooks](https://developer.xamarin.com/workbooks/) version of this sample here:  
https://github.com/joelmartinez/workbooks/tree/master/EcmaUrlParsing.workbook

Monodoc has an API that makes it easy to parse "monodoc URLs". What is a monodoc URL? Every *kind* of endpoint type has a special URL, a few examples include:

* **Type** - T:System.String

* **Method** - M:System.String.Join

* **Property** - P:System.String.Property

Other URL types include field, event, etc.

Internally to monodoc, this API is used to parse the incoming URL, and find the correct XML file, and the correct XML nodes to render. Usage is simple and centers around the `EcmaUrlParser` class.

```csharp
using Monodoc.Ecma;
var parser = new EcmaUrlParser();
```

This `parser` instance can be used to parse Monodoc URLs as described above. In the example below, we have a method url.

```csharp
string someMethod = "M:MyNamespace.MyType.MyMethod<T,K>(string,MyOtherNamespace.OtherType)"
```

With this string, we can use the `parser`’s `TryParse` method to produce an `EcmaDesc` instance.

```csharp
EcmaDesc desc;
parser.TryParse(someMethod, out desc)
```

If the `TryParse` method returned `true`, it means the format was valid and it was able to be parsed. You now have an object graph that breaks down the url into objects that describe it’s component parts. So for example, since this is a method url, you can get information about the  method such as the name, and the number of arguments:

```csharp
Console.WriteLine($"namespace {desc.Namespace}, name {desc.MemberName}, number of arguments {desc.MemberArgumentsCount}");
```

As this is a generic method, you can also get information about the generic member arguments:

```csharp
foreach(var genericParameter in desc.GenericMemberArguments)
    Console.WriteLine($"generic parameter: {genericParameter.TypeName}");
```

In addition to all of the type information about the parameters:

```csharp
foreach(var parameter in desc.MemberArguments)
    Console.WriteLine($"namespace: {parameter.Namespace}, name: {parameter.TypeName}");
```

So for example, you can take the value in the [`ReturnType`](https://github.com/mono/mono/blob/master/mcs/class/corlib/Documentation/en/System.IO/BufferedStream.xml#L100) XML node, parse it, and be able to render it into a linked format, regardless of how “complex” that type may be (generic, etc.).
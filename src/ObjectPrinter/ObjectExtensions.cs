﻿namespace ObjectPrinter
{
	public static class ObjectExtensions
	{
		///<summary>Uses the ObjectPrinter to loop through the properties of the object, dumping them to a string.</summary>
		public static string DumpToString(this object obj)
		{
			return new ObjectPrinter(obj).Print();
		}

		///<summary>Uses the ObjectPrinter to loop through the properties of the object, dumping them to a string.</summary>
		public static string DumpToString(this object obj, string tab, string newline)
		{
			return new ObjectPrinter(obj, tab, newline).Print();
		}

		///<summary>Uses the ObjectPrinter to loop through the properties of the object, dumping them to a string.</summary>
		public static string DumpToString(this object obj, IObjectPrinterConfig config)
		{
			return new ObjectPrinter(obj, config).Print();
		}

		///<summary>Uses the ObjectPrinter to loop through the properties of the object, dumping them to a string.</summary>
		public static LazyStringDelegate DumpToLazyString(this object obj)
		{
			return new LazyStringDelegate(() => new ObjectPrinter(obj).Print());
		}

		///<summary>Uses the ObjectPrinter to loop through the properties of the object, dumping them to a string.</summary>
		public static LazyStringDelegate DumpToLazyString(this object obj, string tab, string newline)
		{
			return new LazyStringDelegate(() => new ObjectPrinter(obj, tab, newline).Print());
		}

		///<summary>Uses the ObjectPrinter to loop through the properties of the object, dumping them to a string.</summary>
		public static LazyStringDelegate DumpToLazyString(this object obj, IObjectPrinterConfig config)
		{
			return new LazyStringDelegate(() => new ObjectPrinter(obj, config).Print());
		}
	}
}

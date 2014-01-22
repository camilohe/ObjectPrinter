using System;
using System.Collections.Generic;
using ObjectPrinter.TypeInspectors;

namespace ObjectPrinter
{
    /// <summary>
    /// Use this class to customize type inspector registration.
    /// Order inspectors will be applied:
    /// - Enum
    /// - Exception
    /// - Enumerables
    /// - Custom
    /// - CatchAll
    /// </summary>
    public class TypeInspectorsRegistration
    {
        readonly List<ITypeInspector> _inspectors = new List<ITypeInspector>();

        /// <summary>unless overridden, in order: XmlNode, Dictionar, NameValueCollection & Enumerable</summary>
        public static IEnumerable<ITypeInspector> DefaultEnumerableInspectors = new ITypeInspector[]
            {
                new XmlNodeTypeInspector(), 
                new DictionaryTypeInspector(), 
                new NameValueCollectionTypeInspector(), 
                new EnumerableTypeInspector(), 
            };

        /// <summary>unless overridden: EnumTypeInspector</summary>
        public static ITypeInspector DefaultEnumTypeInspector = new EnumTypeInspector();

        /// <summary>unless overridden: ExceptionTypeInspector</summary>
        public static ITypeInspector DefaultExceptionTypeInspector = new ExceptionTypeInspector();

        private ITypeInspector _enumTypeInspector = DefaultEnumTypeInspector;
        private ITypeInspector _exceptionTypeInspector = DefaultExceptionTypeInspector;
        private ITypeInspector[] _enumerableTypeInspectors;

        private bool _inspectAllMsTypes;

        public TypeInspectorsRegistration OverrideEnumInspector(ITypeInspector enumTypeInspector)
        {
            _enumTypeInspector = enumTypeInspector;
            return this;
        }

        public TypeInspectorsRegistration OverrideExceptionInspector(ITypeInspector exceptionTypeInspector)
        {
            _exceptionTypeInspector = exceptionTypeInspector;
            return this;
        }

        /// <summary>DefaultEnumerableInspectors is used if not overridden</summary>
        public TypeInspectorsRegistration OverrideEnumerableInspectors(params ITypeInspector[] enumerableTypeInspectors)
        {
            _enumerableTypeInspectors = enumerableTypeInspectors;
            return this;
        }

        ///<summary>
        /// If not specified, ToString() is called on all types in 'System' and 'Microsoft' 
        /// namespaces not already covered by a type inspector.  Calling this will cause
        /// those types to be inspected by the CatchAll inspector
        ///</summary>
        public TypeInspectorsRegistration InspectAllMsTypes()
        {
            _inspectAllMsTypes = true;
            return this;
        }

        /// <summary>Inspectors to be used run before the CatchAll inspector</summary>
        public TypeInspectorsRegistration RegisterInspector(ITypeInspector inspector)
        {
            if (inspector == null) throw new ArgumentNullException("inspector");
            _inspectors.Add(inspector);
            return this;
        }

        public IEnumerable<ITypeInspector> GetRegisteredInspectors()
        {
            yield return _enumTypeInspector ?? DefaultEnumTypeInspector;
            yield return _exceptionTypeInspector ?? DefaultExceptionTypeInspector;

            foreach (var inspector in _enumerableTypeInspectors ?? DefaultEnumerableInspectors)
            {
                yield return inspector;
            }

            foreach (var inspector in _inspectors)
            {
                yield return inspector;
            }

            if (!_inspectAllMsTypes)
            {
                yield return new ToStringTypeInspector {ShouldInspectType = Funcs.IncludeMsBuiltInNamespaces};
            }

            yield return ObjectPrinterConfig.CatchAllTypeInspector;
        }
    }
}
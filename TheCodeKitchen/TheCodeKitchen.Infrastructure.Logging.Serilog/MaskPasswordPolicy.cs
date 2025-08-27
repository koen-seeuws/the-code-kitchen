using Serilog.Core;
using Serilog.Events;
using System.Reflection;

public class MaskPasswordPolicy : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
    {
        var type = value.GetType();

        // Only destructure classes/objects
        if (type.IsClass)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(
                    p => p.Name,
                    p =>
                    {
                        if (p.Name.ToLower().Contains("password"))
                            return new ScalarValue("***REDACTED***");
                        var propValue = p.GetValue(value);
                        return propertyValueFactory.CreatePropertyValue(propValue, true);
                    }
                );

            result = new StructureValue(props.Select(kvp => new LogEventProperty(kvp.Key, kvp.Value)));
            return true;
        }

        result = null!;
        return false;
    }
}
using System.Reflection;

namespace Shared.Extensions;

public static class MappExtensions
{
    // This code is an object-to-object mapper. It copies matching properties from one object to a new object of another type, as long as they have the same property names and types.

    // This is an extension method: it allows any object to use the MapTo<TDestination> method as if it were a method of the object itself
    public static TDestination MapTo<TDestination>(this object source)
    {
        // This checks if the 'source' object is null and if it is throw an ArgumentNullException with the message "source"
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        // This creates an instance of the 'TDestination' type
        TDestination destination = Activator.CreateInstance<TDestination>()!;

        // This retrieves all public instance properties of the source object
        var sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // This retrieves all public instance properties of the destination object
        var destinationProperties = destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var destinationProperty in destinationProperties)
        {
            // This finds a property in the source object that matches the name and type of the current destination property
            var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == destinationProperty.Name && x.PropertyType == destinationProperty.PropertyType);
            // If a matching property is found and the destination property can be written to
            if (sourceProperty != null && destinationProperty.CanWrite)
            {
                // Get the value of the source property
                var value = sourceProperty.GetValue(source);
                // Set the value of the destination property to the value from the source property
                destinationProperty.SetValue(destination, value);
            }
        }
        // Finally, return the populated destination object
        return destination;
    }
}

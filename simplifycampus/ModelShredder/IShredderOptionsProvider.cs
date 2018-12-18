using System;

namespace ModelShredder
{
    public interface IShredderOptionsProvider
    {
        /// <summary>
        /// Provides Shredder Options for a type.
        /// </summary>
        /// <param name="t">The type for which to provide ShredderOptions.</param>
        /// <returns>A <see cref="ShredderOptions"/> instance containing information that controls how the type should be shredderd.</returns>
        ShredderOptions ProvideShredderOptions(Type t);
    }
}
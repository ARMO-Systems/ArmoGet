using System;
using System.Collections.Generic;

namespace ArmoSystems.ArmoGet.ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        public static TResult IfNotDefault< TResult, TSource >( this TSource source, Func< TSource, TResult > onNotDefault, Predicate< TSource > isNotDefault = null )
        {
            if ( onNotDefault == null )
                throw new ArgumentNullException( nameof( onNotDefault ) );

            var isDefault = !isNotDefault?.Invoke( source ) ?? EqualityComparer< TSource >.Default.Equals( source, default( TSource ) );

            return isDefault ? default( TResult ) : onNotDefault( source );
        }
    }
}
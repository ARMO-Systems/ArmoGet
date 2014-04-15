using System;
using System.Collections.Generic;

namespace ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        public static TResult IfNotDefault< TResult, TSource >( this TSource source, Func< TSource, TResult > onNotDefault, Predicate< TSource > isNotDefault = null )
        {
            if ( onNotDefault == null )
                throw new ArgumentNullException( "onNotDefault" );

            var isDefault = isNotDefault == null ? EqualityComparer< TSource >.Default.Equals( source, default( TSource ) ) : !isNotDefault( source );

            return isDefault ? default( TResult ) : onNotDefault( source );
        }
    }
}
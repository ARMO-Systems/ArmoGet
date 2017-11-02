using System;

namespace ArmoSystems.ArmoGet.ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        public static TResult IfNotNull< TResult, TSource >( this TSource source, Func< TSource, TResult > onNotDefault ) where TSource : class
        {
            if ( onNotDefault == null )
                throw new ArgumentNullException( nameof( onNotDefault ) );

            return source == null ? default( TResult ) : onNotDefault( source );
        }
    }
}
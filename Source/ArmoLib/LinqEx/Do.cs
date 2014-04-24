using System;

namespace ArmoSystems.ArmoGet.ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        public static TSource Do< TSource >( this TSource source, Action< TSource > action ) where TSource : class
        {
            if ( action == null )
                throw new ArgumentNullException( "action" );

            action( source );

            return source;
        }
    }
}
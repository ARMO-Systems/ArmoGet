﻿using System;

namespace ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        public static TResult IfNotNull< TResult, TSource >( this TSource source, Func< TSource, TResult > onNotDefault ) where TSource : class
        {
            if ( onNotDefault == null )
                throw new ArgumentNullException( "onNotDefault" );

            return source == null ? default( TResult ) : onNotDefault( source );
        }
    }
}
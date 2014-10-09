using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace ArmoSystems.ArmoGet.ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        public static TSource Do< TSource >( this TSource source, Action< TSource > action )
        {
            if ( action == null )
                throw new ArgumentNullException( "action" );

            action( source );

            return source;
        }

        public static string ToDelimitedString< T >( this IEnumerable< T > source, string delimiter, Func< T, string > selector )
        {
            return source.Select( selector ).ToDelimitedString( delimiter );
        }

        public static bool ContainsAttribute< T >( this Type type ) where T : Attribute
        {
            return type.IsDefined( typeof ( T ), false );
        }

        public static bool IsAny< T >( this T obj, params T[] list ) where T : struct
        {
            return list.Any( item => item.Equals( obj ) );
        }

        public static TResult SafelyGetValue< TSource, TResult >( this TSource source, Func< TSource, TResult > action, TResult defaultResult, Action< Exception > actionIfFailed = null )
        {
            if ( action == null )
                throw new ArgumentNullException( "action" );
            try
            {
                return action( source );
            }
            catch ( Exception ex )
            {
                if ( actionIfFailed != null )
                    actionIfFailed( ex );
            }
            return defaultResult;
        }

        public static void SafelyDoAction< TSource >( this TSource source, Action< TSource > action, Action< Exception > actionIfFailed = null )
        {
            if ( action == null )
                throw new ArgumentNullException( "action" );
            try
            {
                action( source );
            }
            catch ( Exception ex )
            {
                if ( actionIfFailed != null )
                    actionIfFailed( ex );
            }
        }

        public static bool CompareWith( this byte[] left, byte[] right )
        {
            if ( left == null && right == null )
                return true;
            if ( left == null || right == null || left.Length != right.Length )
                return false;

            return !left.Where( ( t, i ) => t != right[ i ] ).Any();
        }

        public static IEnumerable< T > GetEnumNotObsoleteValues< T >()
        {
            var enumType = typeof ( T );
            return Enum.GetValues( enumType ).OfType< T >().Where( item => !enumType.GetMember( item.ToString() )[ 0 ].GetCustomAttributes( typeof ( ObsoleteAttribute ), false ).Any() );
        }

        public static T ToEnum< T >( string type ) where T : struct
        {
            T value;
            if ( !Enum.TryParse( type, out value ) )
                throw new ArgumentException( "type" );
            return value;
        }
    }
}
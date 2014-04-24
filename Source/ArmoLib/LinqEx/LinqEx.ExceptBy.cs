using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmoSystems.ArmoGet.ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        /// <summary>
        /// Returns the set of elements in the first sequence which aren't
        /// in the second sequence, according to a given keys selector.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="firstKeySelector"/> and <paramref name="secondKeySelector"/> </typeparam>
        /// <param name="first">The sequence of potentially included elements.</param>
        /// <param name="second">The sequence of elements whose keys may prevent elements in
        /// <paramref name="first"/> from being returned.</param>
        /// <param name="firstKeySelector">The mapping from first sequence element to key.</param>
        /// <param name="secondKeySelector">The mapping from second sequence element to key.</param>
        /// <returns>A sequence of elements from <paramref name="first"/> whose key was not also a key for
        /// any element in <paramref name="second"/>.</returns>
        public static IEnumerable< TFirst > ExceptBy< TFirst, TSecond, TKey >( this IEnumerable< TFirst > first, IEnumerable< TSecond > second, Func< TFirst, TKey > firstKeySelector, Func< TSecond, TKey > secondKeySelector )
        {
            return ExceptBy( first, second, firstKeySelector, secondKeySelector, null );
        }

        /// <summary>
        /// Returns the set of elements in the first sequence which aren't
        /// in the second sequence, according to a given keys selector.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="firstKeySelector"/> and <paramref name="secondKeySelector"/> </typeparam>
        /// <param name="first">The sequence of potentially included elements.</param>
        /// <param name="second">The sequence of elements whose keys may prevent elements in
        /// <paramref name="first"/> from being returned.</param>
        /// <param name="firstKeySelector">The mapping from first sequence element to key.</param>
        /// <param name="secondKeySelector">The mapping from second sequence element to key.</param>
        /// <param name="keyComparer">>The equality comparer to use to determine whether or not keys are equal.
        /// If null, the default equality comparer for <c>TKey</c> is used.</param>
        /// <returns>A sequence of elements from <paramref name="first"/> whose key was not also a key for
        /// any element in <paramref name="second"/>.</returns>
        public static IEnumerable< TFirst > ExceptBy< TFirst, TSecond, TKey >( this IEnumerable< TFirst > first, IEnumerable< TSecond > second, Func< TFirst, TKey > firstKeySelector, Func< TSecond, TKey > secondKeySelector,
            IEqualityComparer< TKey > keyComparer )
        {
            if ( first == null )
                throw new ArgumentNullException( "first" );
            if ( second == null )
                throw new ArgumentNullException( "second" );
            if ( firstKeySelector == null )
                throw new ArgumentNullException( "firstKeySelector" );
            if ( secondKeySelector == null )
                throw new ArgumentNullException( "secondKeySelector" );
            return ExceptByImpl( first, second, firstKeySelector, secondKeySelector, keyComparer );
        }

        private static IEnumerable< TFirst > ExceptByImpl< TFirst, TSecond, TKey >( this IEnumerable< TFirst > first, IEnumerable< TSecond > second, Func< TFirst, TKey > firstKeySelector, Func< TSecond, TKey > secondKeySelector,
            IEqualityComparer< TKey > keyComparer )
        {
            var keys = new HashSet< TKey >( second.Select( secondKeySelector ), keyComparer );
            foreach ( var element in first )
            {
                var key = firstKeySelector( element );
                if ( keys.Contains( key ) )
                    continue;
                yield return element;
                keys.Add( key );
            }
        }
    }
}
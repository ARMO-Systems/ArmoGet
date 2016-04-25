using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reflection;
using MoreLinq;

namespace ArmoSystems.ArmoGet.ArmoLib.LinqEx
{
    public static partial class LinqEx
    {
        public static TSource Do<TSource>(this TSource source, Action<TSource> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            action(source);

            return source;
        }

        public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter, Func<T, string> selector)
        {
            return source.Select(selector).ToDelimitedString(delimiter);
        }

        public static bool ContainsAttribute<T>(this Type type) where T : Attribute
        {
            return type.IsDefined(typeof(T), false);
        }

        public static bool IsAny<T>(this T obj, params T[] list) where T : struct
        {
            return list.Any(item => item.Equals(obj));
        }

        public static TResult SafelyGetValue<TSource, TResult>(this TSource source, Func<TSource, TResult> action,
            TResult defaultResult, Action<Exception> actionIfFailed = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            try
            {
                return action(source);
            }
            catch (Exception ex)
            {
                if (actionIfFailed != null)
                    actionIfFailed(ex);
            }
            return defaultResult;
        }

        public static void SafelyDoAction<TSource>(this TSource source, Action<TSource> action,
            Action<Exception> actionIfFailed = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            try
            {
                action(source);
            }
            catch (Exception ex)
            {
                if (actionIfFailed != null)
                    actionIfFailed(ex);
            }
        }

        public static bool CompareWith(this byte[] left, byte[] right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null || left.Length != right.Length)
                return false;

            return !left.Where((t, i) => t != right[i]).Any();
        }

        public static IEnumerable<T> GetEnumNotObsoleteValues<T>()
        {
            var enumType = typeof(T);
            return
                Enum.GetValues(enumType)
                    .OfType<T>()
                    .Where(
                        item =>
                            !enumType.GetMember(item.ToString())[0].GetCustomAttributes(typeof(ObsoleteAttribute), false)
                                .Any());
        }

        public static T ToEnum<T>(string type) where T : struct
        {
            T value;
            if (!Enum.TryParse(type, out value))
                throw new ArgumentException("type");
            return value;
        }

        public static void IfNotNull<TSource>(this TSource source, Action<TSource> onNotDefault) where TSource : class
        {
            if (onNotDefault == null)
                throw new ArgumentNullException("onNotDefault");
            if (source != null)
                onNotDefault(source);
        }

        public static IEnumerable<string> GetInnerExceptions(this Exception ex)
        {
            return GetInnerExceptions(ex, exceptions => exceptions.Select(item => item.Message));
        }

        public static IEnumerable<T> GetInnerExceptions<T>(this Exception ex,
            Func<IEnumerable<Exception>, IEnumerable<T>> selector)
        {
            return
                selector(
                    EnumerableEx.Return(ex)
                        .Expand(
                            item =>
                                item.InnerException != null
                                    ? EnumerableEx.Return(item.InnerException)
                                    : Enumerable.Empty<Exception>()));
        }

        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return (T)type.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }

        public static IEnumerable<T> ExpandWithTopLeaves<T>(this IEnumerable<T> values, Func<T, IEnumerable<T>> childrenSelector)
        {
            var localCopyOfValues = values.ToList();
            return localCopyOfValues.Concat(localCopyOfValues.Expand(childrenSelector));
        }

        public static TSource DoAction<TSource>(this TSource source, Action<TSource> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            action(source);
            return source;
        }

        public static T GetAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            return (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }

        public static TimeSpan AddHours(this TimeSpan time, int added)
        {
            return time.Add(TimeSpan.FromHours(added));
        }

        public static TimeSpan AddMinutes(this TimeSpan time, int added)
        {
            return time.Add(TimeSpan.FromMinutes(added));
        }

        internal static TimeSpan AddSeconds(this TimeSpan time, int added)
        {
            return time.Add(TimeSpan.FromSeconds(added));
        }

        public static TSource DoIfNotNull<TSource>(this TSource source, Action<TSource> onNotDefault) where TSource : class
        {
            return LinqEx.IfNotNull(source, item => item.Do(onNotDefault));
        }

        public static IDisposable Schedule(this IScheduler scheduler, Action action, Action<Exception> onException)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            if (onException == null)
                throw new ArgumentNullException("onException");
            return scheduler.Schedule(() =>
            {
                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    onException(exception);
                }
            });
        }

        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            var info = enumValue.GetType().GetMember(enumValue.ToString());
            var attributes = info[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static IEnumerable<TEnum> GetEnumValues<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>();
        }

        public static TEnum Parse<TEnum>(string value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}
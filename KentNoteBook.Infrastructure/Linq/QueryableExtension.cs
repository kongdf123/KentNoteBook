using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using KentNoteBook.Infrastructure.Html.Grid;

namespace KentNoteBook.Infrastructure.Linq
{
	public static class QueryableExtension
	{
		public static IQueryable<T> Where<T>(this IQueryable<T> source, string field, object value, FilterOperator filterOperator) where T : class {

			var property = typeof(T).GetProperties().SingleOrDefault(x => x.Name == field);
			if ( property == null ) {
				throw new NullReferenceException("No such field name.");
			}

			var parameter = Expression.Parameter(typeof(T));

			var converter = System.ComponentModel.TypeDescriptor.GetConverter(property.PropertyType);
			if ( converter.IsValid(value) ) {
				value = converter.ConvertFromString(value.ToString());
			}

			var left = Expression.Property(parameter, field);
			var right = Expression.Convert(Expression.Constant(value, property.PropertyType), property.PropertyType);

			Expression body = null;

			switch ( filterOperator ) {
				case FilterOperator.Equal:
					body = Expression.Equal(left, right);
					break;
				case FilterOperator.NotEqual:
					body = Expression.NotEqual(left, right);
					break;
				case FilterOperator.Contains:
					if ( property.PropertyType == typeof(string) ) {
						var contains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
						body = Expression.Call(left, contains, right);
					}
					break;
				case FilterOperator.GreaterThan:
					body = Expression.GreaterThan(left, right);
					break;
				case FilterOperator.GreaterThanOrEqual:
					body = Expression.GreaterThanOrEqual(left, right);
					break;
				case FilterOperator.LessThan:
					body = Expression.LessThan(left, right);
					break;
				case FilterOperator.LessThanOrEqual:
					body = Expression.LessThanOrEqual(left, right);
					break;
				default:
					break;
			}

			if ( body == null ) {
				return source;
			}

			return source.Where(Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { parameter }));

		}

		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string field, SortDirection direction) where T : class {

			var property = typeof(T).GetProperties().SingleOrDefault(x => x.Name == field);
			if ( property == null ) {
				throw new NullReferenceException("No such field name.");
			}

			var parameter = Expression.Parameter(typeof(T));
			var conversion = Expression.Convert(Expression.Property(parameter, field), typeof(object));   //important to use the Expression.Convert

			var orderByExpression = Expression.Lambda<Func<T, object>>(conversion, parameter);

			if ( direction == SortDirection.Ascending ) {
				return source.OrderBy(orderByExpression);
			}
			else {
				return source.OrderByDescending(orderByExpression);
			}
		}
	}

	public enum SortDirection
	{
		Ascending = 0,
		Descending = 1,
	}

	public enum FilterOperator
	{
		Equal = 0,
		NotEqual = 1,
		Contains = 2,
		GreaterThan = 3,
		GreaterThanOrEqual = 4,
		LessThan = 5,
		LessThanOrEqual = 6,
	}
}

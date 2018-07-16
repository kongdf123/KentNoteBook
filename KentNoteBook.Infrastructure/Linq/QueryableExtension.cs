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

			var property = typeof(T).GetProperties().Where(x => x.Name == field);
			if ( property == null ) {
				throw new NullReferenceException("No such field name.");
			}

			var left = Expression.Parameter(property.GetType());
			var right = Expression.Convert(Expression.Constant(value, typeof(object)), property.GetType());

			Expression body = null;

			switch ( filterOperator ) {
				case FilterOperator.Equal:
					body = Expression.Equal(left, right);
					break;
				case FilterOperator.NotEqual:
					body = Expression.NotEqual(left, right);
					break;
				case FilterOperator.Contains:
					var contains = property.GetType().GetMethod("Contains", new Type[] { property.GetType() });
					body = Expression.Call(left, contains, right);
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
				throw new NotImplementedException("No such operator type.");
			}

			return source.Where(Expression.Lambda<Func<T, bool>>(body, left));

		}

		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string field, SortDirection direction) where T : class {

			var property = typeof(T).GetProperties().Where(x => x.Name == field);
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

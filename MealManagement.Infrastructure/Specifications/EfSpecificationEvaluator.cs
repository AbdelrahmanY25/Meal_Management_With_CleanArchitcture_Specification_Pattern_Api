namespace MealManagement.Infrastructure.Specifications;

internal static class EfSpecificationEvaluator<TEntity> where TEntity : class
{
	internal static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
	{
		var query = inputQuery;

		if (spec.Filter is not null)
			query = query.Where(spec.Filter);

		if (spec.Includes.Count > 0 || spec.IncludeStrings.Count > 0) 
		{
			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
			query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
		}

		if (spec.OrderBy is not null)
			query = query.OrderBy(spec.OrderBy);
		
		else if (spec.OrderByDescending is not null)
			query = query.OrderByDescending(spec.OrderByDescending);

		if (spec.IsPagingEnabled)
			query = query.Skip(spec.Skip!.Value).Take(spec.Take!.Value);

		if (spec.AsNoTracking)
			query = query.AsNoTracking();

		if (spec.IgnoreQueryFilters)
			query = query.IgnoreQueryFilters();

		return query;
	}
}

internal static class EfSpecificationEvaluator<TEntity, TResult> where TEntity : class
{
	internal static IQueryable<TResult> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TResult> spec)
	{
		var query = EfSpecificationEvaluator<TEntity>.GetQuery(inputQuery, spec);

		if (spec.Selector is null)
			throw new InvalidOperationException("Selector must be provided for projection specifications.");

		return query.Select(spec.Selector);
	}
}
namespace MealManagement.Application.Specifications;

public interface ISpecification<TEntity>
{
	Expression<Func<TEntity, bool>>? Filter { get; }
	List<Expression<Func<TEntity, object>>> Includes { get; }

	Expression<Func<TEntity, object>>? OrderBy { get; }
	Expression<Func<TEntity, object>>? OrderByDescending { get; }
	
	bool IsPagingEnabled { get; }
	int? Skip { get; }
	int? Take { get; }
	
	bool AsNoTracking { get; }
	bool IgnoreQueryFilters { get; }
}

public interface ISpecification<TEntity, TResult> : ISpecification<TEntity>
{
	Expression<Func<TEntity, TResult>> Selector { get; }
}
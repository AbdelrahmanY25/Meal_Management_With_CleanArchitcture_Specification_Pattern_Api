global using System.Linq.Expressions;



global using MealManagement.Application.Mapping;
global using MealManagement.Application.Services;
global using MealManagement.Application.Common.Errors;
global using MealManagement.Application.Specifications;
global using MealManagement.Application.Common.Abstractions;
global using MealManagement.Application.Interfaces.Services;
global using MealManagement.Application.Interfaces.Persistence;
global using MealManagement.Application.Contracts.Meals.Requests;
global using MealManagement.Application.Contracts.Meals.Responses;
global using MealManagement.Application.Contracts.MealOptions.Requests;
global using MealManagement.Application.Contracts.OptionItems.Requests;
global using MealManagement.Application.Contracts.MealOptions.Responses;
global using MealManagement.Application.Contracts.OptionItems.Responses;
global using MealManagement.Application.Contracts.MealOptions.Validators;
global using MealManagement.Application.Contracts.OptionItems.Validators;
global using MealManagement.Application.Specifications.MealsSpecifications;
global using MealManagement.Application.Specifications.MealOptionsSpecifications;



global using MealManagement.Domain.Entities;



global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;



global using Mapster;
global using MapsterMapper;
global using FluentValidation;
global using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
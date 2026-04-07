global using System.Linq.Expressions;


global using MealManagment.Application.Mapping;
global using MealManagment.Application.Services;
global using MealManagment.Application.Common.Errors;
global using MealManagment.Application.Specifications;
global using MealManagment.Application.Common.Abstractions;
global using MealManagment.Application.Interfaces.Services;
global using MealManagment.Application.Interfaces.Persistence;
global using MealManagment.Application.Contracts.Meals.Requests;
global using MealManagment.Application.Contracts.Meals.Response;
global using MealManagment.Application.Contracts.MealOptions.Requests;
global using MealManagment.Application.Contracts.OptionItems.Requests;
global using MealManagment.Application.Contracts.MealOptions.Responses;
global using MealManagment.Application.Contracts.OptionItems.Responses;
global using MealManagment.Application.Contracts.MealOptions.Validators;
global using MealManagment.Application.Contracts.OptionItems.Validators;
global using MealManagment.Application.Specifications.MealsSpecifications;
global using MealManagment.Application.Specifications.MealOptionsSpecifications;


global using MealManagment.Domain.Entities;


global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;



global using Mapster;
global using MapsterMapper;
global using FluentValidation;
global using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
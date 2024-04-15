using DaprTool.BuildingBlocks.Utils.Reflection;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationExtensions
{
    /// <summary>
    ///     注册验证器
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        //查找所有自动注册的服务实现类型进行注册
        var dependencyTypes = FindDependencyTypes();
        foreach (var dependencyType in dependencyTypes) AddValidator(services, dependencyType);

        return services;
    }

    #region 私有方法

    /// <summary>
    ///     注册验证器
    /// </summary>
    /// <param name="services"></param>
    /// <param name="validatorType"></param>
    private static void AddValidator(IServiceCollection services, Type validatorType)
    {
        var genericArgument = validatorType.BaseType?.GetGenericArguments().FirstOrDefault();
        if (genericArgument is not null)
            services.AddTransient(typeof(IValidator<>).MakeGenericType(genericArgument), validatorType); // 注册验证器
    }


    /// <summary>
    ///     查找所有 AbstractValidator 的实现类型
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Type> FindDependencyTypes()
    {
        return AssemblyManager.FindTypesByBase(typeof(AbstractValidator<>))
            .Where(type => type.BaseType?.IsGenericType == true)
            .Where(x => x.FullName != null && !x.FullName.StartsWith(nameof(FluentValidation)));
    }

    #endregion
}
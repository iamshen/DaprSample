﻿using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.EntityFramework.Extensions.Common;

namespace Idsrv4.Admin.BusinessLogic.Mappers;

public static class ApiScopeMappers
{
    static ApiScopeMappers()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiScopeMapperProfile>())
            .CreateMapper();
    }

    internal static IMapper Mapper { get; }

    public static ApiScopesDto ToModel(this PagedList<ApiScope> scopes)
        => scopes == null ? null : Mapper.Map<ApiScopesDto>(scopes);

    public static ApiScopeDto ToModel(this ApiScope resource)
        => resource == null ? null : Mapper.Map<ApiScopeDto>(resource);

    public static ApiScope ToEntity(this ApiScopeDto resource)
        => resource == null ? null : Mapper.Map<ApiScope>(resource);

    public static ApiScopeProperty ToEntity(this ApiScopePropertiesDto resource)
        => resource == null ? null : Mapper.Map<ApiScopeProperty>(resource);

    public static ApiScopePropertiesDto ToModel(this PagedList<ApiScopeProperty> scope)
        => scope == null ? null : Mapper.Map<ApiScopePropertiesDto>(scope);

    public static ApiScopePropertiesDto ToModel(this ApiScopeProperty scope)
        => scope == null ? null : Mapper.Map<ApiScopePropertiesDto>(scope);
}
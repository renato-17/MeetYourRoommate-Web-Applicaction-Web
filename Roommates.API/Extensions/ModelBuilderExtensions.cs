using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Roommates.API.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplySnakeCaseNamingConvention(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null)    
                {
                    entity.SetTableName(entity.GetTableName().ToSnakeCase());

                    foreach (var property in entity.GetProperties())
                        property.SetColumnName(property.GetColumnName().ToSnakeCase());
                    foreach (var key in entity.GetKeys())
                        key.SetName(key.GetName().ToSnakeCase());
                    foreach (var foreignKey in entity.GetForeignKeys())
                        foreignKey.SetConstraintName(foreignKey.GetConstraintName().ToSnakeCase());
                    foreach (var index in entity.GetIndexes())
                        index.SetName(index.GetName().ToSnakeCase());
                }
            }
        }
    }
}

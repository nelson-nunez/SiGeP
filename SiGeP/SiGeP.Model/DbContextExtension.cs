﻿using Microsoft.EntityFrameworkCore.Metadata;
using SiGeP.Model.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace SiGeP.Model
{
    public static class DbContextExtension
    {
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var methodToCall = typeof(DbContextExtension).GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { });
            entityData.SetQueryFilter((LambdaExpression)filter);
            var prop = entityData.FindProperty(nameof(ISoftDelete.Deleted));
            entityData.AddIndex(prop);
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => !x.Deleted.HasValue;
            return filter;
        }
    }
}
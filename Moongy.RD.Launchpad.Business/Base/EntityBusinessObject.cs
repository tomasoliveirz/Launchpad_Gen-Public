using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.Launchpad.Business.Base
{
    public abstract class EntityBusinessObject<T>(IBaseDataAccessObject<T> dao, IGenericDataAccessObject genericDao) : BaseBusinessObject, IEntityBusinessObject<T> where T : Entity
    {
        private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                return (PropertyInfo)memberExpression.Member;
            }
            throw new ArgumentException("Expression must be a property expression");
        }

        protected async Task<T> FindAndAttach<T1>(T record, Guid uuid, Expression<Func<T, T1?>>property, Expression<Func<T, int>> foreignKey) where T1:Entity
        {
            var relRecord = await genericDao.GetAsync<T1>(uuid) ?? throw new NotFoundException(nameof(T1), uuid.ToString());
            var propertyInfo = GetPropertyInfo(property);
            var foreignKeyInfo = GetPropertyInfo(foreignKey);
            propertyInfo.SetValue(record, relRecord);
            foreignKeyInfo.SetValue(record, relRecord.Id);
            return record;
        }

        public async virtual Task<OperationResult<Guid>> CreateAsync(T entity)
        {
            return await ExecuteOperation(async () =>
            {
                var result = await dao.CreateAsync(entity);
                return result;
            });
        }

        public async Task<OperationResult> DeleteAsync(Guid uuid)
        {
            return await ExecuteOperation(async () =>
            {
                var record = await dao.GetAsync(uuid) ?? throw new Exception("Record not found");
                await dao.DeleteAsync(record);
            });
        }

        public async Task<OperationResult<T?>> GetAsync(Guid uuid)
        {
            return await ExecuteOperation(async () =>
            {
                var result = await dao.GetAsync(uuid);
                return result;
            });
        }

        public async Task<OperationResult<IEnumerable<T>>> ListAsync()
        {
            return await ExecuteOperation(async () =>
            {
                var result = await dao.ListAsync();
                return result;
            });

        }

        public  async virtual Task<OperationResult> UpdateAsync(Guid uuid, T entity)
        {
            return await Task.Run(() =>
            {
                return new OperationResult() { Exception = new NotImplementedException() };
            });
        }
    }
}

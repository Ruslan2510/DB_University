using Layer.Contexts;
using Layer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Layer
{
    public class LINQLayer
    {
        StorageUnitContext context = new StorageUnitContext();

        public async Task<List<MeasureInfo>> GetMeasureInfoAsync()
        {
            var result = await context.MeasureInfo.ToListAsync();
            return result;
        }

        public async Task<List<Material>> GetMaterialsAsync()
        {
            var result = await context.Materials.ToListAsync();
            return result;
        }

        public async Task<List<Provider>> GetProvidersAsync()
        {
            var result = await context.Providers.ToListAsync();
            return result;
        }

        public async Task<List<StorageUnit>> GetStorageUnitsAsync()
        {
            var result = await context.StorageUnits.Include(x => x.Material).Include(x => x.Provider).Include(x => x.MeasureInfo).ToListAsync();
            return result;
        }

        //Проекиция из БД
        //Сортировка
        public async Task<List<StorageUnit>> GetStorageUnitsIncludeAsync()
        {
            var storageUnits = await GetStorageUnitsAsync();
            var result = storageUnits
                .Select(x => new StorageUnit
                {
                    Id = x.Id,
                    Date = x.Date,
                    Cost = x.Cost,
                    Amount = x.Amount,
                    MaterialId = x.Material.Id,
                    MeasureInfoId = x.MeasureInfo.Id,
                    OrderId = x.OrderId,
                    ProviderId = x.Provider.Id
                }).OrderBy(x => x.OrderId).ToList();

            return result;
        }

        //Фильтрация и выборка
        public async Task<List<Provider>> GetFilteredProvidersAsync(string searchedText)
        {
            searchedText = searchedText.ToLower();

            var providers = await GetProvidersAsync();
            var result = providers
                .Where(x => x.IdentificationCode.ToLower() == searchedText ||
                    x.Address.ToLower().Contains(searchedText) ||
                    x.Name.ToLower().Contains(searchedText))
                .ToList();

            return result;
        }
    }
}

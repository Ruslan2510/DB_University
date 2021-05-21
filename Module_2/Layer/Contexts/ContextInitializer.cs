using Layer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Contexts
{
    public class ContextInitializer : DropCreateDatabaseAlways<StorageUnitContext>
    {

        protected override void Seed(StorageUnitContext context)
        {
            //Материалы
            Material material1 = new Material
            {
                GroupId = 1,
                Name = "Дерево",
            };

            Material material2 = new Material
            {
                GroupId = 2,
                Name = "Бумага",
            };

            Material material3 = new Material
            {
                GroupId = 3,
                Name = "Сталь",
            };

            context.Materials.Add(material1);
            context.Materials.Add(material2);
            context.Materials.Add(material3);

            context.SaveChanges();

            //Поставщики
            Provider provider1 = new Provider
            {
                Name = "Поставщик_1",
                Address = "Украина г.Киев",
                BankAccountNumber = "1111111111",
                IdentificationCode = "1010101010",
            };

            Provider provider2 = new Provider
            {
                Name = "Поставщик_2",
                Address = "Украина г.Запорожье",
                BankAccountNumber = "2222222222",
                IdentificationCode = "2323232323",
            };

            Provider provider3 = new Provider
            {
                Name = "Поставщик_3",
                Address = "Украина г.Херсон",
                BankAccountNumber = "3333333333",
                IdentificationCode = "4545454545",
            };

            context.Providers.Add(provider1);
            context.Providers.Add(provider2);
            context.Providers.Add(provider3);

            context.SaveChanges();

            //Единица измерения конкретных видов
            MeasureInfo measureInfo1 = new MeasureInfo
            {
                Measure = "кг."
            };

            MeasureInfo measureInfo2 = new MeasureInfo
            {
                Measure = "шт."
            };

            MeasureInfo measureInfo3 = new MeasureInfo
            {
                Measure = "куб."
            };

            context.MeasureInfo.Add(measureInfo1);
            context.MeasureInfo.Add(measureInfo2);
            context.MeasureInfo.Add(measureInfo3);

            context.SaveChanges();

            //Единица хранения
            StorageUnit storageUnit1 = new StorageUnit
            {
                Material = material1,
                Date = "2020-12-24 23:14:16",
                Cost = 3000M,
                Amount = 5,
                MeasureInfo = measureInfo2,
                Provider = provider3,
                OrderId = 1
            };

            StorageUnit storageUnit2 = new StorageUnit
            {
                Material = material2,
                Date = "2019-11-20 20:00:16",
                Cost = 5000M,
                Amount = 2,
                MeasureInfo = measureInfo3,
                Provider = provider1,
                OrderId = 2
            };

            StorageUnit storageUnit3 = new StorageUnit
            {
                Material = material3,
                Date = "2018-10-01 16:14:16",
                Cost = 7000M,
                Amount = 8,
                MeasureInfo = measureInfo1,
                Provider = provider2,
                OrderId = 3
            };

            context.StorageUnits.AddRange(new List<StorageUnit>() { storageUnit1, storageUnit2, storageUnit3 });
            context.SaveChanges();
        }
    }
}

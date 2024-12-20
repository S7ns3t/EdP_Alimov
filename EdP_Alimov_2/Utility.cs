using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdP_Alimov_2.entities;

namespace EdP_Alimov_2.utility
{
    internal class Zadanie4
    {
        public int CalculateMaterial(int productTypeId,
               int materialTypeId,
               int productCount,
               double parameter1,
               double parameter2)
        {
            if (productTypeId <= 0 || materialTypeId <= 0 || productCount <= 0 || parameter1 <= 0 || parameter2 <= 0)
            {
                return -1;
            }
            var productType = getProductType(productTypeId);
            if (productType == null)
            {
                return -1;
            }
            var materialType = getMaterialType(materialTypeId);
            if (materialType == null)
            {
                return -1;
            }
            try
            {
                double materialPerUnit = parameter1 * parameter2 * Convert.ToDouble(productType.ProductCoefficient);
                double totalMaterial = materialPerUnit * productCount;
                double defectMultiplier = 1 + (materialType.defect / 100);
                totalMaterial *= defectMultiplier;
                return (int)Math.Ceiling(totalMaterial);
            }
            catch
            {
                return -1;
            }
        }

        private ProductType getProductType(int id)
        {
            using (var context = new MyDbContext())
            {
                return context.productType.FirstOrDefault(ch => ch.id == id);
            }
        }

        private MaterialType getMaterialType(int id)
        {
            using (var context = new MyDbContext())
            {
                return context.materialType.FirstOrDefault(ch => ch.id == id);
            }
        }
    }
}

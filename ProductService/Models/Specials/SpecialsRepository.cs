﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Specials
{
    public class SpecialsRepository : IRepository<ISpecial>
    {
        IList<ISpecial> specialsList;

        public SpecialsRepository()
        {
            specialsList = new List<ISpecial>();
        }

        public IList<ISpecial> GetAll()
        {
            return specialsList;
        }
        public ISpecial GetByProductName(string productName)
        {
            var specialsDict = specialsList.ToDictionary(p => p.ProductName, p => p);

            if (specialsDict.ContainsKey(productName))
            {
                return specialsDict[productName];
            }
            else
            {
                return null;
            }
        }

        public void Save(ISpecial saveThis)
        {
            specialsList.Add(saveThis);
        }
        public void Delete(ISpecial deleteThis)
        {
            throw new NotImplementedException();
        }

        public bool Update(ISpecial updateThis)
        {
            var specialsDict = specialsList.ToDictionary(p => p.ProductName, p => p);

            if (specialsDict.ContainsKey(updateThis.ProductName))
            {
                specialsDict[updateThis.ProductName] = updateThis;
                specialsList = specialsDict.Values.ToList();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
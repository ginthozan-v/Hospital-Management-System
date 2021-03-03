using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Models;
using HMS.ViewModels;

namespace HMS.BusinessLogic
{
    public class AddressClass
    {
        private HMSEntities db = new HMSEntities();

        public int SaveAddress(AddressViewModel add)
        {
            if (add.Id == 0)
            {
                var NewAddress = new Address
                {
                    Street = add.Street,
                    City = add.City,
                    Province = add.Province,
                    Country = add.Country,
                    Post_Code = add.Post_Code
                };
                db.Addresses.Add(NewAddress);
                db.SaveChanges();
                return (NewAddress.Id);
            }
            else
            {
                Address EditAdd = (from a in db.Addresses where a.Id == add.Id select a).FirstOrDefault();

                EditAdd.Id = add.Id;
                EditAdd.Street = add.Street;
                EditAdd.Province = add.Province;
                EditAdd.Country = add.Country;
                EditAdd.Post_Code = add.Post_Code;

                db.SaveChanges();
                return (EditAdd.Id);
            }
        }
    }
}

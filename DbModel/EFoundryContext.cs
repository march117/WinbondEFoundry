using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Validation;

namespace DbModel
{
    public class EFoundryContext:DbEntities
    {
        public override int SaveChanges()
        {
            try
            {
                base.Configuration.ValidateOnSaveEnabled = false;
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            finally
            {
                base.Configuration.ValidateOnSaveEnabled = true;
            }
            
        }
    }
}

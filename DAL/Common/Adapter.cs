using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{


    public class EFAdapter: IEFAdapter
    {
        private string _location;

        public String DbString { set; get; }
        public EFAdapter()
        {
            _location = "None";

            if (_location.Equals("Test"))
            {
                DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+@"C: \Users\20000390\documents\visual studio 2015\Projects\Dashboard\DAL\Database1.mdf"+";Integrated Security=True";
            }
            else
            {
                DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + @"C: \Users\20000390\documents\visual studio 2015\Projects\Dashboard\DAL\Database1.mdf" + ";Integrated Security=True";

            }
        }

        public T Catch<T>(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("DBDev", $"EF adapter catch Message:{ex.Message}", System.Diagnostics.EventLogEntryType.Error);

                throw new Exception("db fail : " + ex.Message);
            }
        }

    }
}

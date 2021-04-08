using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    public class LoadMetaData
    {
        private Type type;

        public LoadMetaData(Type type)
        {
            this.type = type;
        }
    }

    public class LoadMetaData<T> : LoadMetaData
    {
        public LoadMetaData() : base(typeof(T))
        {
        }
    }
}

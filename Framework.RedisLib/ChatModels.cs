using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.RedisLib
{
    [Serializable]
    public class TaskMessage
    {
        public int MerchantId { get; set; }

        public int TaskId { get; set; }
    }
}

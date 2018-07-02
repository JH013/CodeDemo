using Framework.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TxtFileHelper.TxtSpecialLineModify("E:\\12345.txt", 13, "modi13");
            var i = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 5);
            var j = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 4);
            var k = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 10);
        }
    }
}

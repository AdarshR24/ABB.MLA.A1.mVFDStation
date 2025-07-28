using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.MLA.A1.mVFDStation.HelperClasses
{
    public class PLCEnum
    {

        public enum OperationTypeEnum
        {
            WriteSingleCoil,
            WriteSingleRegsiter,
            WriteFloat,
            WriteString,
            WriteInt32,
                M241WriteSingleCoil
        }

        

        public OperationTypeEnum OperationType { get; set; }
        public bool boolValue { get; set; }
        public short intValue { get; set; }
        public int Address { get; set; }
        public string stringValue { get; set; }
        public float floatvalue { get; set; }
        public Int32 Int32Value { get; set; }

        public string M241Bit { get; set; }

    }
}

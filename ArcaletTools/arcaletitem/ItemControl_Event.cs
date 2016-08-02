using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaletTools.Data;

namespace ArcaletTools
{
    public partial class ArcaletItemControl
    {
        public delegate void OnItemInstanceReadComplete(IItemInstanceResult result);
        public delegate void OnItemInstanceWriteComplete(IItemInstanceResult result);

    }
}

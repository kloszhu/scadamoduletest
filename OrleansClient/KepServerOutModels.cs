using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using SixLabors.Fonts.Tables.AdvancedTypographic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Npoi.Mapper.Attributes;
namespace YSAI.OPCUA.KepServer
{
    public class KepServerOutModels
    {
        // Tag Name    Address Data Type Respect Data Type   Client Access   Scan Rate   Scaling Raw Low Raw High Scaled Low Scaled High Scaled Data Type    Clamp Low   Clamp High  Eng Units   Description Negate Value
        [Column(PropertyName ="Tag Name")]
        public string TagName { get; set; }
        [Column(PropertyName = "Address")]
        public string Address { get; set; }
        [Column(PropertyName = "Data Type")]
        public string DataType { get; set; }
        [Column(PropertyName = "RandomValue")]
        public bool RandomValue { get; set; }
    }
}

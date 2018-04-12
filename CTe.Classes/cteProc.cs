using System;
using System.Xml.Serialization;
using ZCTe.Classes.Protocolo;
using ZCTe.Classes.Servicos.Tipos;

namespace ZCTe.Classes
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "cteProc")]
    public class cteProc
    {
        [XmlAttribute]
        public versao versao { get; set; }

        public CTe CTe { get; set; }

        public protCTe protCTe { get; set; }
    }
}
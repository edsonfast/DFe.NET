using System.Collections.Generic;
using ZCTe.Classes;
using ZCTe.Classes.Servicos.Recepcao;
using ZCTe.Classes.Servicos.Recepcao.Retorno;
using CTe.Servicos.ConsultaRecibo;
using CTe.Servicos.Recepcao;
using CTe.Utils.CTe;

namespace CTe.Servicos.EnviarCte
{
    public class ServicoEnviarCte
    {
        public RetornoEnviarCte Enviar(int lote, ZCTe.Classes.CTe cte)
        {
            ServicoCTeRecepcao servicoRecepcao = new ServicoCTeRecepcao();

            retEnviCte retEnviCte = servicoRecepcao.CTeRecepcao(lote, new List<ZCTe.Classes.CTe> {cte});

            if (retEnviCte.cStat != 103)
            {
                return new RetornoEnviarCte(retEnviCte, null, null);
            }

            ConsultaReciboServico servicoConsultaRecibo = new ConsultaReciboServico(retEnviCte.infRec.nRec);

            retConsReciCTe retConsReciCTe = servicoConsultaRecibo.Consultar();


            cteProc cteProc = null;
            if (retConsReciCTe.cStat == 104)
            {

                if (retConsReciCTe.protCTe[0].infProt.cStat != 100)
                {
                    return new RetornoEnviarCte(retEnviCte, retConsReciCTe, null);
                }

                cteProc = new cteProc
                {
                    CTe = cte,
                    versao = ConfiguracaoServico.Instancia.VersaoLayout,
                    protCTe = retConsReciCTe.protCTe[0]
                };
            }

            cteProc.SalvarXmlEmDisco();

            return new RetornoEnviarCte(retEnviCte, retConsReciCTe, cteProc);
        }
    }
}
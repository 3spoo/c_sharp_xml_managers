using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleAppResponsabiliDipendenti
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string FILE_RES = "responsabili.xml";
            const string FILE_DIP = "dipendenti.xml";
            const string FILE_RES_DIP = "reso_dipe.xml";

            XmlDocument xdr = new XmlDocument();
            XmlDocument xdd = new XmlDocument();

            // FILE
            xdr.Load(FILE_RES);
            xdd.Load(FILE_DIP);

            foreach (XmlNode responsabile in xdr.DocumentElement.ChildNodes)
            {
                XmlNode node_dipendenti = xdr.CreateElement("dipendenti");
                responsabile.AppendChild(node_dipendenti);

                foreach (XmlNode dipendente in xdd.DocumentElement.ChildNodes)
                {
                    if (responsabile["codifica"].InnerText.Equals(dipendente["codice_responsabile"].InnerText))
                    {
                        /*
                            ...
                            < dipendente >
                                < nominativo > nominativo_tmp < nominativo />
                                < matricola > matricola_tmp < matricola />
                            < dipendente />
                            ...
                        */

                        XmlNode dipendente_tmp = xdr.CreateElement("dipendente");
                            XmlNode nominativo = xdr.CreateElement("nominativo");
                            nominativo.InnerText = dipendente["nominativo"].InnerText;
                            XmlNode matricola = xdr.CreateElement("matricola");
                            matricola.InnerText = dipendente["matricola"].InnerText;

                            dipendente_tmp.AppendChild(nominativo);
                            dipendente_tmp.AppendChild(matricola);
                        node_dipendenti.AppendChild(dipendente_tmp);
                    }
                }
            }

            xdr.Save(FILE_RES_DIP);
        }
    }
}

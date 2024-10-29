using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes_Estudos.CreateXML
{
    public class CreateXML
    {
        private StringBuilder _codeBuilder = new StringBuilder();

        public void GenerateClass(string className, string Namespace,  string[] atributos)
        {
            
            _codeBuilder.Append("#pragma warning disable CS1591\r\n\r\n" +
                "#if INTEROP\r\n" +
                "using System.Runtime.InteropServices;\r\n" +
                "#endif\r\n" +
                "using System;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "using System.Globalization;\r\n" +
                "using System.Xml.Serialization;\r\n" +
                "using Unimake.Business.DFe.Servicos;\r\n" +
                "using Unimake.Business.DFe.Utility;\r\n\r\n" +
                "namespace Unimake.Business.DFe.Xml.NFe\r\n" +
                "{\r\n" +
                "#if INTEROP\r\n" +
                "    [ClassInterface(ClassInterfaceType.AutoDual)]\r\n" +
                "    [ProgId(\"Unimake.Business.DFe.Xml.NF3e.NF3e\")]\r\n" +
                "    [ComVisible(true)]\r\n" +
                "#endif\r\n" +
                "    [Serializable()]\r\n" +
                "    [XmlRoot(\"NF3e\", Namespace = \"http://www.portalfiscal.inf.br/nf3e\", IsNullable = false)]\r\n");


            _codeBuilder.AppendLine($"public class {className}");
            _codeBuilder.AppendLine("{");

            // Gera os atributos
            foreach (var attribute in atributos)
            {
                _codeBuilder.AppendLine($"    public {attribute};");
            }

            // Gera os métodos
            //foreach (var method in methods)
            //{
            //    _codeBuilder.AppendLine($"    public void {method}() {{}}");
            //}

            _codeBuilder.AppendLine("}");

            Console.WriteLine(_codeBuilder.ToString());
        }

    }

    public class Atributos
    {
        public string Name { get; set; }
    }
}

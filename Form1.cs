using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes_Estudos.DynamicCodeGeneration;
using System.IO;
using Testes_Estudos.Utils;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Testes_Estudos
{
    public partial class Form1 : Form
    {

        public XDocument xml = new XDocument();

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Testar_Click(object sender, EventArgs e)
        {
            var objTeste = new Info()
            {
                ClassName = "Test",
                Atributos = new List<string>()
                {
                    ""
                },

            };

            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GeradorClasse.txt");

            if (File.Exists(folder))
            {
                File.Delete(folder);
            }

            ClassGenerator.ClassGenerator.GenerateClass(objTeste, xml, folder);
        }

        private void Selecionar_Xml_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    try
                    {
                        xml = new XDocument();
                        xml = XDocument.Load(filePath);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
    }

    namespace Utils
    {
        public class Info
        {
            public string ClassName { get; set; }
            public string Namespace { get; set; }
            public string TipoDfe { get; set; }

            public XmlDocument Exemplo { get; set; }
            public List<string> Atributos { get; set; }

            public List<string> Extras { get; set; }

        }

        public static class CriarClasse
        {
            private static StringBuilder _stringBuilder = new StringBuilder();
            private static List<XElement> _elementList = new List<XElement>();
            public static string CriarClasseRoot(XDocument xml, Info info = null)
            {
                var mlDocument = xml.Descendants();

                _elementList = mlDocument.ToList();

                foreach (var element in _elementList)
                {
                    bool Classe = element.HasElements;

                    string cName = $"{element.Name.LocalName.ToString().ToUpper()[0]}{element.Name.LocalName.ToString().Substring(1)}";
                    string eName = $"{element.Name.LocalName.ToString().ToLower()[0]}{element.Name.LocalName.ToString().Substring(1)}";

                    List<XElement> _classeFilha = new List<XElement>();

                    if (!Classe)
                    {

                        _stringBuilder.AppendLine($"[XmlElement(\"{eName}\")");
                        _stringBuilder.AppendLine($"public {cName} {cName}");

                        _classeFilha.Add(element);

                        if (element.NextNode == null)
                        {
                            _stringBuilder.AppendLine("}\r\n");

                            CriarClasseFilha(_classeFilha);

                            //_stringBuilder.AppendLine("}\r\n");
                            continue;
                        }
                        _stringBuilder.AppendLine();
                    }
                    else
                    {
                        _stringBuilder.AppendLine($"[XmlElement(\"{eName}\")");
                        _stringBuilder.AppendLine($"public {cName} {cName}");

                        _classeFilha.Add(element);

                        if (element.NextNode == null)
                        {
                            _stringBuilder.AppendLine("}\r\n");

                            CriarClasseFilha(_classeFilha);

                            //_stringBuilder.AppendLine("}\r\n");
                            continue;
                        }
                        _stringBuilder.AppendLine();
                    }
                }

                //_stringBuilder.AppendLine("}\r\n");

                //foreach (var atributo in _classeFilha.ToHashSet())
                //{
                //    CriarClasseFilha(atributo);
                //}

                return _stringBuilder.ToString();
            }

            public static void CriarClasseFilha(List<XElement> _classeFilha)
            {
                foreach (var element in _classeFilha)
                {
                    _stringBuilder.AppendLine("#if INTEROP\r\n" +
                    "    [ClassInterface(ClassInterfaceType.AutoDual)]\r\n" +
                    $"    [ProgId(\"Unimake.Business.DFe.Xml.TipoDFE.{element.Name.LocalName}\")]\r\n" +   //necessita ajuste na criação do interop
                    "    [ComVisible(true)]\r\n" +
                    "#endif\r\n");

                    _stringBuilder.AppendLine($" public class {element.Name.LocalName}");
                    _stringBuilder.AppendLine("{");
                    GerarAtributos(element);
                    _stringBuilder.AppendLine("}");
                }
            }

            public static void GerarAtributos(XElement atributo)
            {

                //_stringBuilder.AppendLine($"[XmlElement(\"{atributo.Name.LocalName.ToString().ToLower()[0]}{atributo.Name.LocalName.ToString().Substring(1)}\")");
                //_stringBuilder.AppendLine($"public {atributo.Name.LocalName.ToString().ToUpper()[0]}{atributo.Name.LocalName.ToString().Substring(1)} {atributo.Name.LocalName.ToString().ToUpper()[0]}{atributo.Name.LocalName.ToString().Substring(1)}\r\n");

                foreach (var element in atributo.Descendants())
                {
                    bool Classe = element.HasElements;

                    string cName = $"{element.Name.LocalName.ToString().ToUpper()[0]}{element.Name.LocalName.ToString().Substring(1)}";
                    string eName = $"{element.Name.LocalName.ToString().ToLower()[0]}{element.Name.LocalName.ToString().Substring(1)}";

                    int fChaves = default(int);
                    List<XElement> _classeFilha = new List<XElement>();

                    if (!Classe)
                    {

                        _stringBuilder.AppendLine($"[XmlElement(\"{eName}\")");
                        _stringBuilder.AppendLine($"public {cName} {cName}\r\n");
                        //_elementList.Remove(atributo);

                        if (element.NextNode == null)
                        {
                            _stringBuilder.AppendLine("}\r\n");
                        }
                    }
                    else
                    {
                        _stringBuilder.AppendLine($"[XmlElement(\"{eName}\")");
                        _stringBuilder.AppendLine($"public {cName} {cName}\r\n");
                        //_elementList.Remove(atributo);

                        if (element.NextNode == null)
                        {
                            _stringBuilder.AppendLine("}\r\n");

                            //CriarClasseFilha(_classeFilha);

                            //_stringBuilder.AppendLine("}\r\n");

                            fChaves++;
                        }
                        _classeFilha.Add(element);
                    }
                }

                //foreach (var child in _classeFilha)
                //{
                //    CriarClasseFilha(child);
                //}

            }
        }

    }

    namespace DynamicCodeGeneration
    {
        public static class Teste
        {
            public static void ExecuteDynamicCodeGeneration(string[] args = null)
            {
                // Cria um assembly em memória
                AssemblyName assemblyName = new AssemblyName("DynamicAssembly");
                AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

                // Cria um módulo
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

                // Cria uma classe
                TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicClass");

                // Define um método
                MethodBuilder methodBuilder = typeBuilder.DefineMethod("SayHello", MethodAttributes.Public, typeof(string), Type.EmptyTypes);
                ILGenerator generator = methodBuilder.GetILGenerator();
                generator.Emit(OpCodes.Ldstr, "Hello from dynamic code!");
                generator.Emit(OpCodes.Ret);

                // Cria o tipo
                Type dynamicType = typeBuilder.CreateType();

                // Cria uma instância do tipo e chama o método
                object instance = Activator.CreateInstance(dynamicType);
                MethodInfo methodInfo = dynamicType.GetMethod("SayHello");
                string result = (string)methodInfo.Invoke(instance, null);

                Console.WriteLine(result);
            }
        }
    }

    namespace ClassGenerator
    {
        public static class ClassGenerator
        {
            private static StringBuilder _codeBuilder = new StringBuilder();

            public static void GenerateClass(Info infos, XDocument xml, string folder)
            {


                _codeBuilder.AppendLine("#pragma warning disable CS1591\r\n\r\n" +
                    "#if INTEROP\r\n" +
                    "using System.Runtime.InteropServices;\r\n" +
                    "#endif\r\n" +
                    "using System;\r\n" +
                    "using System.Collections.Generic;\r\n" +
                    "using System.Globalization;\r\n" +
                    "using System.Xml.Serialization;\r\n" +
                    "using Unimake.Business.DFe.Servicos;\r\n" +
                    "using Unimake.Business.DFe.Utility;\r\n\r\n" +
                    $"namespace Unimake.Business.DFe.Xml.{infos.TipoDfe}\r\n" +
                    "{\r\n" +
                    "#if INTEROP\r\n" +
                    "    [ClassInterface(ClassInterfaceType.AutoDual)]\r\n" +
                    $"    [ProgId(\"Unimake.Business.DFe.Xml.{infos.TipoDfe}.{infos.ClassName}\")]\r\n" +
                    "    [ComVisible(true)]\r\n" +
                    "#endif\r\n" +
                    "    [Serializable()]\r\n" +
                   $"    [XmlRoot(\"NF3e\", Namespace = \"{infos.Namespace}\", IsNullable = false)]");
                _codeBuilder.AppendLine($"public class {infos.ClassName} : XmlBase");
                _codeBuilder.AppendLine("{");

                _codeBuilder.AppendLine(CriarClasse.CriarClasseRoot(xml, infos));

                Console.WriteLine(_codeBuilder.ToString());
                Console.WriteLine(folder);



                File.WriteAllText(folder, _codeBuilder.ToString());
                //File.WriteAllText(folder, _codeBuilder.ToString());
            }

        }


    }

}


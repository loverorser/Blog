// See https://aka.ms/new-console-template for more information
//using DW.Table;
using DW.Table;
using System.Text;
using System.Xml.Linq;

class Program {
    static void Main(string[] args)
    {
        if (args.Length!=2)
            throw new Exception("参数个数不合法！");
        string inputPath = args[0];
        string outputPath = args[1];
        A.ConvertFolder(inputPath, outputPath);
        return;
        A.ConvertFolder("C:\\Users\\lijiaxing_b\\source\\repos\\TestTable\\TestTable\\Input",
    "C:\\Users\\lijiaxing_b\\source\\repos\\TestTable\\TestTable\\Output");
        Console.ReadLine();
    }
}
class A
{
    public static void ConvertFolder(string inputPath,string outputPath)
    {
        var files = Directory.GetFiles(inputPath, "*.*", searchOption: SearchOption.AllDirectories);
        int totalCount = files.Length;
        int count = 0;
        Console.WriteLine("开始转换表");
        foreach (var v in files)
        {
            count++;
            Console.WriteLine($"进度:{count}/{totalCount} 转换表:{v}");
            Convert(v,inputPath,outputPath);

        }
    }

    public static void Convert(string txtPath,string inputPath,string outputPath)
    {
        //"C:\Users\lijiaxing_b\source\repos\TestTable\TestTable\Input\Monster\Boss.txt"
        //C:\Users\lijiaxing_b\source\repos\TestTable\TestTable\Input
        //C:\Users\lijiaxing_b\source\repos\TestTable\TestTable\Output
        string tableNameWithTxt = txtPath.Substring(txtPath.LastIndexOf('\\')+1);
        string tableName = tableNameWithTxt.Substring(0, tableNameWithTxt.LastIndexOf('.'));

        string fulltTableNameWithTxt = txtPath.Replace(inputPath, "");
        string fullTableName= fulltTableNameWithTxt.Substring(0, fulltTableNameWithTxt.LastIndexOf('.'));
        string csPath = $"{outputPath}{fullTableName}.cs";

        string dirPath = csPath.Substring(0, csPath.LastIndexOf('\\'));

        if(!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        if(File.Exists(csPath))
            File.Delete(csPath);

        StringBuilder sb = new StringBuilder();
        StringBuilder sb_init = new StringBuilder();
        StringBuilder sb_decl = new StringBuilder();
        

        var lines = File.ReadAllLines(txtPath);

        string[] types=null;
        string[] rems=null;
        string[] fieldNames=null;

        int count = -1;
        foreach (var line in lines)
        {
            if (line.StartsWith("//"))
                continue;
            count++;
            if (count==0)
            {
                types=line.Split('\t');
            }
            else if (count==1)
            {
                rems=line.Split("\t");
            }
            else if (count==2)
            {
                fieldNames=line.Split("\t");

                {
                    StringBuilder sb_decl_decl = new StringBuilder();
                    for (int i = 0; i<types.Length; i++)
                    {
                        sb_decl_decl.Append($$"""
                                    /// <summary>
                                    /// {{rems[i]}}
                                    /// </summary>
                                    public {{types[i]}} {{fieldNames[i]}} { get; set; }{{Environment.NewLine}}
                            """);
                    }
                    sb_decl.Append($$"""
                        {{sb_decl_decl}}
                        """);
                }

            }
            else
            {
                var strs = line.Split('\t');
                string id = strs[0];
                StringBuilder sb_init_init = new StringBuilder();
                for(int i=0;i<types.Length;i++)
                {
                    if (types[i]=="string")
                    {
                        sb_init_init.Append($"{fieldNames[i]}=\"{strs[i]}\",");
                    }
                    else
                    {
                        sb_init_init.Append($"{fieldNames[i]}={strs[i]},");
                    }

                    
                }
                //ID=0, Name="测试0", Value=2
                sb_init.Append($$"""
                                s_Dic[{{id}}]=new {{tableName}}() {{{sb_init_init}}};{{Environment.NewLine}}
                    """);
            }

        }


        sb.Append($$"""
            namespace DW.Table
            {
                /// <summary>
                /// Auto Generated,Do not modify!
                /// </summary>
                public partial class {{tableName}}
                {
                    static bool s_HasGetAll = false;
                    static Dictionary<int, {{tableName}}> s_Dic = new();
                    public static {{tableName}} Get(int id)
                    {
                        if (!s_Dic.ContainsKey(id))
                            InternalGet(id);

                        return s_Dic[id];
                    }
                    public static Dictionary<int, {{tableName}}> GetAll()
                    {
                        if (!s_HasGetAll)
                        {
                            InternalGetAll();
                            s_HasGetAll=true;
                        }
                        return s_Dic;
                    }
                    static void InternalGet(int id)
                    {
                        throw new NotImplementedException();
                    }
                    static void InternalGetAll()
                    {
                    {{sb_init}}
                    }
                }

                /// <summary>
                /// Auto Generated,Do not modify!
                /// </summary>
                public partial class {{tableName}} {
                    {{sb_decl}}
                }
            }
            """);

        //Console.Write(sb);
        File.WriteAllText(csPath, sb.ToString());
    }
}
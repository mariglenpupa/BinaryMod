using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using dnlib.DotNet.Emit;
using dnlib.DotNet;
using System.Windows.Forms;//tmp

namespace Smth
{
    class Builder
    {
        // To use just add a class into your project.
        // Paste this code. Add "DnLib" to your project (Project > Manage NuGet Packages...)
        // Call Build and specify your parameters.
        // path -> Where to save new binary
        // file -> The main file. (Usually the "stub")
        // replacements -> a 2d array in this format: {"ip", ip.Text}, {"port", port.Text}
        // There is room for improvement lmk if you want to help :D
        public async void Build(string path, byte[] file, string[,] replacements)
        {
            ModuleDefMD asmDef = ModuleDefMD.Load(file);
            Modify(asmDef, path, replacements);
            asmDef.Write(path);
            asmDef.Dispose();
            MessageBox.Show("Done!");
        }
        void Modify(ModuleDefMD asmDef, string AsmName, string[,] rep)
        {
            try
            {
                foreach (TypeDef type in asmDef.Types)
                {
                    asmDef.Assembly.Name = Path.GetFileNameWithoutExtension(AsmName);
                    asmDef.Name = Path.GetFileName(AsmName);
                    foreach (MethodDef method in type.Methods)
                    {
                        if (method.Body == null) continue;
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                            {
                                // Check for strings from the array & replace with new values.
                                for (int k = rep.Length / 2; k > 0; k--)
                                {
                                    if (method.Body.Instructions[i].Operand.ToString() == rep[0, 0]) 
                                        method.Body.Instructions[i].Operand = rep[0, 1];
                                }
                            }
                        }
                    }
                }
            }
            catch { throw; }
        }
    }
}

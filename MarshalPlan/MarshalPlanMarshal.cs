using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Reflection;

namespace PoiTech.MarshalPlan
{
    public static class MarshalPlanMarshal
    {
        private static int idCounter;

        private static void CreateWrappers(Type interfaceType, ModuleBuilder mb,
            out TypeBuilder rawInterface, out TypeBuilder CCP, out TypeBuilder RCP)
        {
            idCounter += 1;
            List<MethodDesc> methodDescs = new List<MethodDesc>();
            foreach (MethodInfo mi in interfaceType.GetMethods())
            {
                methodDescs.Add(new MethodDesc(mi));
            }
            rawInterface = mb.DefineType(interfaceType.FullName + "-RAW" + idCounter, TypeAttributes.Interface);
            CCW = mb.DefineType(interfaceType.FullName + "-CCP" + idCounter, TypeAttributes.Class);
            RCW = mb.DefineType(interfaceType.FullName + "-RCP" + idCounter, TypeAttributes.Class);

        }

        public static IntPtr GetComInterfaceForObject()
        {
            
        }
    }
}

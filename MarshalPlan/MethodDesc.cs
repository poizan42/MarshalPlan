using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace PoiTech.MarshalPlan
{
    internal class MethodDesc
    {
        private MethodInfo mi;
        private List<MarshalAsExAttribute> paramMarshalers;

        public MethodBuilder BuildRuntimeProxyMethod(TypeBuilder RCP)
        {
            MethodBuilder mb = RCP.DefineMethod(mi.Name, MethodAttributes.Public);
        }

        public MethodBuilder BuildCOMProxyMethod(TypeBuilder CCP)
        {
        }

        public Type[] GetRawTypes()
        {
            List<Type> res = new List<Type>();
            foreach (ParameterInfo pi in mi.GetParameters())
            {
                object[] marshalAsAttributes = pi.GetCustomAttributes(typeof(MarshalAsAttribute), false);
                if (marshalAsAttributes.Length > 0)
                {
                    MarshalAsAttribute attr = (MarshalAsAttribute)marshalAsAttributes[0];
                    switch (attr.Value)
                    {
                        case UnmanagedType.I1:
                        case UnmanagedType.U1:
                            res.Add(typeof(byte));
                            break;
                        case UnmanagedType.I2:
                        case UnmanagedType.U2:
                        case UnmanagedType.VariantBool:
                            res.Add(typeof(short));
                            break;
                        case UnmanagedType.Bool:
                        case UnmanagedType.Error:
                        case UnmanagedType.U4:
                        case UnmanagedType.I4:
                            res.Add(typeof(int));
                            break;
                        case UnmanagedType.I8:
                        case UnmanagedType.U8:
                            res.Add(typeof(long));
                            break;
                        case UnmanagedType.R4:
                            res.Add(typeof(float));
                            break;
                        case UnmanagedType.R8:
                            res.Add(typeof(double));
                            break;
                        case UnmanagedType.AnsiBStr:
                        case UnmanagedType.BStr:
                        case UnmanagedType.IDispatch:
                        case UnmanagedType.Interface:
                        case UnmanagedType.IUnknown:
                        case UnmanagedType.LPArray:
                        case UnmanagedType.LPStr:
                        case UnmanagedType.LPStruct:
                        case UnmanagedType.LPTStr:
                        case UnmanagedType.LPWStr:
                        case UnmanagedType.SafeArray:
                        case UnmanagedType.Struct:
                        case UnmanagedType.SysInt:
                        case UnmanagedType.SysUInt:
                        case UnmanagedType.TBStr:
                        case UnmanagedType.VBByRefStr:
                            res.Add(typeof(IntPtr));
                            break;
                        default:
                            throw new Exception("Unsupported unmanged type: " + attr.Value);
                    }
                }
            }
            return res.ToArray();
        }

        public MethodDesc(MethodInfo mi)
        {
            paramMarshalers = new List<MarshalAsExAttribute>();
            foreach (ParameterInfo pi in mi.GetParameters())
            {
                object[] marshalAsAttributes = pi.GetCustomAttributes(typeof(MarshalAsAttribute), false);
            }
        }

    }
}

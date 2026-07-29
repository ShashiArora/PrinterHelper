// Decompiled with JetBrains decompiler
// Type: PrinterHelper.Properties.Resources
// Assembly: PrinterHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DDC417CF-4400-4A62-855A-45727A385E86
// Assembly location: C:\AllProject\PRINTERhELPER\PrinterHelper.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PrinterHelper.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (PrinterHelper.Properties.Resources.resourceMan == null)
          PrinterHelper.Properties.Resources.resourceMan = new ResourceManager("PrinterHelper.Properties.Resources", typeof (PrinterHelper.Properties.Resources).Assembly);
        return PrinterHelper.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => PrinterHelper.Properties.Resources.resourceCulture;
      set => PrinterHelper.Properties.Resources.resourceCulture = value;
    }
  }
}

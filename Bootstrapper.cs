// Decompiled with JetBrains decompiler
// Type: PrinterHelper.Bootstrapper
// Assembly: PrinterHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DDC417CF-4400-4A62-855A-45727A385E86
// Assembly location: C:\AllProject\PRINTERhELPER\PrinterHelper.exe

using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;

namespace PrinterHelper
{
  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) => pipelines.AfterRequest += (Action<NancyContext>) (ctx => ctx.Response.WithHeader("Access-Control-Allow-Origin", "*").WithHeader("Access-Control-Allow-Methods", "POST, GET, DELETE, PUT, OPTIONS, PATCH").WithHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization").WithHeader("Access-Control-Max-Age", "3600"));
  }
}

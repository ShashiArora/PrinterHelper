// Decompiled with JetBrains decompiler
// Type: PrinterHelper.clsService
// Assembly: PrinterHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DDC417CF-4400-4A62-855A-45727A385E86
// Assembly location: C:\AllProject\PRINTERhELPER\PrinterHelper.exe

using Nancy;
using Newtonsoft.Json;
using PdfiumViewer;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace PrinterHelper
{
    public class clsService : NancyModule
    {
        public string GetDefaultPrinter()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                printerSettings.PrinterName = installedPrinter;
                if (printerSettings.IsDefaultPrinter)
                    return installedPrinter;
            }
            return string.Empty;
        }

        public void PrintFile(byte[] filecontent, string printername)
        {
            MemoryStream ms = new MemoryStream(filecontent);
            PdfDocument pdfDocument = PdfDocument.Load(ms);

            using (PrintDocument printDocument = pdfDocument.CreatePrintDocument())
            {
                printDocument.PrinterSettings.PrinterName = printername;
                printDocument.OriginAtMargins = true;
                printDocument.PrinterSettings.DefaultPageSettings.Landscape = false;
                printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 400, 400);
                printDocument.PrintController = new StandardPrintController();
                printDocument.Print();
            }

            pdfDocument.Dispose();
            ms.Dispose();
        }


        public clsService()
        {
            this.Get<string>("/CheckExist", (Func<object, string>)(_ => JsonConvert.SerializeObject((object)new
            {
                isError = false,
                errorMessage = "Service working"
            })));
            this.Get<string>("/GetPrinterList", (Func<object, string>)(_ => FileHelper.ReadListPrinter()));
            this.Post<string>("/PrintFile", (Func<object, string>)(x =>
            {
                Stream body = this.Request.Body;
                int length = (int)body.Length;
                byte[] numArray = new byte[length];
                body.Read(numArray, 0, length);
                string str = Encoding.Default.GetString(numArray);
                printRequest printRequest;
                try
                {
                    printRequest = JsonConvert.DeserializeObject<printRequest>(str);
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject((object)new
                    {
                        isError = false,
                        errorMessage = "Invalid request"
                    });
                }

                try
                {
                    if (printRequest.acPass != "#%%!@#")
                        return JsonConvert.SerializeObject(new
                        {
                            isError = true,
                            errorMessage = "Error printing"
                        });

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;

                    using (WebClient webClient = new WebClient())
                    {
                        string printerName;
                        if (printRequest.printerId == "0")
                        { 
                            printerName = GetDefaultPrinter();
                        }
                        else
                        {
                            printerName = printRequest.printerId.ToString();
                        }

                        byte[] fileData = webClient.DownloadData(new Uri(printRequest.fileUrl));
                        this.PrintFile(fileData, printerName);
                    }
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject((object)new
                    {
                        isError = true,
                        errorMessage = ex.Message
                    });
                }
                return JsonConvert.SerializeObject((object)new
                {
                    isError = false,
                    errorMessage = "File printed"
                });
            }));
            this.Post<string>("/SavePrinterList", (Func<object, string>)(x =>
            {
                Stream body = this.Request.Body;
                int length = (int)body.Length;
                byte[] numArray = new byte[length];
                body.Read(numArray, 0, length);
                string jsonList = Encoding.Default.GetString(numArray);
                try
                {
                    return !FileHelper.WriteListPrinter(jsonList) ? JsonConvert.SerializeObject((object)new
                    {
                        isError = true,
                        errorMessage = "Can not save info!"
                    }) : JsonConvert.SerializeObject((object)new
                    {
                        isError = false,
                        errorMessage = "Saved successfull!"
                    });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject((object)new
                    {
                        isError = true,
                        errorMessage = "Can not save info!"
                    });
                }
            }));
        }
    }
}

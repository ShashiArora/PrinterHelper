// Decompiled with JetBrains decompiler
// Type: PrinterHelper.FileHelper
// Assembly: PrinterHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DDC417CF-4400-4A62-855A-45727A385E86
// Assembly location: C:\AllProject\PRINTERhELPER\PrinterHelper.exe

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace PrinterHelper
{
    public static class FileHelper
    {
        public static void CleanTempFile(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            string pattern = "\\d+(?=.\\w+)";
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                try
                {
                    if (Regex.IsMatch(file.Name, pattern))
                    {
                        if (new TimeSpan(DateTime.Now.Ticks - long.Parse(Regex.Match(file.Name, pattern).Value)).TotalMinutes > 30.0)
                            file.Delete();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        //public static string ReadListPrinter()
        //{
        //  string contents;
        //  if (!File.Exists("printers.json"))
        //  {
        //    contents = JsonConvert.SerializeObject((object) new List<PriterName>()
        //    {
        //      new PriterName() { Id = 0, Name = "Microsoft Print to PDF" },
        //      new PriterName() { Id = 1, Name = "Microsoft Print to PDF" },
        //      new PriterName() { Id = 2, Name = "Microsoft Print to PDF" },
        //      new PriterName() { Id = 3, Name = "Microsoft Print to PDF" }
        //    });
        //    File.WriteAllText("printers.json", contents);
        //  }
        //  else
        //  {
        //    using (StreamReader streamReader = new StreamReader("printers.json"))
        //    {
        //      contents = streamReader.ReadToEnd();
        //      JsonConvert.DeserializeObject<List<PriterName>>(contents);
        //    }
        //  }
        //  return contents;
        //}
        public static string ReadListPrinter()
        {
            List<PriterName> printerList = new List<PriterName>();
            var installedPrinters = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

            int id = 0;
            foreach (string printer in installedPrinters)
            {
                printerList.Add(new PriterName() { Id = id++, Name = printer });
            }

            string contents = JsonConvert.SerializeObject(printerList);

            File.WriteAllText("printers.json", contents);

            return contents;
        }

        public static List<PriterName> getListPrinter()
        {
            List<PriterName> listPrinter;
            if (!File.Exists("printers.json"))
            {
                listPrinter = new List<PriterName>();
                listPrinter.Add(new PriterName()
                {
                    Id = 0,
                    Name = "Microsoft Print to PDF"
                });
                listPrinter.Add(new PriterName()
                {
                    Id = 1,
                    Name = "Microsoft Print to PDF"
                });
                listPrinter.Add(new PriterName()
                {
                    Id = 2,
                    Name = "Microsoft Print to PDF"
                });
                listPrinter.Add(new PriterName()
                {
                    Id = 3,
                    Name = "Microsoft Print to PDF"
                });
            }
            else
            {
                using (StreamReader streamReader = new StreamReader("printers.json"))
                    listPrinter = JsonConvert.DeserializeObject<List<PriterName>>(streamReader.ReadToEnd());
            }
            return listPrinter;
        }

        public static bool WriteListPrinter(string jsonList)
        {
            try
            {
                File.WriteAllText("printers.json", jsonList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

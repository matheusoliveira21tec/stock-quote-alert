using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Analysis;
using ServiceStack;
using ServiceStack.Text;
using System.Threading;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;

namespace StockPricesProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string name;
            double min;
            double max;
            Console.WriteLine("Digite a ação que quer monitorar:");
            name = Console.ReadLine();
            Console.WriteLine("Digite o valor minímo para receber o aviso de compra:");
            min = Double.Parse(Console.ReadLine());
            Console.WriteLine("Digite o valor maximo para receber o aviso de venda:");
            max = Double.Parse(Console.ReadLine());
            Email email = new Email();
            while (true)
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri("https://brapi.ga/api/quote/") };
                var response = await client.GetAsync(name);
                var content = await response.Content.ReadAsStringAsync();
                var myDeserializedClass = JsonConvert.DeserializeObject<Root>(content);
                if (myDeserializedClass.results[0].regularMarketPrice >= max)
                {
                    email.sendMessage(true);
                    Console.WriteLine("email de venda enviado");
                }
                if (myDeserializedClass.results[0].regularMarketPrice <= min)
                {
                    email.sendMessage(false);
                    Console.WriteLine("email de compra enviado");
                }
                Thread.Sleep(300000);
            }
           
        }
    }

   
}
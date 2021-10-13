using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Parser.DataFolder;
using Parser.Model;

namespace Parser.Hub
{
    class ProductHub
    {
        Data data = new Data();
        HubConnection hubConnection;
        public ProductHub()
        {
            hubConnection = new HubConnectionBuilder()
           .WithUrl("https://localhost:44354/collectionhub")
           .Build();

            hubConnection.On<List<Product>>("ReceiveMessage", messageCol =>
            {

              var  messages = messageCol;
               
            });
            var x = Task.Run(async () => await hubConnection.StartAsync());

            try
            {
                x.Wait();
            }
            catch  { }
           
            Send(new Product());
        }

         public void Send(Product product)
        {
            if(hubConnection.State == HubConnectionState.Disconnected)
            {
                data.AddProduct(product);
            }
            else
            {
              var x = Task.Run(async () => await hubConnection.SendAsync("SendMessage", product));
            }
            
        }
      
    }
}


using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Client.Blazor.Wasm.Pages
{
	public partial class Index : IDisposable
	{
		private HubConnection _hubConnection;

		public List<ProductViewModel> ExchangedData = new List<ProductViewModel>();

		protected async override Task OnInitializedAsync()
		{
			await StartHubConnection();
			await _hubConnection.SendAsync("GetProducts");

			//AddTransferProductsDataListener();
			AddExchangeDataListener();
		}

		private async Task StartHubConnection()
		{
			_hubConnection = new HubConnectionBuilder()
							.WithUrl("http://localhost:4300/TransferHub")
							.Build();

			await _hubConnection.StartAsync();
			if (_hubConnection.State == HubConnectionState.Connected)
				Console.WriteLine("connection started");
		}

		//private void AddTransferProductsDataListener()
		//{
		//	_hubConnection.On<List<ProductViewModel>>("GetProducts", (data) =>
		//	{
		//		ExchangedData = data;
		//		StateHasChanged();
		//	});
		//}

		private async Task DeleteProduct(int id)
        {
			await StartHubConnection();
			await _hubConnection.InvokeAsync("DeleteProduct", id);
		}


		private void AddExchangeDataListener()
		{
			_hubConnection.On<List<ProductViewModel>>("GetProducts", (data) =>
			{
				ExchangedData = data;
				StateHasChanged();
			});
		}

		public void Dispose()
		{
			_hubConnection.DisposeAsync();
		}
	}
}
